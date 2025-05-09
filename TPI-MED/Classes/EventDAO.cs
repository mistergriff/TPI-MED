using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

/// <summary>
/// Fournit des méthodes pour gérer les événements dans la base de données.
/// </summary>
public class EventDAO
{
    /// <summary>
    /// Ajoute un nouvel événement dans la base de données.
    /// </summary>
    /// <param name="evt">L'événement à ajouter.</param>
    /// <returns>L'identifiant de l'événement nouvellement ajouté.</returns>
    public int AjouterEvenement(Event evt)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = @"INSERT INTO events (date, subject, person, adminTime, users_id, interviews_id)
                           VALUES (@date, @subject, @person, @adminTime, @userId, @interviewId)";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@date", evt.Date);
                cmd.Parameters.AddWithValue("@subject", evt.Sujet);
                cmd.Parameters.AddWithValue("@person", evt.Personne);
                cmd.Parameters.AddWithValue("@adminTime", evt.TempsAdmin);
                cmd.Parameters.AddWithValue("@userId", evt.UserId);
                cmd.Parameters.AddWithValue("@interviewId", evt.InterviewId);

                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }
        }
    }

    /// <summary>
    /// Récupère une liste d'événements associés à un utilisateur spécifique.
    /// </summary>
    /// <param name="userId">L'identifiant de l'utilisateur.</param>
    /// <returns>Une liste d'événements associés à l'utilisateur.</returns>
    public List<Event> GetByUserId(int userId)
    {
        var result = new List<Event>();

        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM events WHERE users_id = @userId";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Event()
                        {
                            Id = reader.GetInt32("id"),
                            Date = reader.GetDateTime("date"),
                            Sujet = reader.GetString("subject"),
                            Personne = reader.GetString("person"),
                            TempsAdmin = reader.GetInt32("adminTime"),
                            UserId = reader.GetInt32("users_id"),
                            InterviewId = reader.IsDBNull(reader.GetOrdinal("interviews_id")) ? (int?)null : reader.GetInt32("interviews_id")
                        });
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Récupère un événement par son identifiant unique.
    /// </summary>
    /// <param name="id">L'identifiant de l'événement.</param>
    /// <returns>L'événement correspondant ou <c>null</c> s'il n'existe pas.</returns>
    public Event GetById(int id)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM events WHERE id = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Event
                        {
                            Id = reader.GetInt32("id"),
                            Date = reader.GetDateTime("date"),
                            Sujet = reader.GetString("subject"),
                            Personne = reader.GetString("person"),
                            TempsAdmin = reader.GetInt32("adminTime"),
                            UserId = reader.GetInt32("users_id"),
                            InterviewId = reader.IsDBNull(reader.GetOrdinal("interviews_id")) ? (int?)null : reader.GetInt32("interviews_id")
                        };
                    }
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Récupère les années scolaires disponibles en fonction des événements existants.
    /// </summary>
    /// <returns>Une liste des années scolaires disponibles (ex. : "2024-2025").</returns>
    public List<string> GetAvailableYears(int id)
    {
        var result = new HashSet<string>();

        // Récupérer tous les événements
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = "SELECT date FROM events WHERE users_id = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Calculer l'année scolaire à partir de la date
                        var eventDate = reader.GetDateTime("date");
                        string anneeScolaire = GetAnneeScolaire(eventDate);
                        result.Add(anneeScolaire);
                    }
                }
            }
        }

        // Retourner les années triées
        var sortedYears = new List<string>(result);
        sortedYears.Sort();
        return sortedYears;
    }

    /// <summary>
    /// Calcule l'année scolaire à partir d'une date donnée.
    /// </summary>
    /// <param name="date">La date de l'événement.</param>
    /// <returns>L'année scolaire correspondante (ex. : "2024-2025").</returns>
    private string GetAnneeScolaire(DateTime date)
    {
        if (date.Month >= 8) // Si le mois est août ou plus tard
        {
            return $"{date.Year}-{date.Year + 1}";
        }
        else // Si le mois est avant août
        {
            return $"{date.Year - 1}-{date.Year}";
        }
    }

    /// <summary>
    /// Modifie un événement existant dans la base de données.
    /// </summary>
    /// <param name="evt">L'événement à modifier.</param>
    /// <returns><c>true</c> si la modification a réussi, sinon <c>false</c>.</returns>
    public bool ModifierEvenement(Event evt)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = @"UPDATE events 
                       SET date = @date, subject = @subject, person = @person, adminTime = @adminTime, users_id = @userId, interviews_id = @interviewId 
                       WHERE id = @id";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@date", evt.Date);
                cmd.Parameters.AddWithValue("@subject", evt.Sujet);
                cmd.Parameters.AddWithValue("@person", evt.Personne);
                cmd.Parameters.AddWithValue("@adminTime", evt.TempsAdmin);
                cmd.Parameters.AddWithValue("@userId", evt.UserId);
                cmd.Parameters.AddWithValue("@interviewId", evt.InterviewId.HasValue ? (object)evt.InterviewId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@id", evt.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    /// <summary>
    /// Supprime un événement de la base de données, ainsi que l'entretien associé s'il existe.
    /// </summary>
    /// <param name="id">L'identifiant de l'événement à supprimer.</param>
    /// <returns><c>true</c> si la suppression a réussi, sinon <c>false</c>.</returns>
    public bool SupprimerEvenement(int id)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();

            // 1. Récupérer l'ID de l'interview liée
            int? interviewId = null;
            string getInterviewSql = "SELECT interviews_id FROM events WHERE id = @id";
            using (var getCmd = new MySqlCommand(getInterviewSql, conn))
            {
                getCmd.Parameters.AddWithValue("@id", id);
                var result = getCmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    interviewId = Convert.ToInt32(result);
            }

            // 2. Supprimer l’événement (en premier pour libérer la contrainte)
            string deleteEventSql = "DELETE FROM events WHERE id = @id";
            using (var deleteCmd = new MySqlCommand(deleteEventSql, conn))
            {
                deleteCmd.Parameters.AddWithValue("@id", id);
                deleteCmd.ExecuteNonQuery();
            }

            // 3. Supprimer l’interview si elle existe
            if (interviewId.HasValue)
            {
                string deleteInterviewSql = "DELETE FROM interviews WHERE id = @interviewId";
                using (var deleteCmd = new MySqlCommand(deleteInterviewSql, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@interviewId", interviewId.Value);
                    deleteCmd.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}