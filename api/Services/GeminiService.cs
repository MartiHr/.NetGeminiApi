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

        public async Task<string> GenerateContentAsync(string prompt)
        {
            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var response = await _httpClient.PostAsJsonAsync($"{GeminiUrl}?key={_apiKey}", requestBody);

            // Ensure success
            response.EnsureSuccessStatusCode();

            // Return response as string (possibly deserialize it into a model)
            return await response.Content.ReadAsStringAsync();
        }
    }
}
