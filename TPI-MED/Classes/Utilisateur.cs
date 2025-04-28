using System;

/// <summary>
/// Représente un utilisateur de l'application.
/// </summary>
public class Utilisateur
{
    /// <summary>
    /// Obtient ou définit l'identifiant unique de l'utilisateur.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtient ou définit le nom de l'utilisateur.
    /// </summary>
    public string Nom { get; set; }

    /// <summary>
    /// Obtient ou définit l'adresse e-mail de l'utilisateur.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Obtient ou définit le mot de passe haché de l'utilisateur.
    /// </summary>
    public string MotDePasse { get; set; }

    /// <summary>
    /// Obtient ou définit le sel utilisé pour le hachage du mot de passe.
    /// </summary>
    public string Sel { get; set; }

    /// <summary>
    /// Obtient ou définit la date de création du compte utilisateur.
    /// </summary>
    public DateTime DateCreation { get; set; }

    /// <summary>
    /// Obtient ou définit le token utilisé pour la validation de l'adresse e-mail.
    /// </summary>
    public string TokenValidation { get; set; }

    /// <summary>
    /// Obtient ou définit une valeur indiquant si l'utilisateur a validé son compte.
    /// Par défaut, cette valeur est <c>false</c>.
    /// </summary>
    public bool EstValide { get; set; } = false;

    /// <summary>
    /// Obtient ou définit le code de validation à deux facteurs (2FA) de l'utilisateur.
    /// </summary>
    public string Code2FA { get; set; }

    /// <summary>
    /// Obtient ou définit la date et l'heure de génération du code 2FA.
    /// </summary>
    public DateTime? Code2FA_Date { get; set; }
}