using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Fournit des méthodes utilitaires pour la gestion des mots de passe, y compris la génération de sel et le hachage.
/// </summary>
public static class PasswordHelper
{
    /// <summary>
    /// Génère un sel cryptographique aléatoire pour le hachage des mots de passe.
    /// </summary>
    /// <returns>Une chaîne Base64 représentant le sel généré.</returns>
    public static string GenerateSalt()
    {
        var buffer = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(buffer);
        }
        return Convert.ToBase64String(buffer);
    }

    /// <summary>
    /// Hache un mot de passe en utilisant un algorithme SHA-256 et un sel fourni.
    /// </summary>
    /// <param name="password">Le mot de passe à hacher.</param>
    /// <param name="salt">Le sel à utiliser pour le hachage.</param>
    /// <returns>Une chaîne Base64 représentant le mot de passe haché.</returns>
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