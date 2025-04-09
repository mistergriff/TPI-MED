using System;
using System.Drawing;
using Wisej.Web;

partial class LoginPage
{
    private Panel panelForm;
    private Label lblIdentifiant;
    private Label lblMotDePasse;
    private TextBox txtIdentifiant;
    private TextBox txtMotDePasse;
    private Button btnConnexion;
    private Button btnCreerCompte;

    private void InitializeComponent()
    {
        // Panel centré
        this.panelForm = new Panel()
        {
            Width = 320,
            Height = 200,
            Anchor = AnchorStyles.None,
            BackColor = System.Drawing.Color.LightGray,
            Location = new System.Drawing.Point((this.Width - 320) / 2, (this.Height - 200) / 2)
        };

        // Labels et champs
        this.lblIdentifiant = new Label() { Text = "Nom ou email :", Location = new System.Drawing.Point(20, 20) };
        this.txtIdentifiant = new TextBox() { Location = new System.Drawing.Point(150, 20), Width = 140 };

        this.lblMotDePasse = new Label() { Text = "Mot de passe :", Location = new System.Drawing.Point(20, 60) };
        this.txtMotDePasse = new TextBox() { Location = new System.Drawing.Point(150, 60), Width = 140, PasswordChar = '*' };

        // Boutons
        this.btnConnexion = new Button() { Text = "Se connecter", Location = new System.Drawing.Point(150, 100) };
        this.btnConnexion.Click += new EventHandler(this.btnConnexion_Click);

        this.btnCreerCompte = new Button() { Text = "Créer un compte", Location = new System.Drawing.Point(150, 140) };
        this.btnCreerCompte.Click += new EventHandler(this.btnCreerCompte_Click);

        // Ajout au panel
        this.panelForm.Controls.AddRange(new Control[]
        {
                lblIdentifiant, txtIdentifiant,
                lblMotDePasse, txtMotDePasse,
                btnConnexion, btnCreerCompte
        });

        // Ajout à la page
        this.Controls.Add(this.panelForm);

        this.Text = "Connexion";
        this.Size = new System.Drawing.Size(800, 500);
    }
}
