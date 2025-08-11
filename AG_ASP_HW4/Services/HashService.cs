using System.Security.Cryptography;
using System.Text;

namespace AP_project.Services
{
    public class HashService : IHashService
    {
        public string ComputeHash(string salt, string password)
        {
            byte[] passBytes = Encoding.Unicode.GetBytes(salt + password);
            byte[] hashBytes = SHA256.HashData(passBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        public string GenerateSalt()
        {
            byte[] saltBuf = new byte[16];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBuf);
            return BitConverter.ToString(saltBuf).Replace("-", "");
        }
    }
}