using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;

/// <summary>
/// Fournit des méthodes pour gérer les utilisateurs dans la base de données.
/// </summary>
public class UtilisateurDAO
{
    /// <summary>
    /// Crée un nouvel utilisateur dans la base de données.
    /// </summary>
    /// <param name="user">L'utilisateur à créer.</param>
    /// <returns>Retourne <c>true</c> si l'utilisateur a été créé avec succès, sinon <c>false</c>.</returns>
    public bool CreerUtilisateur(Utilisateur user)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = @"INSERT INTO Utilisateur (Nom, Email, MotDePasse, Sel, TokenValidation, EstValide)
               VALUES (@Nom, @Email, @MotDePasse, @Sel, @Token, @Valide)";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nom", user.Nom);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@MotDePasse", user.MotDePasse);
                cmd.Parameters.AddWithValue("@Sel", user.Sel);
                cmd.Parameters.AddWithValue("@Token", user.TokenValidation);
                cmd.Parameters.AddWithValue("@Valide", user.EstValide);

                try
                {
                    cmd.ExecuteNonQuery();
                    MailHelper.EnvoyerMailValidation(user.Email, user.TokenValidation, user.Nom);
                    return true;
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }

    /// <summary>
    /// Récupère un utilisateur par son nom ou son adresse e-mail.
    /// </summary>
    /// <param name="identifiant">Le nom ou l'adresse e-mail de l'utilisateur.</param>
    /// <returns>Retourne l'utilisateur correspondant ou <c>null</c> s'il n'existe pas.</returns>
    public Utilisateur GetByNomOuEmail(string identifiant)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM Utilisateur WHERE Nom = @id OR Email = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", identifiant);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Utilisateur
                        {
                            Id = reader.GetInt32("Id"),
                            Nom = reader.GetString("Nom"),
                            Email = reader.GetString("Email"),
                            MotDePasse = reader.GetString("MotDePasse"),
                            Sel = reader.GetString("Sel"),
                            EstValide = reader.GetBoolean("EstValide"),
                            DateCreation = reader.GetDateTime("DateCreation")
                        };
                    }
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Récupère un utilisateur par son identifiant unique.
    /// </summary>
    /// <param name="id">L'identifiant unique de l'utilisateur.</param>
    /// <returns>Retourne l'utilisateur correspondant ou <c>null</c> s'il n'existe pas.</returns>
    public Utilisateur GetById(int id)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM Utilisateur WHERE Id = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Utilisateur()
                        {
                            Id = reader.GetInt32("Id"),
                            Nom = reader.GetString("Nom"),
                            Email = reader.GetString("Email"),
                            MotDePasse = reader.GetString("MotDePasse"),
                            Sel = reader.GetString("Sel"),
                            DateCreation = reader.GetDateTime("DateCreation"),
                            EstValide = reader.GetBoolean("EstValide"),
                            TokenValidation = reader.IsDBNull(reader.GetOrdinal("TokenValidation")) ? null : reader.GetString("TokenValidation"),
                            Code2FA = reader.IsDBNull(reader.GetOrdinal("Code2FA")) ? null : reader.GetString("Code2FA"),
                            Code2FA_Date = reader.IsDBNull(reader.GetOrdinal("Code2FA_Date")) ? (DateTime?)null : reader.GetDateTime("Code2FA_Date")
                        };
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Valide un utilisateur en utilisant son token de validation.
    /// </summary>
    /// <param name="token">Le token de validation de l'utilisateur.</param>
    /// <returns>Retourne <c>true</c> si l'utilisateur a été validé avec succès, sinon <c>false</c>.</returns>
    public bool ValiderUtilisateurParToken(string token)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();

            string select = "SELECT * FROM Utilisateur WHERE TokenValidation = @token AND EstValide = 0";
            using (var cmdSelect = new MySqlCommand(select, conn))
            {
                cmdSelect.Parameters.AddWithValue("@token", token);
                using (var reader = cmdSelect.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return false;
                }
            }

            string update = "UPDATE Utilisateur SET EstValide = 1, TokenValidation = NULL WHERE TokenValidation = @token";
            using (var cmdUpdate = new MySqlCommand(update, conn))
            {
                cmdUpdate.Parameters.AddWithValue("@token", token);
                return cmdUpdate.ExecuteNonQuery() > 0;
            }
        }
    }

    /// <summary>
    /// Enregistre un code de validation à deux facteurs (2FA) pour un utilisateur.
    /// </summary>
    /// <param name="utilisateurId">L'identifiant de l'utilisateur.</param>
    /// <param name="code">Le code 2FA à enregistrer.</param>
    /// <param name="date">La date de génération du code 2FA.</param>
    public void EnregistrerCode2FA(int utilisateurId, string code, DateTime date)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            var sql = "UPDATE Utilisateur SET Code2FA = @code, Code2FA_Date = @date WHERE Id = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@id", utilisateurId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Invalide le code de validation à deux facteurs (2FA) pour un utilisateur.
    /// </summary>
    /// <param name="utilisateurId">L'identifiant de l'utilisateur.</param>
    public void InvaliderCode2FA(int utilisateurId)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            var sql = "UPDATE Utilisateur SET Code2FA = NULL, Code2FA_Date = NULL WHERE Id = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", utilisateurId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}