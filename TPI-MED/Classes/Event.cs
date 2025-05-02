using System;

public class Event
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Sujet { get; set; }
    public string Personne { get; set; }
    public int TempsAdmin { get; set; }
    public int UserId { get; set; }
    public int? InterviewId { get; set; }
}