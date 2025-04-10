using System;
using Wisej.Web;

partial class ValidationPage
{
    private Panel panelForm;
    private Label lblMessage;
    private Button btnValider;

    private void InitializeComponent()
    {
        // Panel centré
        this.panelForm = new Panel()
        {
            Size = new System.Drawing.Size(400, 200),
            Anchor = AnchorStyles.None,
            BackColor = System.Drawing.Color.WhiteSmoke,
            BorderStyle = BorderStyle.Solid,
            Location = new System.Drawing.Point((this.Width - 400) / 2, (this.Height - 200) / 2)
        };

        this.lblMessage = new Label()
        {
            Text = "Cliquez sur le bouton ci-dessous\npour valider votre compte.",
            Font = new System.Drawing.Font("Segoe UI", 12),
            AutoSize = true,
            Location = new System.Drawing.Point(20, 20),
            Width = 360,
            ForeColor = System.Drawing.Color.DimGray
        };

        this.btnValider = new Button()
        {
            Text = "✔️ Valider mon compte",
            Location = new System.Drawing.Point(100, 100),
            Size = new System.Drawing.Size(200, 40),
            BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        };
        this.btnValider.Click += new System.EventHandler(this.btnValider_Click);

        this.panelForm.Controls.Add(this.lblMessage);
        this.panelForm.Controls.Add(this.btnValider);
        this.Controls.Add(this.panelForm);

        this.Text = "Validation de compte";
        this.Size = new System.Drawing.Size(800, 400);
        //this.Appear += new EventHandler(this.ValidationPage_Appear);
    }
}
