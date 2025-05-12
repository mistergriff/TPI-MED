////////////////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                                //
//      Date de création: 09.04.2025                                          //
//      Description: Page de validation du token après création du compte     //
//      Date de dernière révision: 12.05.2025                                 //
////////////////////////////////////////////////////////////////////////////////

using System;
using Wisej.Web;

/// <summary>
/// Représente la page de validation de compte utilisateur.
/// </summary>
public partial class ValidationPage : Page
{
    /// <summary>
    /// Le token de validation associé à l'utilisateur.
    /// </summary>
    private string _token;

    /// <summary>
    /// Timer utilisé pour gérer la temporisation avant l'affichage du message de validation.
    /// </summary>
    private Timer _delayTimer;

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="ValidationPage"/>.
    /// </summary>
    /// <param name="token">Le token de validation de l'utilisateur.</param>
    public ValidationPage(string token)
    {
        InitializeComponent();
        _token = token;
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton de validation.
    /// Valide le compte utilisateur en utilisant le token fourni.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void btnValider_Click(object sender, EventArgs e)
    {
        UtilisateurDAO dao = new UtilisateurDAO();
        bool ok = dao.ValidateUserByToken(_token);
        btnValider.Enabled = false; // Désactiver le bouton après clic

        if (!ok)
        {
            lblMessage.Text = "Lien invalide ou déjà utilisé.";
            return;
        }

        // Temporisation avant affichage du message
        _delayTimer = new Timer();
        _delayTimer.Interval = 1000; // 1 secondes
        _delayTimer.Tick += DelayTimer_Tick;
        _delayTimer.Start();
    }

    /// <summary>
    /// Gère l'événement Tick du timer.
    /// Affiche un message de confirmation après la temporisation.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void DelayTimer_Tick(object sender, EventArgs e)
    {
        _delayTimer.Stop();
        _delayTimer.Dispose();

        btnValider.Visible = false; // Masquer le bouton après validation
        lblMessage.Text = "Votre compte a bien été validé.\n Veuillez fermer cette page";
        lblMessage.ForeColor = System.Drawing.Color.SeaGreen;
        lblMessage.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
    }
}