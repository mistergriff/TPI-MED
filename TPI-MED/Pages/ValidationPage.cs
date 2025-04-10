using System;
using Wisej.Web;

public partial class ValidationPage : Page
{
    private string _token;
    private Timer _delayTimer;

    public ValidationPage(string token)
    {
        InitializeComponent();
        _token = token;
    }

    private void btnValider_Click(object sender, EventArgs e)
    {
        UtilisateurDAO dao = new UtilisateurDAO();
        bool ok = dao.ValiderUtilisateurParToken(_token);

        if (!ok)
        {
            lblMessage.Text = "Lien invalide ou déjà utilisé.";
            return;
        }

        // Temporisation avant affichage du message
        _delayTimer = new Timer();
        _delayTimer.Interval = 3000; // 3 secondes
        _delayTimer.Tick += DelayTimer_Tick;
        _delayTimer.Start();
    }

    private void DelayTimer_Tick(object sender, EventArgs e)
    {
        _delayTimer.Stop();
        _delayTimer.Dispose();
        lblMessage.Text = "Votre compte a été validé avec succès ! Vous pouvez maintenant vous connecter.";
    }
}