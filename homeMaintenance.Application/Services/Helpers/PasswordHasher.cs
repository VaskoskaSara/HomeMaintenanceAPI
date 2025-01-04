using homeMaintenance.Application.Ports.In;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace homeMaintenance.Application.Services.Helpers
{
    public class PasswordHasher : IPasswordHasher
    {
        public bool IsValidStrongPassword(string password)
        {
            if (password.Length < 8 || !Regex.IsMatch(password, @"[0-9]") || !Regex.IsMatch(password, @"[a-z]") ||
                !Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[@$%*?&#]"))
            {
                return false;
            }

            return true;
        }

        public string HashPassword(string password, byte[] salt)
        {
            try
            {
                using var hasher = new Argon2id(Encoding.UTF8.GetBytes(password))
                {
                    Salt = salt,
                    Iterations = 4,
                    MemorySize = 8 * 1024,
                    DegreeOfParallelism = 8
                };
                var hashBytes = hasher.GetBytes(32);
                var saltedHash = new byte[salt.Length + hashBytes.Length];
                Buffer.BlockCopy(salt, 0, saltedHash, 0, salt.Length);
                Buffer.BlockCopy(hashBytes, 0, saltedHash, salt.Length, hashBytes.Length);

                return Convert.ToBase64String(saltedHash);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerifyPassword(string password, string storedPasswordHash)
        {
            var storedHashBytes = Convert.FromBase64String(storedPasswordHash);
            var salt = new byte[16];
            var hash = new byte[storedHashBytes.Length - salt.Length];

            Buffer.BlockCopy(storedHashBytes, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(storedHashBytes, salt.Length, hash, 0, hash.Length);

            using var hasher = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                Iterations = 4,
                MemorySize = 8 * 1024,
                DegreeOfParallelism = 8
            };
            var computedHash = hasher.GetBytes(32);
            return hash.SequenceEqual(computedHash);
        }

        public byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}
