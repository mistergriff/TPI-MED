using MySql.Data.MySqlClient;

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
}