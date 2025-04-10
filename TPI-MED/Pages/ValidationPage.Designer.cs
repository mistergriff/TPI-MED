using System;
using Wisej.Web;

partial class ValidationPage
{
    private Label lblMessage;
    private Button btnValider;

    private void InitializeComponent()
    {
        this.lblMessage = new Label()
        {
            Text = "Cliquez ci-dessous pour valider votre compte.",
            Font = new System.Drawing.Font("Segoe UI", 12),
            AutoSize = true,
            Location = new System.Drawing.Point(30, 40)
        };

        this.btnValider = new Button()
        {
            Text = "Valider mon compte",
            Location = new System.Drawing.Point(30, 90),
            Width = 200
        };
        this.btnValider.Click += new EventHandler(this.btnValider_Click);

        this.Controls.Add(this.lblMessage);
        this.Controls.Add(this.btnValider);

        this.Text = "Validation de compte";
        this.Size = new System.Drawing.Size(500, 200);
    }
}
