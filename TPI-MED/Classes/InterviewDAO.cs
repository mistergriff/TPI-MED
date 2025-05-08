using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Fournit des méthodes pour gérer les entretiens dans la base de données.
/// </summary>
public class InterviewDAO
{
    /// <summary>
    /// Ajoute un nouvel entretien dans la base de données.
    /// </summary>
    /// <param name="itv">L'entretien à ajouter.</param>
    /// <returns>L'identifiant de l'entretien nouvellement ajouté.</returns>
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

    /// <summary>
    /// Modifie un entretien existant dans la base de données.
    /// </summary>
    /// <param name="interview">L'entretien à modifier.</param>
    /// <returns><c>true</c> si la modification a réussi, sinon <c>false</c>.</returns>
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

    /// <summary>
    /// Récupère un entretien par son identifiant unique.
    /// </summary>
    /// <param name="id">L'identifiant de l'entretien.</param>
    /// <returns>L'entretien correspondant ou <c>null</c> s'il n'existe pas.</returns>
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

    /// <summary>
    /// Récupère la durée totale des entretiens par type pour un utilisateur donné.
    /// </summary>
    /// <param name="userId">L'identifiant de l'utilisateur.</param>
    /// <returns>Un dictionnaire contenant les types d'entretiens et leur durée totale.</returns>
    public Dictionary<string, int> GetDureeParType(int userId)
    {
        var result = new Dictionary<string, int>();

        using (var conn = Database.GetConnection())
        {
            conn.Open();

            string sql = @"
            SELECT it.name, SUM(i.time) AS duree
            FROM interviews i
            JOIN events e ON e.interviews_id = i.id
            JOIN interviews_types it ON it.id = i.interviews_types_id
            WHERE e.users_id = @userId
            GROUP BY it.name";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string label = reader.GetString("name");
                        int duree = reader.GetInt32("Duree");

                        result[label] = duree;
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Récupère les statistiques des motivations cochées pour un utilisateur donné.
    /// </summary>
    /// <param name="userId">L'identifiant de l'utilisateur.</param>
    /// <returns>Un dictionnaire contenant les motivations et leur somme.</returns>
    public Dictionary<string, int> GetStatsMotivations(int userId)
    {
        var motivations = new Dictionary<string, int>
        {
            { "addictive_behaviors", 0 },
            { "critical_incident", 0 },
            { "student_conflict", 0 },
            { "incivility_violence", 0 },
            { "grief", 0 },
            { "unhappiness", 0 },
            { "learning_difficulties", 0 },
            { "career_guidance_issues", 0 },
            { "family_difficulties", 0 },
            { "stress", 0 },
            { "financial_difficulties", 0 },
            { "suspected_abuse", 0 },
            { "discrimination", 0 },
            { "difficulties_tensions_with_a_teacher", 0 },
            { "harassment_intimidation", 0 },
            { "gender_sexual_orientation", 0 },
            { "other", 0 }
        };

        using (var conn = Database.GetConnection())
        {
            conn.Open();
            string sql = @"
            SELECT 
                SUM(addictive_behaviors) as addictive_behaviors,
                SUM(critical_incident) as critical_incident,
                SUM(student_conflict) as student_conflict,
                SUM(incivility_violence) as incivility_violence,
                SUM(grief) as grief,
                SUM(unhappiness) as unhappiness,
                SUM(learning_difficulties) as learning_difficulties,
                SUM(career_guidance_issues) as career_guidance_issues,
                SUM(family_difficulties) as family_difficulties,
                SUM(stress) as stress,
                SUM(financial_difficulties) as financial_difficulties,
                SUM(suspected_abuse) as suspected_abuse,
                SUM(discrimination) as discrimination,
                SUM(difficulties_tensions_with_a_teacher) as difficulties_tensions_with_a_teacher,
                SUM(harassment_intimidation) as harassment_intimidation,
                SUM(gender_sexual_orientation) as gender_sexual_orientation,
                SUM(other) as other
            FROM interviews i
            JOIN events e ON e.interviews_id = i.id
            WHERE e.users_id = @userId";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        foreach (var key in motivations.Keys.ToList())
                        {
                            motivations[key] = reader.IsDBNull(reader.GetOrdinal(key)) ? 0 : reader.GetInt32(key);
                        }
                    }
                }
            }
        }
        return motivations;
    }
}