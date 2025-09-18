using Microsoft.AspNetCore.Mvc;
using TipsterApp.Data;

namespace TipsterApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipsController : ControllerBase
    {
        private readonly ITipStorage _storage;
        private readonly IConfiguration _config;

        public TipsController(ITipStorage storage, IConfiguration config)
        {
            _storage = storage;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader(Name = "Authorization")] string? auth)
        {
            var token = _config["ApiToken"];
            if (string.IsNullOrWhiteSpace(auth) || !auth.StartsWith("Bearer ") || auth.Substring(7) != token)
                return Unauthorized();

            var tips = await _storage.GetAllAsync();
            return Ok(tips);
        }
    }
}
