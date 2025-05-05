public class Seance
{
    public int Id { get; set; }         // events_have_sessions_type.id
    public int TypeId { get; set; }     // sessions_types.id
    public int EventId { get; set; }    // events_have_sessions_type.events_id
    public int Temps { get; set; }      // durée (time)
}
