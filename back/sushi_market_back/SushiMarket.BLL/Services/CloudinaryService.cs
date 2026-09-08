using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;

namespace SushiMarket.BLL.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string?> UploadImageAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) return null;

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = $"sushi_market/{folderName}",
                PublicId = $"{fileNameWithoutExtension}_{Guid.NewGuid()}",
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl?.ToString();
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) return;

            try
            {
                var publicId = ExtractPublicIdFromUrl(imageUrl);
                if (string.IsNullOrEmpty(publicId)) return;

                var deleteParams = new DeletionParams(publicId);
                await _cloudinary.DestroyAsync(deleteParams);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string ExtractPublicIdFromUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                var segments = uri.Segments;

               int uploadIndex = Array.IndexOf(segments, "upload/");
                if (uploadIndex == -1 || uploadIndex >= segments.Length - 1)
                {
                    return string.Empty;
                }

               var pathSegments = segments.Skip(uploadIndex + 1).ToList();

               if (pathSegments.Count > 0 && pathSegments[0].StartsWith('v') && pathSegments[0].All(char.IsDigit))
                {
                    pathSegments.RemoveAt(0);
                }

                var fullPath = string.Concat(pathSegments);
                var publicId = Path.ChangeExtension(fullPath, null);

                return publicId;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}