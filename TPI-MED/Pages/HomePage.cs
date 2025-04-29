using System;
using Wisej.Web;
using Wisej.Web.Ext.NavigationBar;

/// <summary>
/// Représente la page d'accueil de l'application.
/// </summary>
public partial class HomePage : Page
{
    private Panel pageContainer;
    private NavigationBar navigationBar;
    private NavigationBarItem navAccueil;
    private NavigationBarItem navData;
    private NavigationBarItem navStats;
    private NavigationBarItem navDeconnexion;

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="HomePage"/>.
    /// </summary>
    public HomePage()
    {
        InitializeComponent();

        this.Text = "App";

        // Création de la barre de navigation
        this.navigationBar = new NavigationBar()
        {
            Dock = DockStyle.Left,
            Width = 180,
            BackColor = System.Drawing.Color.FromArgb(90, 90, 90),
            Logo = "./src/img/Logo.png"
        };

        this.navAccueil = new NavigationBarItem()
        {
            Text = "Accueil",
            Name = "Accueil",
            Icon = "./src/img/accueil.png"
        };

        this.navData = new NavigationBarItem()
        {
            Text = "Entrées",
            Name = "Entrées",
            Icon = "./src/img/data.png"
        };

        this.navStats = new NavigationBarItem()
        {
            Text = "Statistiques",
            Name = "Statistiques",
            Icon = "./src/img/chart.svg"
        };

        this.navDeconnexion = new NavigationBarItem()
        {
            Text = "Déconnexion",
            Name = "Déconnexion",
            Icon = "./src/img/logout.png"
        };

        this.navigationBar.Items.AddRange(new NavigationBarItem[] {
                navAccueil,
                navData,
                navStats,
            });
        this.navigationBar.Items.Add(navDeconnexion);

        this.navigationBar.ItemClick += NavigationBar_ItemClick;

        this.navigationBar.ShowUser = false;

        pageContainer = new Panel()
        {
            Dock = DockStyle.Fill
        };

        // Ajout des composants à la page
        this.Controls.Add(this.pageContainer);
        this.Controls.Add(this.navigationBar);

        // Charge la première "page"
        ShowPage(new AccueilPage());
    }



    /// <summary>
    /// Gère l'événement de clic sur le bouton de navigation "Accueil".
    /// Met à jour le contenu pour afficher un message de bienvenue.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void navAccueil_Click(object sender, EventArgs e)
    {
        ShowPage(new AccueilPage());
        this.Text = "Accueil";
    }

    private void ShowPage(UserControl page)
    {
        pageContainer.Controls.Clear();
        page.Dock = DockStyle.Fill;
        pageContainer.Controls.Add(page);
    }

    private void NavigationBar_ItemClick(object sender, NavigationBarItemClickEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Accueil":
                ShowPage(new AccueilPage());
                this.Text = "Accueil";
                break;
            case "Entrées":
                ShowPage(new DataPage());
                this.Text = "Entrées";
                break;
            case "Statistiques":
                ShowPage(new StatsPage());
                this.Text = "Statistiques";
                break;
            case "Déconnexion":
                if (MessageBox.Show("Voulez vous vous déconnecter du compte", buttons: MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.MainPage = new LoginPage();
                    //SessionUtilisateur.NomUtilisateur = null;
                    AlertBox.Show("Compte déconnecté avec succès.", icon: MessageBoxIcon.Information, autoCloseDelay: 10000, showProgressBar: true, showCloseButton: true);
                    break;
                }
                else
                {
                    break;
                }
        }
    }
}