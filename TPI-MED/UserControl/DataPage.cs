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
                new { Date = "01/05/2025", Sujet = "Entretien élèves", Personnes = "2", Duree = 45, TempsAdmin = 10, Motivation = "test" },
                new { Date = "02/05/2025", Sujet = "Séance équipe", Personnes = "5", Duree = 60, TempsAdmin = 15, Motivation = "test" },
                new { Date = "03/05/2025", Sujet = "Projet MPP", Personnes = "3", Duree = 90, TempsAdmin = 20, Motivation = "test" }
            };

        dataGrid.DataSource = donnees;

        // Ajouter une colonne "Editer"
        var editButtonColumn = new DataGridViewButtonColumn()
        {
            Name = "Edit",
            HeaderText = "Modifier",
            Text = "✏️",
            UseColumnTextForButtonValue = true,
            Width = 50
        };

        // Bouton supprimer
        var btnSupprimer = new DataGridViewButtonColumn()
        {
            Name = "Delete",
            HeaderText = "Supprimer",
            Text = "🗑️",
            DefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = System.Drawing.Color.FromArgb(255, 0, 0),
                ForeColor = System.Drawing.Color.White
            },
            UseColumnTextForButtonValue = true,
            Width = 50
        };

        this.dataGrid.Columns.Add(editButtonColumn);
        this.dataGrid.Columns.Add(btnSupprimer);
        this.dataGrid.CellClick += new DataGridViewCellEventHandler(this.dataGrid_CellContentClick);
    }

    private void btnAjouter_Click(object sender, EventArgs e)
    {
        // Afficher une alerte. Plus tard, ouvrir un formulaire
        //AlertBox.Show("Ouverture de la page d'ajout d'entrée...", MessageBoxIcon.Information);
        var form = new NewEntryForm();
        form.ShowDialog();
    }

    private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        // Ignore les clics sur la colonne de sélection
        if (e.ColumnIndex < 0 || e.RowIndex < 0)
            return;

        if (dataGrid.Columns[e.ColumnIndex].Name == "Edit")
        {
            var ligne = dataGrid.Rows[e.RowIndex];
            string sujet = ligne.Cells["Sujet"]?.Value?.ToString();
            string date = ligne.Cells["Date"]?.Value?.ToString();

            AlertBox.Show($"Édition de l'entrée : {sujet} ({date})", MessageBoxIcon.Information);
        }

        else if (dataGrid.Columns[e.ColumnIndex].Name == "Delete")
        {
            if(MessageBox.Show("Voulez-vous vraiment supprimer cette entrée ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            var ligne = dataGrid.Rows[e.RowIndex];
            string sujet = ligne.Cells["Sujet"]?.Value?.ToString();
            string date = ligne.Cells["Date"]?.Value?.ToString();

            AlertBox.Show($"Suppression de l'entrée : {sujet} ({date})", MessageBoxIcon.Information);
        }
    }
}
