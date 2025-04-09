using System;
using Wisej.Web;

public partial class ValidationPage : Page
{
    private string _token;
    private bool ok;

    public ValidationPage(string token)
    {
        InitializeComponent();
        _token = token;
        this.Load += ValidationPage_Load;
        this.Appear += ValidationPage_Appear;
    }

    private void ValidationPage_Appear(object sender, EventArgs e)
    {
        lblMessage.Text = ok
            ? "Votre compte a été validé avec succès !"
            : "Lien de validation invalide ou expiré.";
    }

    private void ValidationPage_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_token))
        {
            lblMessage.Text = "Token manquant.";
            return;
        }

        UtilisateurDAO dao = new UtilisateurDAO();
        ok = dao.ValiderUtilisateurParToken(_token);
    }

}