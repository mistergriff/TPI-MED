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
                Application.MainPage = new ValidationPage(token);
            }
            else
            {
                Application.MainPage = new LoginPage();
            }
        }

        // 
        // Vous pouvez utiliser la m�thode d'entr�e ci-dessous
        // pour recevoir les param�tres de l'URL dans la collection args.
        //
        // static void Main(NameValueCollection args)
        // {
        // }
    }
}