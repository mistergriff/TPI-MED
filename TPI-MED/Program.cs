using Wisej.Web;
using Microsoft.Extensions.Configuration;

namespace TPI_MED
{
	static class Program
	{

        public static IConfiguration Configuration;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
		{
            var builder = new ConfigurationBuilder()
			.SetBasePath(Application.MapPath("~"))
			.AddJsonFile("appsettings.json", optional: true)
			.AddJsonFile("appsettings.Local.json", optional: true); // chargé en dernier

            Configuration = builder.Build();

            string token = Application.QueryString["token"];

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
		// You can use the entry method below
		// to receive the parameters from the URL in the args collection.
		//
		//static void Main(NameValueCollection args)
		//{
		//}
	}
}