using System;

/// <summary>
/// Représente un événement dans l'application.
/// </summary>
public class Event
{
    /// <summary>
    /// Obtient ou définit l'identifiant unique de l'événement.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtient ou définit la date de l'événement.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Obtient ou définit le sujet de l'événement.
    /// </summary>
    public string Sujet { get; set; }

    /// <summary>
    /// Obtient ou définit les personnes impliquées dans l'événement.
    /// </summary>
    public string Personne { get; set; }

    /// <summary>
    /// Obtient ou définit le temps administratif (en minutes) associé à l'événement.
    /// </summary>
    public int TempsAdmin { get; set; }

    /// <summary>
    /// Obtient ou définit l'identifiant de l'utilisateur associé à l'événement.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Obtient ou définit l'identifiant de l'entretien associé à l'événement, s'il existe.
    /// </summary>
    public int? InterviewId { get; set; }
}
