using System;
using System.Security.Cryptography;

namespace ClinicSystem.Helpers
{
    public class PasswordHasher
    {
        private const int Iterations = 10000;
        private const int SaltSize = 16;
        private const int KeySize = 32;

        public static string HashPassword(string plainPassword)
        {
            byte[] salt = new byte[SaltSize];

            using (RandomNumberGenerator rnd = RandomNumberGenerator.Create())
                rnd.GetBytes(salt);

            using (Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(plainPassword, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] key = rfc.GetBytes(KeySize);

                return $"{Iterations}.{Convert.ToBase64String(key)}.{Convert.ToBase64String(salt)}";
            }
        }
    }
}
