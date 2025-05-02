using MySql.Data.MySqlClient;
using Mysqlx.Crud;

public class InterviewDAO
{
    public int AjouterInterview(Interview itv)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = @"INSERT INTO interviews (
                            time, interviews_types_id,
                            addictive_behaviors, critical_incident, student_conflict, incivility_violence, grief,
                            unhappiness, learning_difficulties, career_guidance_issues, family_difficulties, stress,
                            financial_difficulties, suspected_abuse, discrimination, difficulties_tensions_with_a_teacher,
                            harassment_intimidation, gender_sexual_orientation, other)
                          VALUES (
                            @time, @type,
                            @ab, @ic, @sc, @iv, @gr,
                            @uh, @ld, @cg, @fd, @st,
                            @fdif, @sa, @dis, @dt,
                            @har, @gs, @oth)";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@time", itv.Time);
                cmd.Parameters.AddWithValue("@type", itv.InterviewTypeId);
                cmd.Parameters.AddWithValue("@ab", itv.addictive_behaviors);
                cmd.Parameters.AddWithValue("@ic", itv.critical_incident);
                cmd.Parameters.AddWithValue("@sc", itv.student_conflict);
                cmd.Parameters.AddWithValue("@iv", itv.incivility_violence);
                cmd.Parameters.AddWithValue("@gr", itv.grief);
                cmd.Parameters.AddWithValue("@uh", itv.unhappiness);
                cmd.Parameters.AddWithValue("@ld", itv.learning_difficulties);
                cmd.Parameters.AddWithValue("@cg", itv.career_guidance_issues);
                cmd.Parameters.AddWithValue("@fd", itv.family_difficulties);
                cmd.Parameters.AddWithValue("@st", itv.stress);
                cmd.Parameters.AddWithValue("@fdif", itv.financial_difficulties);
                cmd.Parameters.AddWithValue("@sa", itv.suspected_abuse);
                cmd.Parameters.AddWithValue("@dis", itv.discrimination);
                cmd.Parameters.AddWithValue("@dt", itv.difficulties_tensions_with_a_teacher);
                cmd.Parameters.AddWithValue("@har", itv.harassment_intimidation);
                cmd.Parameters.AddWithValue("@gs", itv.gender_sexual_orientation);
                cmd.Parameters.AddWithValue("@oth", itv.other);

                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }
        }
    }

    public Interview GetById(int id)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = "SELECT * FROM interviews WHERE id = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var interview = new Interview
                        {
                            Id = reader.GetInt32("id"),
                            Time = reader.GetInt32("time"),
                            InterviewTypeId = reader.GetInt32("interviews_types_id")
                        };

                        // Lire tous les champs booléens
                        foreach (var prop in typeof(Interview).GetProperties())
                        {
                            if (prop.PropertyType == typeof(bool))
                            {
                                int ordinal = reader.GetOrdinal(prop.Name);
                                if (!reader.IsDBNull(ordinal))
                                {
                                    prop.SetValue(interview, reader.GetBoolean(ordinal));
                                }
                            }
                        }

                        return interview;
                    }
                }
            }
        }

        return null;
    }
}