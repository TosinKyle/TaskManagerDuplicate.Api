using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using TaskManagerDuplicate.Domain.PasswordHasher.Interface;

namespace TaskManagerDuplicate.Domain.PasswordHasher.Implementation
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private readonly HashAlgorithmName _hashalgorithmname = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';
        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var HashPassword = Rfc2898DeriveBytes.Pbkdf2(password,salt,Iterations,_hashalgorithmname,KeySize);
            return string.Join(Delimiter,Convert.ToBase64String(HashPassword),Convert.ToBase64String(salt));
        }

        public bool VerifyPassword(string PasswordHash, string inputPassword)
        {
            var elements = PasswordHash.Split(Delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var HashPassword = Convert.FromBase64String(elements[1]);
            var HashInputPassword = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, _hashalgorithmname, KeySize);
            return CryptographicOperations.FixedTimeEquals(HashPassword,HashInputPassword);
        }

        public string SaltedPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            return Convert.ToBase64String(salt);
        }
    }
}
