using System;
using Wisej.Web.Ext.NavigationBar;
using Wisej.Web;

partial class HomePage
{
    private NavigationBar navigationBar;
    private NavigationBarItem navAccueil;
    private NavigationBarItem navDeconnexion;
    private Label lblContenu;

    private void InitializeComponent()
    {
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
            Icon = "./src/img/accueil.png"
        };
        this.navAccueil.Click += new EventHandler(this.navAccueil_Click);

        this.navDeconnexion = new NavigationBarItem()
        {
            Text = "Déconnexion",
            Icon = "./src/img/logout.png"
        };
        this.navDeconnexion.Click += new EventHandler(this.navDeconnexion_Click);

        this.navigationBar.Items.AddRange(new NavigationBarItem[] {
                navAccueil,
                navDeconnexion
            });

        // Contenu principal
        this.lblContenu = new Label()
        {
            Text = "Bienvenue dans l'application.",
            Location = new System.Drawing.Point(200, 50),
            Font = new System.Drawing.Font("Segoe UI", 14),
            AutoSize = true
        };

        // Ajout des composants à la page
        this.Controls.Add(this.navigationBar);
        this.Controls.Add(this.lblContenu);

        this.Text = "Accueil";
        this.Size = new System.Drawing.Size(800, 600);
    }
}
