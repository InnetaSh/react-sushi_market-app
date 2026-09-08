using Microsoft.AspNetCore.Http;

namespace SushiMarket.BLL.Services.Interfaces.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<string?> UploadImageAsync(IFormFile file, string folderName);
        Task DeleteImageAsync(string imageUrl);
    }

}
