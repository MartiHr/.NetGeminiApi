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
        public async Task<IActionResult> GenerateContent([FromBody] string prompt)
        {
            var result = await geminiService.GenerateContentAsync(prompt);
            return Ok(result);
        }
    }
}
