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
        /// Obtient ou définit la configuration de l'application.
        /// </summary>
        public static IConfiguration Configuration;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// Configure la page principale en fonction des paramètres de la requête.
        /// </summary>
        static void Main()
        {
            // Initialise le constructeur de configuration pour charger les fichiers de configuration JSON.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Application.MapPath("~"))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Local.json", optional: true); // Chargé en dernier pour écraser les paramètres globaux.

            // Construit la configuration.
            Configuration = builder.Build();

            // Récupère le paramètre "token" de la chaîne de requête.
            string token = Application.QueryString["token"];

            // Définit la page principale de l'application en fonction de la présence du token.
            if (!string.IsNullOrEmpty(token))
            {
                // Si un token est présent, redirige vers la page de validation.
                Application.MainPage = new ValidationPage(token);
            }
            else
            {
                // Si aucun token n'est présent, redirige vers la page de connexion.
                Application.SetSessionTimeout(10 * 60); // Définit le délai d'inactivité de la session à 10 minutes.
                Application.MainPage = new LoginPage();
            }
        }

        // 
        // Vous pouvez utiliser la méthode d'entrée ci-dessous
        // pour recevoir les paramètres de l'URL dans la collection args.
        //
        // static void Main(NameValueCollection args)
        // {
        // }
    }
}