using System;
using System.Collections.Generic;
using Wisej.Web;

public partial class DataPage : UserControl
{
    public DataPage()
    {
        InitializeComponent();
        ChargerDonneesFactices();
    }

    private void ChargerDonneesFactices()
    {
        var donnees = new List<dynamic>
            {
                new { Date = "01/05/2025", Sujet = "Entretien élèves", Personnes = "2", Duree = 45, TempsAdmin = 10 },
                new { Date = "02/05/2025", Sujet = "Séance équipe", Personnes = "5", Duree = 60, TempsAdmin = 15 },
                new { Date = "03/05/2025", Sujet = "Projet MPP", Personnes = "3", Duree = 90, TempsAdmin = 20 }
            };

        dataGrid.DataSource = donnees;

        // Ajouter une colonne "Editer"
        var editButtonColumn = new DataGridViewButtonColumn()
        {
            Name = "Edit",
            HeaderText = "Edition",
            Text = "✏️ Modifier",
            UseColumnTextForButtonValue = true,
            Width = 100
        };

        this.dataGrid.Columns.Add(editButtonColumn);
        this.dataGrid.CellClick += new DataGridViewCellEventHandler(this.dataGrid_CellContentClick);
    }

    private void btnAjouter_Click(object sender, EventArgs e)
    {
        // Afficher une alerte. Plus tard, ouvrir un formulaire
        AlertBox.Show("Ouverture de la page d'ajout d'entrée...", MessageBoxIcon.Information);
    }

    private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        // Ignore les clics sur la colonne de sélection
        if (e.ColumnIndex < 0 || e.RowIndex < 0)
            return;

        if (dataGrid.Columns[e.ColumnIndex].Name == "ColModifier")
        {
            var ligne = dataGrid.Rows[e.RowIndex];
            string sujet = ligne.Cells["Sujet"]?.Value?.ToString();
            string date = ligne.Cells["Date"]?.Value?.ToString();

            AlertBox.Show($"Édition de l'entrée : {sujet} ({date})", MessageBoxIcon.Information);
        }
    }
}
