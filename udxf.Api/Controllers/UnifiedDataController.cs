using Microsoft.AspNetCore.Mvc;
using udxf.Utility;

namespace udxf.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnifiedDataController : ControllerBase
    {
        private readonly ILogger<UnifiedDataController> _logger;

        public UnifiedDataController(ILogger<UnifiedDataController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Reformat")]
        public IActionResult Reformat([FromBody] string data)
        {
            string passPhrase = "miko";
            var saltedKey = passPhrase.ApplySalt("salt");
            var encryptedData = data.Encrypt(saltedKey.Key, saltedKey.IV);

            var jsonResponse = new
            {
                Original = data,
                ReformattedData = data.Reformat(),
                EncryptedData = encryptedData,
                ReformattedDataFromEncryption = encryptedData.Reformat((saltedKey.Key, saltedKey.IV))
            };

            return Ok(jsonResponse);
        }
    }
}
