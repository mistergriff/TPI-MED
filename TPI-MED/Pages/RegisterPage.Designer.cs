using System;
using Wisej.Web;

partial class RegisterPage
{
    private Panel panelForm;
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
        // Panel centré
        this.panelForm = new Panel()
        {
            Width = 500,
            Height = 400,
            Anchor = AnchorStyles.None,
            BackColor = System.Drawing.Color.LightGray,
            Location = new System.Drawing.Point((this.Width - 500) / 2, (this.Height - 400) / 2)
        };

        this.lblNom = new Label() { Text = "Nom d'utilisateur :", Location = new System.Drawing.Point(30, 20) };
        this.txtNom = new TextBox() { Location = new System.Drawing.Point(180, 20), Width = 200 };

        this.lblEmail = new Label() { Text = "Email :", Location = new System.Drawing.Point(30, 60) };
        this.txtEmail = new TextBox() { Location = new System.Drawing.Point(180, 60), Width = 200 };

        this.lblMotDePasse = new Label() { Text = "Mot de passe :", Location = new System.Drawing.Point(30, 100) };
        this.txtMotDePasse = new TextBox() { Location = new System.Drawing.Point(180, 100), Width = 200, PasswordChar = '*' };

        this.lblConfirmation = new Label() { Text = "Confirmer :", Location = new System.Drawing.Point(30, 140) };
        this.txtConfirmation = new TextBox() { Location = new System.Drawing.Point(180, 140), Width = 200, PasswordChar = '*' };

        this.btnInscription = new Button() { Text = "S'inscrire", Location = new System.Drawing.Point(180, 190) };
        this.btnInscription.Click += new EventHandler(this.btnInscription_Click);

        this.btnRetour = new Button() { Text = "Retour", Location = new System.Drawing.Point(180, 230) };
        this.btnRetour.Click += new EventHandler(this.btnRetour_Click);

        this.panelForm.Controls.AddRange(new Control[]
        {
                lblNom, txtNom, lblEmail, txtEmail,
                lblMotDePasse, txtMotDePasse,
                lblConfirmation, txtConfirmation,
                btnInscription, btnRetour
        });

        // Ajout à la page
        this.Controls.Add(this.panelForm);

        this.Text = "Inscription";
        this.Size = new System.Drawing.Size(450, 300);
    }
}
