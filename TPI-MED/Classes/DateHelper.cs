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
}
