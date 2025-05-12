/////////////////////////////////////////////////////////////////////////
//      Auteur: WiseJ                                                  //
//      Date de cr�ation: 09.04.2025                                   //
//      Description: Classe autog�n�r�e par WiseJ qui contient le main //
//      Date de derni�re r�vision: 12.05.2025                          //
/////////////////////////////////////////////////////////////////////////

using Wisej.Web;
using Microsoft.Extensions.Configuration;

namespace TPI_MED
{
    /// <summary>
    /// Classe principale de l'application.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Obtient ou d�finit la configuration de l'application.
        /// </summary>
        public static IConfiguration Configuration;

        /// <summary>
        /// Point d'entr�e principal de l'application.
        /// Configure la page principale en fonction des param�tres de la requ�te.
        /// </summary>
        static void Main()
        {
            // Initialise le constructeur de configuration pour charger les fichiers de configuration JSON.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Application.MapPath("~"))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Local.json", optional: true); // Charg� en dernier pour �craser les param�tres globaux.

            // Construit la configuration.
            Configuration = builder.Build();

            // R�cup�re le param�tre "token" de la cha�ne de requ�te.
            string token = Application.QueryString["token"];

            // D�finit la page principale de l'application en fonction de la pr�sence du token.
            if (!string.IsNullOrEmpty(token))
            {
                // Si un token est pr�sent, redirige vers la page de validation.
                Application.MainPage = new ValidationPage(token);
            }
            else
            {
                // Si aucun token n'est pr�sent, redirige vers la page de connexion.
                Application.SetSessionTimeout(10 * 60); // D�finit le d�lai d'inactivit� de la session � 10 minutes.
                Application.MainPage = new LoginPage();
            }
        }
    }
}