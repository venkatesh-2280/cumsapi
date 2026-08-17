using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace STAapi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtService;

        public AuthController(IJwtTokenService jwtService)
        {
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("generate-token")]
        public IActionResult GenerateToken()
        {
            //var encryptedIssuer = Encrypt("Gnsa~Flexicode");
            //var encryptedAudience = Encrypt("GnsaFlexiSTA-app");
            //var issuer = Encrypt(encryptedIssuer);
            //var audience = Encrypt(encryptedAudience);
            var token = _jwtService.GeneratePreLoginToken();
            return Ok(new { token });
        }

        private static readonly string SecretKey = "MY_SUPER_SECRET_KEY_123!"; // must be 16/24/32 bytes for AES

        public static string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(SecretKey);
            aes.IV = new byte[16]; // zero IV for simplicity (or generate random IV)

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(SecretKey);
            aes.IV = new byte[16]; // must match encryption IV

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
