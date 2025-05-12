//////////////////////////////////////////////////////////////////////
//      Auteur: WiseJ                                               //
//      Date de création: 09.04.2025                                //
//      Description: Classe autogénérée par Wisej                   //
//      Date de dernière révision: 12.05.2025                       //
//////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Wisej.Core;

namespace TPI_MED
{
    /// <summary>
    /// La classe <see cref="Startup"/> configure les services et le pipeline de requêtes de l'application.
    /// Pour plus d'informations sur la configuration de votre application, visitez https://go.microsoft.com/fwlink/?LinkID=398940.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">L'instance de configuration de l'application.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Obtient la configuration de l'application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// Configure et démarre le serveur web Wisej.
        /// </summary>
        /// <param name="args">Les arguments de ligne de commande passés à l'application.</param>
        public static void Main(string[] args)
        {
            // Crée un constructeur d'application web avec des options spécifiques.
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
                WebRootPath = "./"
            });

            // Construit l'application.
            var app = builder.Build();

            // Configure Wisej pour gérer les requêtes.
            app.UseWisej();

            // Active le serveur de fichiers pour servir les fichiers statiques.
            app.UseFileServer();

            // Démarre l'application.
            app.Run();
        }
    }
}