using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHelper
{
    public static string GenerateSalt()
    {
        var buffer = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(buffer);
        }
        return Convert.ToBase64String(buffer);
    }

    public static string HashPassword(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] combined = Encoding.UTF8.GetBytes(password + salt);
            byte[] hash = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hash);
        }
    }
}
