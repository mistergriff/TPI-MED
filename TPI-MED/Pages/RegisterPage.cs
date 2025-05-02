using System;
using System.Linq;
using Wisej.Web;

/// <summary>
/// Représente la page d'inscription de l'application.
/// </summary>
public partial class RegisterPage : Page
{
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="RegisterPage"/>.
    /// </summary>
    public RegisterPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton d'inscription.
    /// Valide les données saisies par l'utilisateur et crée un nouveau compte si les validations réussissent.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
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
            EstValide = false,
            DateCreation = DateTime.Now
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

    /// <summary>
    /// Gère l'événement de clic sur le bouton de retour.
    /// Redirige l'utilisateur vers la page de connexion.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void btnRetour_Click(object sender, EventArgs e)
    {
        Application.MainPage = new LoginPage();
    }

    /// <summary>
    /// Vérifie si une adresse e-mail est valide.
    /// </summary>
    /// <param name="email">L'adresse e-mail à valider.</param>
    /// <returns>Retourne <c>true</c> si l'adresse e-mail est valide, sinon <c>false</c>.</returns>
    private bool IsValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(
            email,
            @"^[a-zA-Z0-9._%+-]+@(eduvaud\.ch|edu-vd\.ch|mediateur\.mycpnv\.ch)$",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Vérifie si un mot de passe respecte les critères de sécurité.
    /// </summary>
    /// <param name="pw">Le mot de passe à valider.</param>
    /// <returns>Retourne <c>true</c> si le mot de passe est valide, sinon <c>false</c>.</returns>
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