using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace SushiMarket.BLL.Helpers
{
    public class TranslatorHelper
    {
        public class TranslateApiResponse
        {
            [JsonPropertyName("translated_text")]
            public string TranslatedText { get; set; } = string.Empty;
        }

        public class Translator
        {
            private static readonly string ApiUrl =
                "https://api.translateapi.ai/api/v1/translate/";

            private readonly IConfiguration _configuration;

            public Translator(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<string> TranslateAsync(
                string text,
                string fromLang,
                string toLang)
            {
                if (string.IsNullOrWhiteSpace(text))
                    return text;

                var apiKey = _configuration["Translator:ApiKey"];

                if (string.IsNullOrWhiteSpace(apiKey))
                    return text;

                try
                {
                    using var httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Bearer",
                            apiKey);

                    var payload = new
                    {
                        text = text,
                        source_language = fromLang.ToLower(),
                        target_language = toLang.ToLower()
                    };

                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var content = JsonContent.Create(
                        payload,
                        options: options);

                    var response = await httpClient.PostAsync(
                        ApiUrl,
                        content);

                    var raw = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        return text;

                    var result =
                        JsonSerializer.Deserialize<TranslateApiResponse>(raw);

                    return result?.TranslatedText ?? text;
                }
                catch (Exception)
                {
                    return text;
                }
            }
        }
    }
}