using System;
using Wisej.Web;

public partial class Code2FAPage : Page
{
    private int _utilisateurId;
    private UtilisateurDAO dao = new UtilisateurDAO();

    public Code2FAPage(int utilisateurId)
    {
        InitializeComponent();
        _utilisateurId = utilisateurId;
    }

    private void btnValider_Click(object sender, EventArgs e)
    {
        var utilisateur = dao.GetById(_utilisateurId);

        if (utilisateur == null || string.IsNullOrEmpty(utilisateur.Code2FA))
        {
            lblMessage.Text = "Utilisateur introuvable.";
            lblMessage.ForeColor = System.Drawing.Color.DarkRed;
            return;
        }

        // Vérifie si le code est bon et dans les 15 minutes
        if (txtCode.Text == utilisateur.Code2FA &&
            utilisateur.Code2FA_Date.HasValue &&
            DateTime.Now <= utilisateur.Code2FA_Date.Value.AddMinutes(15))
        {
            lblMessage.Text = "Code valide. Connexion réussie.";
            lblMessage.ForeColor = System.Drawing.Color.SeaGreen;

            // Nettoyer le code de la base
            dao.InvaliderCode2FA(_utilisateurId);

            // Redirection après un délai
            Timer delay = new Timer() { Interval = 1500 };
            delay.Tick += (s, args) =>
            {
                delay.Stop();
                Application.MainPage = new HomePage();
            };
            delay.Start();
        }
        else
        {
            lblMessage.Text = "Code invalide ou expiré.";
            lblMessage.ForeColor = System.Drawing.Color.DarkRed;
        }
    }
}
