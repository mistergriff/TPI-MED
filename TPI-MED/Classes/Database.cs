//////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                      //
//      Date de création: 09.04.2025                                //
//      Description: Classe de gestion de la base de données MySQL  //
//      Date de dernière révision: 12.05.2025                       //
//////////////////////////////////////////////////////////////////////


using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using TPI_MED;

/// <summary>
/// Fournit des méthodes pour gérer la connexion à la base de données MySQL.
/// </summary>
public class Database
{
    /// <summary>
    /// Chaîne de connexion utilisée pour se connecter à la base de données.
    /// </summary>
    private static string connectionString = Program.Configuration.GetConnectionString("Default");

    /// <summary>
    /// Obtient une nouvelle instance de connexion MySQL.
    /// </summary>
    /// <returns>Une instance de <see cref="MySqlConnection"/> configurée avec la chaîne de connexion.</returns>
    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}