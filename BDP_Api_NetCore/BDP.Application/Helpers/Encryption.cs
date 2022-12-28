using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Application.Helpers
{
    public class Encryption
    {
        /// <summary>
        /// Hash an ordinary string via salt
        /// </summary>
        /// <param name="text">Text to be encrypted</param>
        /// <param name="salt">Salt in byte array</param>
        /// <returns>Encrypted text</returns>
        public static string Hash(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed; 
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            return salt;
        }

        public static bool ValidatePassword(string password, string passwordHash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            return Hash(password, saltBytes) == passwordHash;
        }
    }
}
