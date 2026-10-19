using System;
using System.Collections.Generic;
using System.Linq;
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


        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            if (ValidationHelper.IsEmptyOrNull(plainPassword) ||
             string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }

            List<string> tokens = hashedPassword.Split(new char[] { '.' }).ToList();
            int iterations;
            byte[] key = new byte[KeySize];
            byte[] salt = new byte[SaltSize];

            try
            {
                iterations = Convert.ToInt32(tokens[0]);
                key = Convert.FromBase64String(tokens[1]);
                salt = Convert.FromBase64String(tokens[2]);
            }
            catch (Exception ex)
            {
                // log later
                return false;
            }

            using (Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(plainPassword, salt, iterations, HashAlgorithmName.SHA256))
            {
                byte[] newKey = rfc.GetBytes(KeySize);

                plainPassword = $"{Iterations}.{Convert.ToBase64String(newKey)}.{Convert.ToBase64String(salt)}";

                return plainPassword.Equals(hashedPassword);
            }
        }
    }
}
