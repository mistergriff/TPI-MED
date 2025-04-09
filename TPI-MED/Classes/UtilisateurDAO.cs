using MySql.Data.MySqlClient;
using System.Diagnostics;

public class UtilisateurDAO
{
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
}