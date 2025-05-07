using System;
using Wisej.Web;

/// <summary>
/// Représente la page d'accueil de l'application Journal de médiation.
/// </summary>
public partial class AccueilPage : UserControl
{
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="AccueilPage"/>.
    /// </summary>
    public AccueilPage()
    {
        InitializeComponent();

        this.Text = "Accueil";

        // Titre de bienvenue
        var title = new Label()
        {
            Text = "Bienvenue sur l'application\nJournal de médiation !",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        // Affichage de la date actuelle
        var label = new Label()
        {
            Text = "Bonjour, nous sommes le " + DateTime.Now.ToString("d"),
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        // Bouton pour accéder aux entrées
        var dataButton = new Button()
        {
            Text = "Accès aux entrées",
            Dock = DockStyle.Bottom,
            Height = 50
        };

        // Bouton pour accéder aux statistiques
        var statsButton = new Button()
        {
            Text = "Accès aux statistiques",
            Dock = DockStyle.Bottom,
            Height = 50
        };

        // Ajout des contrôles à la page
        this.Controls.AddRange(new Control[] {
            title,
            dataButton,
            statsButton
        });

        // Gestion des événements de clic
        dataButton.Click += DataButton_Click;
        statsButton.Click += StatsButton_Click;
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton "Accès aux statistiques".
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void StatsButton_Click(object sender, EventArgs e)
    {
        // Logique pour accéder aux statistiques (à implémenter)
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton "Accès aux entrées".
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void DataButton_Click(object sender, EventArgs e)
    {
        // Logique pour accéder aux entrées (à implémenter)
    }
}
