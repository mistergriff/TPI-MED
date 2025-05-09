using System;

public static class DateHelper
{
    public static bool EstDansAnneeScolaireCourante(DateTime date)
    {
        var aujourdHui = DateTime.Today;
        int anneeDebut;

        // Si on est entre janvier et juillet, l'année scolaire a commencé l'année précédente
        if (aujourdHui.Month >= 1 && aujourdHui.Month <= 7)
        {
            anneeDebut = aujourdHui.Year - 1;
        }
        else
        {
            anneeDebut = aujourdHui.Year;
        }

        DateTime debutAnneeScolaire = new DateTime(anneeDebut, 8, 1);
        DateTime finAnneeScolaire = new DateTime(anneeDebut + 1, 7, 31);

        return date >= debutAnneeScolaire && date <= finAnneeScolaire;
    }

    public static bool EstDansAnneeScolaire(DateTime date, string anneeScolaire)
    {
        // Format attendu : "2022-2023"
        var parties = anneeScolaire.Split('-');
        if (parties.Length != 2
            || !int.TryParse(parties[0], out int anneeDebut)
            || !int.TryParse(parties[1], out int anneeFin))
        {
            throw new ArgumentException("Format d'année scolaire invalide. Format attendu : '2022-2023'.");
        }

        var debut = new DateTime(anneeDebut, 8, 1);
        var fin = new DateTime(anneeFin, 7, 31);

        return date >= debut && date <= fin;
    }

    public static (DateTime debut, DateTime fin) GetPlageAnneeScolaire(string annee)
    {
        // annee doit être sous la forme "2024-2025"
        var parties = annee.Split('-');
        if (parties.Length != 2 || !int.TryParse(parties[0], out int debut) || !int.TryParse(parties[1], out int fin))
            throw new ArgumentException("Format d'année scolaire invalide, donnée reçue : " + annee);

        return (
            new DateTime(debut, 8, 1),
            new DateTime(fin, 7, 31)
        );
    }
}