using System;
using Wisej.Web;

partial class ValidationPage
{
    private Label lblMessage;

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

        this.Controls.Add(this.lblMessage);

        this.Text = "Validation de compte";
        this.Size = new System.Drawing.Size(600, 200);
        this.Load += new EventHandler(this.ValidationPage_Appear);
    }
}
