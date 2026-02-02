using System.Security.Cryptography;
using System.Text;

namespace BookMyTurfwebservices.Utilities;

public static class EncryptionHelper
{
    private static readonly byte[] Salt = Encoding.ASCII.GetBytes("BookMyTurf2024Salt");

    public static string Encrypt(string plainText, string encryptionKey)
    {
        if (string.IsNullOrEmpty(plainText))
            return string.Empty;

        using var aes = Aes.Create();
        aes.Key = DeriveKey(encryptionKey, 32);
        aes.IV = DeriveKey(encryptionKey, 16);

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
        cryptoStream.FlushFinalBlock();

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string Decrypt(string cipherText, string encryptionKey)
    {
        if (string.IsNullOrEmpty(cipherText))
            return string.Empty;

        try
        {
            using var aes = Aes.Create();
            aes.Key = DeriveKey(encryptionKey, 32);
            aes.IV = DeriveKey(encryptionKey, 16);

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            return streamReader.ReadToEnd();
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string GenerateHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }

    public static string GenerateSecureRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private static byte[] DeriveKey(string password, int keySize)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, Salt, 10000, HashAlgorithmName.SHA256);
        return deriveBytes.GetBytes(keySize);
    }
}