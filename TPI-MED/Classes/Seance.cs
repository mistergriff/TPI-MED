//////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                      //
//      Date de création: 02.05.2025                                //
//      Description: Classe d'obejt de type Seance                  //
//      Date de dernière révision: 12.05.2025                       //
//////////////////////////////////////////////////////////////////////

/// <summary>
/// Représente une séance associée à un événement dans l'application.
/// </summary>
public class Seance
{
    /// <summary>
    /// Obtient ou définit l'identifiant unique de la séance.
    /// Correspond à l'identifiant dans la table events_have_sessions_type.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtient ou définit l'identifiant du type de séance.
    /// Correspond à l'identifiant dans la table sessions_types.
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// Obtient ou définit l'identifiant de l'événement associé à la séance.
    /// Correspond à l'identifiant dans la table events_have_sessions_type.events_id.
    /// </summary>
    public int EventId { get; set; }

    /// <summary>
    /// Obtient ou définit la durée de la séance (en minutes).
    /// </summary>
    public int Temps { get; set; }
}
