using System.Security.Cryptography;
using System.Text;

namespace udxf.Utility
{
    public static class EncryptionExtensions
    {
        public static string Encrypt(this string plainText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        public static string Decrypt(this string cipherText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        public static (byte[] Key, byte[] IV) ApplySalt(this string password, string salt, int iterations = 10000, HashAlgorithmName? hashAlgorithmName = null)
        {
            var hashAlgorithm = hashAlgorithmName ?? HashAlgorithmName.SHA384;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), iterations, hashAlgorithm))
            {
                return (deriveBytes.GetBytes(32), deriveBytes.GetBytes(16));
            }
        }
    }
}
