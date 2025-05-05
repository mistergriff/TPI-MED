using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class EventDAO
{
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