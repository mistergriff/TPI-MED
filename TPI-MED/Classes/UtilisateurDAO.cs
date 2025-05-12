//////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                      //
//      Date de création: 09.04.2025                                //
//      Description: Classe de gestion des utilisateurs avec la DB  //
//      Date de dernière révision: 12.05.2025                       //
//////////////////////////////////////////////////////////////////////

using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;

/// <summary>
/// Fournit des méthodes pour gérer les utilisateurs dans la base de données `users`.
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
            string sql = @"INSERT INTO users (name, Mail, password, Salt, token, isValid, CreationDate)
                           VALUES (@Nom, @Email, @MotDePasse, @Sel, @Token, @Valide, @DateCreation)";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nom", user.Nom);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@MotDePasse", user.MotDePasse);
                cmd.Parameters.AddWithValue("@Sel", user.Sel);
                cmd.Parameters.AddWithValue("@Token", user.TokenValidation);
                cmd.Parameters.AddWithValue("@Valide", user.EstValide);
                cmd.Parameters.AddWithValue("@DateCreation", user.DateCreation);

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

    public bool ExisteNomOuEmail(string nom, string email)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM users WHERE name = @nom OR mail = @mail";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@mail", email);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
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
            string sql = "SELECT * FROM users WHERE name = @id OR Mail = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", identifiant);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Utilisateur
                        {
                            Id = reader.GetInt32("id"),
                            Nom = reader.GetString("name"),
                            Email = reader.GetString("Mail"),
                            MotDePasse = reader.GetString("password"),
                            Sel = reader.GetString("Salt"),
                            EstValide = reader.GetBoolean("isValid"),
                            DateCreation = reader.GetDateTime("CreationDate"),
                            TokenValidation = reader.IsDBNull(reader.GetOrdinal("token")) ? null : reader.GetString("token"),
                            Code2FA = reader.IsDBNull(reader.GetOrdinal("2FACode")) ? null : reader.GetString("2FACode"),
                            Code2FA_Date = reader.IsDBNull(reader.GetOrdinal("2FACodeDate")) ? (DateTime?)null : reader.GetDateTime("2FACodeDate")
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
            string sql = "SELECT * FROM users WHERE id = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Utilisateur()
                        {
                            Id = reader.GetInt32("id"),
                            Nom = reader.GetString("name"),
                            Email = reader.GetString("Mail"),
                            MotDePasse = reader.GetString("password"),
                            Sel = reader.GetString("Salt"),
                            DateCreation = reader.GetDateTime("CreationDate"),
                            EstValide = reader.GetBoolean("isValid"),
                            TokenValidation = reader.IsDBNull(reader.GetOrdinal("token")) ? null : reader.GetString("token"),
                            Code2FA = reader.IsDBNull(reader.GetOrdinal("2FACode")) ? null : reader.GetString("2FACode"),
                            Code2FA_Date = reader.IsDBNull(reader.GetOrdinal("2FACodeDate")) ? (DateTime?)null : reader.GetDateTime("2FACodeDate")
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

            string select = "SELECT * FROM users WHERE token = @token AND isValid = 0";
            using (var cmdSelect = new MySqlCommand(select, conn))
            {
                cmdSelect.Parameters.AddWithValue("@token", token);
                using (var reader = cmdSelect.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return false;
                }
            }

            string update = "UPDATE users SET isValid = 1, token = NULL WHERE token = @token";
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
            var sql = "UPDATE users SET 2FACode = @code, 2FACodeDate = @date WHERE id = @id";
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
            var sql = "UPDATE users SET 2FACode = NULL, 2FACodeDate = NULL WHERE id = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", utilisateurId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
