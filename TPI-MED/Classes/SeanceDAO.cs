using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class SeanceDAO
{
    /// <summary>
    /// Ajoute une ou plusieurs séances associées à un événement.
    /// </summary>
    /// <param name="eventId">L'ID de l'événement.</param>
    /// <param name="seances">Liste des séances à ajouter.</param>
    public void AjouterSeancesPourEvenement(int eventId, List<Seance> seances)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();

            foreach (var seance in seances)
            {
                string sql = @"INSERT INTO events_have_sessions_type (sessions_types_id, events_id, time)
                           VALUES (@sessionTypeId, @eventId, @time)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@sessionTypeId", seance.TypeId);
                    cmd.Parameters.AddWithValue("@eventId", eventId);
                    cmd.Parameters.AddWithValue("@time", seance.Temps);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public void ModifierSeancesPourEvenement(int eventId, List<Seance> seances)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();

            // Supprimer les séances existantes
            string deleteSql = "DELETE FROM events_have_sessions_type WHERE events_id = @eventId";
            using (var deleteCmd = new MySqlCommand(deleteSql, conn))
            {
                deleteCmd.Parameters.AddWithValue("@eventId", eventId);
                deleteCmd.ExecuteNonQuery();
            }

            // Réinsérer les nouvelles
            foreach (var seance in seances)
            {
                string insertSql = @"INSERT INTO events_have_sessions_type (sessions_types_id, events_id, time)
                                 VALUES (@typeId, @eventId, @time)";

                using (var insertCmd = new MySqlCommand(insertSql, conn))
                {
                    insertCmd.Parameters.AddWithValue("@typeId", seance.TypeId);
                    insertCmd.Parameters.AddWithValue("@eventId", eventId);
                    insertCmd.Parameters.AddWithValue("@time", seance.Temps);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }
    }


    public List<Seance> GetByEventId(int eventId)
    {
        var seances = new List<Seance>();

        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = "SELECT sessions_types_id, time FROM events_have_sessions_type WHERE events_id = @eventId";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@eventId", eventId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var seance = new Seance
                        {
                            TypeId = reader.GetInt32("sessions_types_id"),
                            Temps = reader.GetInt32("time")
                        };

                        seances.Add(seance);
                    }
                }
            }
        }

        return seances;
    }

    // Méthode pour SeanceDAO : Obtenir le total par type de session
    public Dictionary<string, int> GetDureeTotaleParType(int userId)
    {
        var result = new Dictionary<string, int>();

        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = @"
            SELECT st.name, SUM(ehst.time) AS total
            FROM events_have_sessions_type ehst
            JOIN sessions_types st ON ehst.sessions_types_id = st.id
            JOIN events e ON e.id = ehst.events_id
            WHERE e.users_id = @userId
            GROUP BY st.name
        ";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString("name");
                        var total = reader.GetInt32("total");
                        result[name] = total;
                    }
                }
            }
        }

        return result;
    }
}