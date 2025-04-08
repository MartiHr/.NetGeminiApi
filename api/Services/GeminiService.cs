using api.Models.GeminiResponse;

namespace api.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey;
        private const string GeminiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

        public GeminiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Get API key from environment
            _apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
        }

        public async Task<GeminiResponse?> GenerateContentAsync(List<string> conversationHistory)
        {
            var oldParts = conversationHistory
                        .Select(message => new { text = message })
                        .ToArray();

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = oldParts }
                }
            };

            var response = await _httpClient.PostAsJsonAsync($"{GeminiUrl}?key={_apiKey}", requestBody);
            // Ensure success
            response.EnsureSuccessStatusCode();

            // Return response as string (possibly deserialize it into a model)
            GeminiResponse? geminiResponse = await response.Content.ReadFromJsonAsync<GeminiResponse>();
            return geminiResponse;
        }
    }
}
