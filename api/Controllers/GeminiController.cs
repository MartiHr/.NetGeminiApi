using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GeminiController : ControllerBase
    {
        private readonly GeminiService geminiService;

        public GeminiController(GeminiService _geminiService)
        {
            geminiService = _geminiService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateContent([FromBody] List<string> history)
        {
            var result = await geminiService.GenerateContentAsync(history);

            var text = result?.Candidates?.FirstOrDefault()?.Content?.Parts?
                .FirstOrDefault()?.Text;

            return Ok(new { response = text });
        }
    }
}
