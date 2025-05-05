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

    public bool ModifierInterview(Interview interview)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = @"UPDATE interviews SET 
            time = @time,
            interviews_types_id = @typeId,
            addictive_behaviors = @addictive,
            critical_incident = @critical,
            student_conflict = @conflict,
            incivility_violence = @violence,
            grief = @grief,
            unhappiness = @unhappy,
            learning_difficulties = @learning,
            career_guidance_issues = @career,
            family_difficulties = @family,
            stress = @stress,
            financial_difficulties = @financial,
            suspected_abuse = @abuse,
            discrimination = @discrimination,
            difficulties_tensions_with_a_teacher = @tension,
            harassment_intimidation = @harassment,
            gender_sexual_orientation = @gender,
            other = @other
            WHERE id = @id";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", interview.Id);
                cmd.Parameters.AddWithValue("@time", interview.Time);
                cmd.Parameters.AddWithValue("@typeId", interview.InterviewTypeId);
                cmd.Parameters.AddWithValue("@addictive", interview.addictive_behaviors);
                cmd.Parameters.AddWithValue("@critical", interview.critical_incident);
                cmd.Parameters.AddWithValue("@conflict", interview.student_conflict);
                cmd.Parameters.AddWithValue("@violence", interview.incivility_violence);
                cmd.Parameters.AddWithValue("@grief", interview.grief);
                cmd.Parameters.AddWithValue("@unhappy", interview.unhappiness);
                cmd.Parameters.AddWithValue("@learning", interview.learning_difficulties);
                cmd.Parameters.AddWithValue("@career", interview.career_guidance_issues);
                cmd.Parameters.AddWithValue("@family", interview.family_difficulties);
                cmd.Parameters.AddWithValue("@stress", interview.stress);
                cmd.Parameters.AddWithValue("@financial", interview.financial_difficulties);
                cmd.Parameters.AddWithValue("@abuse", interview.suspected_abuse);
                cmd.Parameters.AddWithValue("@discrimination", interview.discrimination);
                cmd.Parameters.AddWithValue("@tension", interview.difficulties_tensions_with_a_teacher);
                cmd.Parameters.AddWithValue("@harassment", interview.harassment_intimidation);
                cmd.Parameters.AddWithValue("@gender", interview.gender_sexual_orientation);
                cmd.Parameters.AddWithValue("@other", interview.other);

                return cmd.ExecuteNonQuery() > 0;
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