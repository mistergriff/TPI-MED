//////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                      //
//      Date de création: 29.04.2025                                //
//      Description: UserControl qui affiche la page d'accueil      //
//      Date de dernière révision: 12.05.2025                       //
//////////////////////////////////////////////////////////////////////

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
            Text = $"Bienvenue {Application.Session["userName"]} sur l'application\nJournal de médiation !\nNous sommes le {DateTime.Now.ToString("d")}",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        // Ajout des contrôles à la page
        this.Controls.Add(title);
    }
}
