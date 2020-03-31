using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Linq;

namespace Chess.Users.Utilities
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Generate salt
            byte[] salt = GetSalt();

            // Generate hash with the specified password and salt
            var hash = KeyDerivation.Pbkdf2(password: password,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 10000,
                                numBytesRequested: 32);

            // Store hash and salt 
            byte[] hashedPassword = new byte[48];
            Buffer.BlockCopy(salt, 0, hashedPassword, 0, 16);
            Buffer.BlockCopy(hash, 0, hashedPassword, 16, 32);

            // Return hashed password
            return Convert.ToBase64String(hashedPassword);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Get hashed password and convert it into bytes
            byte[] hashedPasswordInBytes = Convert.FromBase64String(hashedPassword);

            // Extract salt from hashed password in bytes
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashedPasswordInBytes, 0, salt, 0, 16);

            // Generate hash with the specified password and extracted salt
            var hash = KeyDerivation.Pbkdf2(password: password,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 10000,
                                numBytesRequested: 32);

            // Compare new generated hash bytes with the old ones
            var hashBytes = new byte[32];
            Buffer.BlockCopy(hashedPasswordInBytes, 16, hashBytes, 0, 32);

            return hash.SequenceEqual<byte>(hashBytes);
        }

        private static byte[] GetSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}
