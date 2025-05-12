//////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                      //
//      Date de création: 09.04.2025                                //
//      Description: Page de login de l'application                 //
//      Date de dernière révision: 12.05.2025                       //
//////////////////////////////////////////////////////////////////////

using System;
using Wisej.Web;

/// <summary>
/// Représente la page de connexion de l'application.
/// </summary>
public partial class LoginPage : Page
{
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="LoginPage"/>.
    /// </summary>
    public LoginPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton de connexion.
    /// Vérifie les informations d'identification de l'utilisateur et initie le processus de connexion.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void btnConnexion_Click(object sender, EventArgs e)
    {
        string identifiant = txtIdentifiant.Text;
        string motDePasse = txtMotDePasse.Text;

        UtilisateurDAO dao = new UtilisateurDAO();
        Utilisateur utilisateur = dao.GetByNomOuEmail(identifiant);

        if (utilisateur == null)
        {
            MessageBox.Show("Utilisateur ou mot de passe incorrect.");
            return;
        }

        if (!utilisateur.EstValide)
        {
            MessageBox.Show("Veuillez valider votre compte par e-mail.");
            return;
        }

        string hash = PasswordHelper.HashPassword(motDePasse, utilisateur.Sel);
        if (hash == utilisateur.MotDePasse)
        {
            AlertBox.Show("Connexion réussie !", icon: MessageBoxIcon.Information);
            string code2FA = new Random().Next(100000, 999999).ToString();
            DateTime dateGeneration = DateTime.Now;

            dao.EnregistrerCode2FA(utilisateur.Id, code2FA, dateGeneration);
            MailHelper.EnvoyerCode2FA(utilisateur.Email, code2FA);

            // Redirige vers une page de saisie du code
            Application.MainPage = new Code2FAPage(utilisateur.Id);
        }
        else
        {
            MessageBox.Show("Utilisateur ou mot de passe incorrect.");
        }
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton de création de compte.
    /// Redirige l'utilisateur vers la page d'inscription.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void btnCreerCompte_Click(object sender, EventArgs e)
    {
        Application.MainPage = new RegisterPage();
    }
}