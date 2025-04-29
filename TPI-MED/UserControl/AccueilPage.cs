using System;
using Wisej.Web;


public partial class AccueilPage : UserControl
{
    public AccueilPage()
    {
        InitializeComponent();        

        this.Text = "Accueil";

        var title = new Label()
        {
            Text = "Bienvenue sur l'application\nJournal de médiation !",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };
        
        var label = new Label()
        {
            Text = "Bonjour, nous somme le " + DateTime.Now.ToString("d"),
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        var dataButton = new Button()
        {
            Text = "Accès aux entrées",
            Dock = DockStyle.Bottom,
            Height = 50 
        };

        var statsButton = new Button()
        {
            Text = "Accès aux statistiques",
            Dock = DockStyle.Bottom,
            Height = 50 
        };

        this.Controls.AddRange(new Control[] {
            title,
            dataButton,
            statsButton
        });

        dataButton.Click += DataButton_Click;
        statsButton.Click += StatsButton_Click;
    }

    private void StatsButton_Click(object sender, EventArgs e)
    {
        
    }

    private void DataButton_Click(object sender, EventArgs e)
    {
        
    }
}