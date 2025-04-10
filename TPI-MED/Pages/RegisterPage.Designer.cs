using System;
using Wisej.Web;

partial class RegisterPage
{
    private PictureBox logo;
    private Panel panelForm;
    private Label lblTitre;
    private Label lblNom;
    private Label lblEmail;
    private Label lblMotDePasse;
    private Label lblConfirmation;
    private TextBox txtNom;
    private TextBox txtEmail;
    private TextBox txtMotDePasse;
    private TextBox txtConfirmation;
    private Button btnInscription;
    private Button btnRetour;

    private void InitializeComponent()
    {
        // Panel principal
        this.panelForm = new Panel()
        {
            Size = new System.Drawing.Size(450, 570),
            Anchor = AnchorStyles.None,
            BackColor = System.Drawing.Color.WhiteSmoke,
            BorderStyle = BorderStyle.Solid,
            Location = new System.Drawing.Point((this.Width - 450) / 2, (this.Height - 570) / 2)
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
            Text = "Créer un compte",
            Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
            AutoSize = true,
            Location = new System.Drawing.Point(130, 215)
        };

        // Champ nom
        this.lblNom = new Label() { Text = "Nom d'utilisateur :", Location = new System.Drawing.Point(30, 260) };
        this.txtNom = new TextBox() { Location = new System.Drawing.Point(190, 260), Width = 200 };

        // Champ email
        this.lblEmail = new Label() { Text = "Email :", Location = new System.Drawing.Point(30, 300) };
        this.txtEmail = new TextBox() { Location = new System.Drawing.Point(190, 300), Width = 200 };

        // Champ mot de passe
        this.lblMotDePasse = new Label() { Text = "Mot de passe :", Location = new System.Drawing.Point(30, 340) };
        this.txtMotDePasse = new TextBox() { Location = new System.Drawing.Point(190, 340), Width = 200, PasswordChar = '*' };

        // Champ confirmation
        this.lblConfirmation = new Label() { Text = "Confirmer mot de passe :", Location = new System.Drawing.Point(30, 380) };
        this.txtConfirmation = new TextBox() { Location = new System.Drawing.Point(190, 380), Width = 200, PasswordChar = '*' };

        // Bouton inscription
        this.btnInscription = new Button()
        {
            Text = "S'inscrire",
            Location = new System.Drawing.Point(190, 430),
            Size = new System.Drawing.Size(200, 40),
            BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        };
        this.btnInscription.Click += new System.EventHandler(this.btnInscription_Click);

        // Bouton retour
        this.btnRetour = new Button()
        {
            Text = "Retour à la connexion",
            Location = new System.Drawing.Point(190, 480),
            Size = new System.Drawing.Size(200, 35),
            BackColor = System.Drawing.Color.Gainsboro,
            Font = new System.Drawing.Font("Segoe UI", 9)
        };
        this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click);

        // Ajout au panel
        this.panelForm.Controls.AddRange(new Control[]
        {
                logo,
                lblTitre, lblNom, txtNom,
                lblEmail, txtEmail,
                lblMotDePasse, txtMotDePasse,
                lblConfirmation, txtConfirmation,
                btnInscription, btnRetour
        });

        // Ajout à la page
        this.Controls.Add(this.panelForm);

        this.Text = "Inscription";
        this.Size = new System.Drawing.Size(800, 500);
    }
}
