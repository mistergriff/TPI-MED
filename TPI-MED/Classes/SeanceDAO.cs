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
                string sql = @"INSERT INTO events_have_sessions_type (id, sessions_types_id, events_id, time)
                           VALUES (@id, @sessionTypeId, @eventId, @time)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@sessionTypeId", seance.TypeId);
                    cmd.Parameters.AddWithValue("@eventId", eventId);
                    cmd.Parameters.AddWithValue("@time", seance.Temps);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
