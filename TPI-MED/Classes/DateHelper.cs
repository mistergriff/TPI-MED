//////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                      //
//      Date de création: 08.05.2025                                //
//      Description: Classe de gestion des années scolaires         //
//      Date de dernière révision: 12.05.2025                       //
//////////////////////////////////////////////////////////////////////

using System;

/// <summary>
/// Fournit des méthodes utilitaires pour gérer les années scolaires.
/// </summary>
public static class DateHelper
{
    /// <summary>
    /// Vérifie si une date donnée se trouve dans l'année scolaire courante.
    /// </summary>
    /// <param name="date">La date à vérifier.</param>
    /// <returns><c>true</c> si la date est dans l'année scolaire courante, sinon <c>false</c>.</returns>
    public static bool IsInCurrentSchoolYear(DateTime date)
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

    /// <summary>
    /// Vérifie si une date donnée se trouve dans une année scolaire spécifique.
    /// </summary>
    /// <param name="date">La date à vérifier.</param>
    /// <param name="anneeScolaire">L'année scolaire au format "YYYY-YYYY" (par exemple, "2022-2023").</param>
    /// <returns><c>true</c> si la date est dans l'année scolaire spécifiée, sinon <c>false</c>.</returns>
    /// <exception cref="ArgumentException">L'année scolaire n'est pas dans un format valide.</exception>
    public static bool IsInSchoolYear(DateTime date, string anneeScolaire)
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

    /// <summary>
    /// Obtient la plage de dates correspondant à une année scolaire donnée.
    /// </summary>
    /// <param name="annee">L'année scolaire au format "YYYY-YYYY" (par exemple, "2024-2025").</param>
    /// <returns>Un tuple contenant la date de début et la date de fin de l'année scolaire.</returns>
    /// <exception cref="ArgumentException">L'année scolaire n'est pas dans un format valide.</exception>
    public static (DateTime debut, DateTime fin) GetBeachSchoolYear(string annee)
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