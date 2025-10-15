using System.Security.Cryptography;

namespace Auth_API.Utility;

public static class CryptographyUtility
{


    public static string GenerateRefreshToken(int size = 64)
    {

        var randomBytes = new byte[size];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);

    }
    public static string HashPassword(string password)
    {
        // Configuration
        int saltSize = 16; // 128 bits
        int keySize = 32;  // 256 bits
        int iterations = 10_000;

        // Generate a cryptographic salt
        byte[] salt = RandomNumberGenerator.GetBytes(saltSize);

        // Derive a key (hash) from the password and salt
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        byte[] key = pbkdf2.GetBytes(keySize);

        // Combine salt + key + iteration count into a single string (Base64)
        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}.{iterations}";
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split('.');
        if (parts.Length != 3)
            return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedKey = Convert.FromBase64String(parts[1]);
        int iterations = int.Parse(parts[2]);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        byte[] computedKey = pbkdf2.GetBytes(storedKey.Length);

        return CryptographicOperations.FixedTimeEquals(computedKey, storedKey);
    }
}

