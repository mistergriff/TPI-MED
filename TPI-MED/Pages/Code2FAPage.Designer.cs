//////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                      //
//      Date de création: 10.04.2025                                //
//      Description: Designer de Page de validation du code 2FA     //
//      Date de dernière révision: 12.05.2025                       //
//////////////////////////////////////////////////////////////////////

using System;
using Wisej.Web;


partial class Code2FAPage
{
    private Panel panelForm;
    private Label lblTitre;
    private Label lblMessage;
    private TextBox txtCode;
    private Button btnValider;

    private void InitializeComponent()
    {
        this.panelForm = new Panel()
        {
            Size = new System.Drawing.Size(400, 200),
            BackColor = System.Drawing.Color.WhiteSmoke,
            BorderStyle = BorderStyle.Solid,
            Anchor = AnchorStyles.None,
            Location = new System.Drawing.Point((this.Width - 400) / 2, (this.Height - 200) / 2)
        };

        this.lblTitre = new Label()
        {
            Text = "Vérification en deux étapes",
            Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
            Location = new System.Drawing.Point(60, 20),
            AutoSize = true
        };

        this.txtCode = new TextBox()
        {
            Watermark = "Entrez le code à 6 chiffres",
            Location = new System.Drawing.Point(60, 70),
            Width = 280
        };

        this.btnValider = new Button()
        {
            Text = "Valider le code",
            Location = new System.Drawing.Point(60, 110),
            Size = new System.Drawing.Size(280, 35),
            BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        };
        this.btnValider.Click += new EventHandler(this.btnValider_Click);

        this.lblMessage = new Label()
        {
            Location = new System.Drawing.Point(60, 155),
            Size = new System.Drawing.Size(280, 20),
            ForeColor = System.Drawing.Color.DarkRed,
            AutoSize = true
        };

        this.panelForm.Controls.AddRange(new Control[] { lblTitre, txtCode, btnValider, lblMessage });
        this.Controls.Add(this.panelForm);

        this.Text = "Vérification 2FA";
        this.Size = new System.Drawing.Size(800, 400);

        MobileTabHelper.ActiverNavigation(this.panelForm, this.btnValider);
    }
}
