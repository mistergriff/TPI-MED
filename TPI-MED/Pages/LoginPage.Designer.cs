using System;
using System.Drawing;
using Wisej.Web;

partial class LoginPage
{
    private PictureBox logo;
    private Panel panelForm;
    private Label lblTitre;
    private Label lblIdentifiant;
    private Label lblMotDePasse;
    private TextBox txtIdentifiant;
    private TextBox txtMotDePasse;
    private Button btnConnexion;
    private Button btnCreerCompte;

    private void InitializeComponent()
    {
        // Panel contenant le formulaire
        this.panelForm = new Panel()
        {
            Size = new System.Drawing.Size(400, 480),
            Anchor = AnchorStyles.None,
            BackColor = System.Drawing.Color.WhiteSmoke,
            BorderStyle = BorderStyle.Solid,
            Location = new System.Drawing.Point((this.Width - 400) / 2, (this.Height - 480) / 2)
        };

        // Logo
        this.logo = new PictureBox()
        {
            ImageSource = "./src/img/LogoFull.png",
            BackColor = System.Drawing.Color.WhiteSmoke,
            Size = new System.Drawing.Size(200, 200),
            Location = new System.Drawing.Point((panelForm.Width - 200) / 2, 10),
            SizeMode = PictureBoxSizeMode.Zoom
        };

        // Titre
        this.lblTitre = new Label()
        {
            Text = "Connexion",
            Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
            AutoSize = true,
            Location = new System.Drawing.Point(140, 220)
        };

        // Identifiant
        this.lblIdentifiant = new Label()
        {
            Text = "Nom ou email :",
            Location = new System.Drawing.Point(30, 270),
            AutoSize = true
        };

        this.txtIdentifiant = new TextBox()
        {
            Location = new System.Drawing.Point(160, 270),
            Width = 200,
            Name = "txtIdentifiant"
        };

        // Mot de passe
        this.lblMotDePasse = new Label()
        {
            Text = "Mot de passe :",
            Location = new System.Drawing.Point(30, 310),
            AutoSize = true
        };

        this.txtMotDePasse = new TextBox()
        {
            Location = new System.Drawing.Point(160, 310),
            Width = 200,
            PasswordChar = '*',
            Name = "txtMotDePasse"
        };

        // Bouton Connexion
        this.btnConnexion = new Button()
        {
            Text = "Se connecter",
            Name = "btnConnexion",
            Location = new System.Drawing.Point(160, 360),
            Size = new System.Drawing.Size(200, 35),
            BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
        };
        //this.AcceptButton = this.btnConnexion; // Rendre le bouton de connexion le bouton par défaut
        this.btnConnexion.Click += new System.EventHandler(this.btnConnexion_Click);

        // Bouton Créer un compte
        this.btnCreerCompte = new Button()
        {
            Text = "Créer un compte",
            Name = "btnCreeCompte",
            Location = new System.Drawing.Point(160, 410),
            Size = new System.Drawing.Size(200, 35),
            BackColor = System.Drawing.Color.Gainsboro,
            ForeColor = System.Drawing.Color.Black,
            Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Regular)
        };
        this.btnCreerCompte.Click += new System.EventHandler(this.btnCreerCompte_Click);

        // Ajout au panel
        this.panelForm.Controls.Add(this.logo);
        this.panelForm.Controls.Add(this.lblTitre);
        this.panelForm.Controls.Add(this.lblIdentifiant);
        this.panelForm.Controls.Add(this.txtIdentifiant);
        this.panelForm.Controls.Add(this.lblMotDePasse);
        this.panelForm.Controls.Add(this.txtMotDePasse);
        this.panelForm.Controls.Add(this.btnConnexion);
        this.panelForm.Controls.Add(this.btnCreerCompte);

        // Ajout à la page
        this.Controls.Add(this.panelForm);

        this.panelForm.AutoTabIndex();

        // Propriétés générales de la page
        this.Text = "Connexion";
        this.Size = new System.Drawing.Size(800, 500);

        MobileTabHelper.ActiverNavigation(this.panelForm, this.btnConnexion);

    }
}