using System;
using Wisej.Web;

/// <summary>
/// Représente la page de validation du code à deux facteurs (2FA).
/// </summary>
public partial class Code2FAPage : Page
{
    /// <summary>
    /// Identifiant unique de l'utilisateur en cours de validation.
    /// </summary>
    private int _utilisateurId;

    /// <summary>
    /// Instance de <see cref="UtilisateurDAO"/> pour interagir avec la base de données.
    /// </summary>
    private UtilisateurDAO dao = new UtilisateurDAO();

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="Code2FAPage"/>.
    /// </summary>
    /// <param name="utilisateurId">L'identifiant de l'utilisateur pour lequel le code 2FA est validé.</param>
    public Code2FAPage(int utilisateurId)
    {
        InitializeComponent();
        _utilisateurId = utilisateurId;
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton de validation.
    /// Vérifie si le code 2FA saisi est valide et non expiré.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void btnValider_Click(object sender, EventArgs e)
    {
        var utilisateur = dao.GetById(_utilisateurId);

        if (utilisateur == null || string.IsNullOrEmpty(utilisateur.Code2FA))
        {
            lblMessage.Text = "Utilisateur introuvable.";
            lblMessage.ForeColor = System.Drawing.Color.DarkRed;
            return;
        }

        // Vérifie si le code est correct et s'il est encore valide (moins de 15 minutes).
        if (txtCode.Text == utilisateur.Code2FA &&
            utilisateur.Code2FA_Date.HasValue &&
            DateTime.Now <= utilisateur.Code2FA_Date.Value.AddMinutes(15))
        {
            lblMessage.Text = "Code valide. Connexion réussie.";
            lblMessage.ForeColor = System.Drawing.Color.SeaGreen;

            // Invalide le code 2FA dans la base de données.
            dao.InvaliderCode2FA(_utilisateurId);

            // Redirige l'utilisateur vers la page d'accueil après un délai.
            Timer delay = new Timer() { Interval = 1000 };
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