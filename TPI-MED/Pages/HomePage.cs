using System;
using Wisej.Web;

/// <summary>
/// Représente la page d'accueil de l'application.
/// </summary>
public partial class HomePage : Page
{
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="HomePage"/>.
    /// </summary>
    public HomePage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton de navigation "Accueil".
    /// Met à jour le contenu pour afficher un message de bienvenue.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void navAccueil_Click(object sender, EventArgs e)
    {
        lblContenu.Text = "Bienvenue dans l'application Journal de médiation.";
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton de navigation "Déconnexion".
    /// Redirige l'utilisateur vers la page de connexion.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void navDeconnexion_Click(object sender, EventArgs e)
    {
        Application.MainPage = new LoginPage();
    }
}