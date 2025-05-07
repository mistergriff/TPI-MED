/// <summary>
/// Représente un entretien dans l'application.
/// </summary>
public class Interview
{
    /// <summary>
    /// Obtient ou définit l'identifiant unique de l'entretien.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtient ou définit la durée de l'entretien (en minutes).
    /// </summary>
    public int Time { get; set; }

    /// <summary>
    /// Obtient ou définit l'identifiant du type d'entretien.
    /// </summary>
    public int InterviewTypeId { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne des conduites addictives.
    /// </summary>
    public bool addictive_behaviors { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne un incident critique.
    /// </summary>
    public bool critical_incident { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne un conflit entre élèves.
    /// </summary>
    public bool student_conflict { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne des incivilités ou des violences.
    /// </summary>
    public bool incivility_violence { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne un deuil.
    /// </summary>
    public bool grief { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne un mal-être.
    /// </summary>
    public bool unhappiness { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne des difficultés d'apprentissage.
    /// </summary>
    public bool learning_difficulties { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne des questions d'orientation professionnelle.
    /// </summary>
    public bool career_guidance_issues { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne des difficultés familiales.
    /// </summary>
    public bool family_difficulties { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne du stress.
    /// </summary>
    public bool stress { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne des difficultés financières.
    /// </summary>
    public bool financial_difficulties { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne une suspicion de maltraitance.
    /// </summary>
    public bool suspected_abuse { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne une discrimination.
    /// </summary>
    public bool discrimination { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne des difficultés ou tensions avec un enseignant.
    /// </summary>
    public bool difficulties_tensions_with_a_teacher { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne du harcèlement ou de l'intimidation.
    /// </summary>
    public bool harassment_intimidation { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne des questions de genre ou d'orientation sexuelle et affective.
    /// </summary>
    public bool gender_sexual_orientation { get; set; }

    /// <summary>
    /// Indique si l'entretien concerne un autre sujet.
    /// </summary>
    public bool other { get; set; }
}
