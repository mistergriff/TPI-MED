/// <summary>
/// Représente un événement formaté pour l'affichage dans l'application.
/// </summary>
public class EventAffichage
{
    /// <summary>
    /// Obtient ou définit l'identifiant unique de l'événement.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtient ou définit la date de l'événement sous forme de chaîne.
    /// </summary>
    public string Date { get; set; }

    /// <summary>
    /// Obtient ou définit le sujet de l'événement.
    /// </summary>
    public string Sujet { get; set; }

    /// <summary>
    /// Obtient ou définit les personnes impliquées dans l'événement.
    /// </summary>
    public string Personnes { get; set; }

    /// <summary>
    /// Obtient ou définit la durée de l'événement (en minutes).
    /// </summary>
    public int Duree { get; set; }

    /// <summary>
    /// Obtient ou définit le temps administratif (en minutes) associé à l'événement.
    /// </summary>
    public int TempsAdmin { get; set; }

    /// <summary>
    /// Obtient ou définit le type de l'événement (par exemple, entretien ou séance).
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Obtient ou définit les motivations associées à l'événement.
    /// </summary>
    public string Motivations { get; set; }
}
