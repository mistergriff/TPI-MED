using System;
using System.Linq;
using Wisej.Web;

public partial class RegisterPage : Page
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private void btnInscription_Click(object sender, EventArgs e)
    {
        string nom = txtNom.Text;
        string email = txtEmail.Text;
        string motDePasse = txtMotDePasse.Text;
        string confirmation = txtConfirmation.Text;

        if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(motDePasse))
        {
            MessageBox.Show("Veuillez remplir tous les champs.");
            return;
        }

        if (!IsValidEmail(email))
        {
            MessageBox.Show("Veuillez entrer une adresse e-mail valide.");
            return;
        }

        if (!IsPasswordValid(motDePasse))
        {
            MessageBox.Show("Le mot de passe doit contenir au minimum 8 caractères, une majuscule et un chiffre.");
            return;
        }

        if (motDePasse != confirmation)
        {
            MessageBox.Show("Les mots de passe ne correspondent pas.");
            return;
        }

        string salt = PasswordHelper.GenerateSalt();
        string hash = PasswordHelper.HashPassword(motDePasse, salt);
        string token = Guid.NewGuid().ToString(); // token unique


        Utilisateur utilisateur = new Utilisateur
        {
            Nom = nom,
            Email = email,
            MotDePasse = hash,
            Sel = salt,
            TokenValidation = token,
            EstValide = false
        };

        UtilisateurDAO dao = new UtilisateurDAO();
        if (dao.CreerUtilisateur(utilisateur))
        {
            MessageBox.Show("Inscription réussie,\nveuillez valider votre adresse mail !");
            Application.MainPage = new LoginPage();
        }
        else
        {
            MessageBox.Show("Erreur : cet utilisateur existe déjà.", icon: MessageBoxIcon.Error);
        }
    }

    private void btnRetour_Click(object sender, EventArgs e)
    {
        Application.MainPage = new LoginPage();
    }

    private bool IsValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(
            email,
            @"^[a-zA-Z0-9._%+-]+@(eduvaud\.ch|edu-vaud\.ch|mediateur\.mycpnv\.ch)$",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

    private bool IsPasswordValid(string pw)
    {
        if (pw.Length < 8)
            return false;
        if (!pw.Any(char.IsUpper))
            return false;
        if (!pw.Any(char.IsDigit))
            return false;
        return true;
    }

}
