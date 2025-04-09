using System;
using Wisej.Web;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void navAccueil_Click(object sender, EventArgs e)
    {
        lblContenu.Text = "Bienvenue dans l'application Journal de médiation.";
    }

    private void navDeconnexion_Click(object sender, EventArgs e)
    {
        Application.MainPage = new LoginPage();
    }
}