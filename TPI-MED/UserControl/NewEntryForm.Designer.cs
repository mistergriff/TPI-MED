using System;
using Wisej.Web;


partial class NewEntryForm
{
    private DateTimePicker datePicker;
    private TextBox txtSujet;
    private TextBox txtPersonnes;
    private NumericUpDown numDuree;
    private RadioButton radioSeance;
    private RadioButton radioEntretien;
    private Button btnAjouter;

    private void InitializeComponent()
    {
        this.datePicker = new DateTimePicker()
        {
            Location = new System.Drawing.Point(30, 30),
            Width = 250
        };

        this.txtSujet = new TextBox()
        {
            Location = new System.Drawing.Point(30, 70),
            Watermark = "Sujet",
            Width = 250
        };

        this.txtPersonnes = new TextBox()
        {
            Location = new System.Drawing.Point(30, 110),
            Watermark = "Personnes concernées",
            Width = 250
        };

        this.numDuree = new NumericUpDown()
        {
            Location = new System.Drawing.Point(30, 150),
            Width = 100,
            Minimum = 0,
            Maximum = 300,
            Value = 30
        };

        this.radioSeance = new RadioButton()
        {
            Text = "Séance",
            Location = new System.Drawing.Point(30, 190),
            Checked = true
        };

        this.radioEntretien = new RadioButton()
        {
            Text = "Entretien",
            Location = new System.Drawing.Point(120, 190)
        };

        this.btnAjouter = new Button()
        {
            Text = "Ajouter",
            Location = new System.Drawing.Point(30, 240),
            Width = 120,
            BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
            ForeColor = System.Drawing.Color.White
        };
        this.btnAjouter.Click += new EventHandler(this.btnAjouter_Click);

        this.Controls.AddRange(new Control[]
        {
                datePicker, txtSujet, txtPersonnes,
                numDuree, radioSeance, radioEntretien, btnAjouter
        });

        this.Text = "Nouvelle entrée";
        this.Size = new System.Drawing.Size(350, 350);
        this.StartPosition = FormStartPosition.CenterParent;
    }
}
