using System;
using System.Threading;
using TPI_MED;
using Wisej.Web;

public partial class ValidationPage : Page
{
    private string _token;
    private bool ok;
    private Wisej.Web.Timer _delayTimer;

    public ValidationPage(string token)
    {
        InitializeComponent();
        _token = token;
        this.Load += ValidationPage_Load;
    }

    private void btnValider_Click(object sender, EventArgs e)
    {
        UtilisateurDAO dao = new UtilisateurDAO();
        bool ok = dao.ValiderUtilisateurParToken(_token);

        // Temporisation avant affichage du message
        _delayTimer = new Wisej.Web.Timer();
        _delayTimer.Interval = 3000; // 3 secondes
        _delayTimer.Tick += DelayTimer_Tick;
        _delayTimer.Start();
    }

    private void DelayTimer_Tick(object sender, EventArgs e)
    {
        _delayTimer.Stop();
        _delayTimer.Dispose();
        lblMessage.Text = ok ? "Votre compte a été validé avec succès ! Vous pouvez maintenant vous connecter." : "Lien invalide ou expiré.";

        if (ok)
        {
            // Rediriger vers la page de connexion après 3 secondes
            Thread.Sleep(3000);
            Application.MainPage = new LoginPage();
        }
    }

    private void ValidationPage_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_token))
        {
            lblMessage.Text = "Token manquant.";
            btnValider.Enabled = false;
            return;
        }
    }

}