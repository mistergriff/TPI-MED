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
            Text = "",
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 14),
            Dock = DockStyle.Fill,
            Location = new System.Drawing.Point(40, 60)
        };

        this.btnValider = new Button()
        {
            Text = "Valider le compte",
            Size = new System.Drawing.Size(100, 30),
            Location = new System.Drawing.Point(40, 120)
        };

        this.btnValider.Click += new EventHandler(this.btnValider_Click);

        this.Controls.Add(this.btnValider);
        this.Controls.Add(this.lblMessage);

        this.Text = "Validation de compte";
        this.Size = new System.Drawing.Size(600, 200);
    }
}
