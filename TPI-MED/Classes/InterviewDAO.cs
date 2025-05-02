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
                cmd.Parameters.AddWithValue("@ab", itv.AddictiveBehaviors);
                cmd.Parameters.AddWithValue("@ic", itv.CriticalIncident);
                cmd.Parameters.AddWithValue("@sc", itv.StudentConflict);
                cmd.Parameters.AddWithValue("@iv", itv.IncivilityViolence);
                cmd.Parameters.AddWithValue("@gr", itv.Grief);
                cmd.Parameters.AddWithValue("@uh", itv.Unhappiness);
                cmd.Parameters.AddWithValue("@ld", itv.LearningDifficulties);
                cmd.Parameters.AddWithValue("@cg", itv.CareerGuidanceIssues);
                cmd.Parameters.AddWithValue("@fd", itv.FamilyDifficulties);
                cmd.Parameters.AddWithValue("@st", itv.Stress);
                cmd.Parameters.AddWithValue("@fdif", itv.FinancialDifficulties);
                cmd.Parameters.AddWithValue("@sa", itv.SuspectedAbuse);
                cmd.Parameters.AddWithValue("@dis", itv.Discrimination);
                cmd.Parameters.AddWithValue("@dt", itv.TensionsWithTeacher);
                cmd.Parameters.AddWithValue("@har", itv.Harassment);
                cmd.Parameters.AddWithValue("@gs", itv.GenderOrientation);
                cmd.Parameters.AddWithValue("@oth", itv.Other);

                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }
        }
    }
}