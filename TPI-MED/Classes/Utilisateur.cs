using System;

public class Utilisateur
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public string MotDePasse { get; set; }
    public string Sel { get; set; }
    public DateTime DateCreation { get; set; }
    public string TokenValidation { get; set; } // Token pour la validation de l'email
    public bool EstValide { get; set; } = false; // Par défaut, l'utilisateur n'est pas validé
}