using System;
using System.Collections.Generic;
using Wisej.Web;

public partial class DataPage : UserControl
{
    public DataPage()
    {
        InitializeComponent();
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

    private void ChargerDonnees()
    {
        int userId = 1;

        var events = new EventDAO().GetByUserId(userId);
        var listeAffichage = new List<dynamic>();

        // Correspondance propriété => label lisible
        var motivationLabels = new Dictionary<string, string>()
{
    { "addictive_behaviors", "Conduites addictives" },
    { "critical_incident", "Incident critique" },
    { "student_conflict", "Conflit entre élèves" },
    { "incivility_violence", "Incivilités / Violences" },
    { "grief", "Deuil" },
    { "unhappiness", "Mal-être" },
    { "learning_difficulties", "Difficultés d'apprentissage" },
    { "career_guidance_issues", "Orientation professionnelle" },
    { "family_difficulties", "Difficultés familiales" },
    { "stress", "Stress" },
    { "financial_difficulties", "Difficultés financières" },
    { "suspected_abuse", "Suspicion de maltraitance" },
    { "discrimination", "Discrimination" },
    { "difficulties_tensions_with_a_teacher", "Tensions avec un·e enseignant·e" },
    { "harassment_intimidation", "Harcèlement / Intimidation" },
    { "gender_sexual_orientation", "Genre / orientation" },
    { "other", "Autre" }
};


        foreach (var evt in events)
        {
            int duree = 0;
            string motivations = "";

            if (evt.InterviewId.HasValue)
            {
                var interview = new InterviewDAO().GetById(evt.InterviewId.Value);
                duree = interview?.Time ?? 0;

                var motifs = new List<string>();

                foreach (var prop in typeof(Interview).GetProperties())
                {
                    if (prop.PropertyType == typeof(bool)
                        && motivationLabels.ContainsKey(prop.Name)
                        && (bool)prop.GetValue(interview) == true)
                    {
                        motifs.Add(motivationLabels[prop.Name]);
                    }
                }

                motivations = string.Join(", ", motifs);
            }

            listeAffichage.Add(new
            {
                Date = evt.Date.ToString("dd/MM/yyyy"),
                Sujet = evt.Sujet,
                Personnes = evt.Personne,
                Duree = duree,
                TempsAdmin = evt.TempsAdmin,
                Type = evt.InterviewId.HasValue ? "Entretien" : "Séance",
                Motivations = motivations
            });
        }

        dataGrid.Columns.Clear();
        dataGrid.DataSource = listeAffichage;

        var editButtonColumn = new DataGridViewButtonColumn()
        {
            Name = "Edit",
            HeaderText = "Modifier",
            Text = "✏️",
            UseColumnTextForButtonValue = true,
            DisplayIndex = 7,
            Width = 50
        };

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
            DisplayIndex = 8,
            UseColumnTextForButtonValue = true,
            Width = 50
        };

        dataGrid.Columns.Add(editButtonColumn);
        dataGrid.Columns.Add(btnSupprimer);
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
            if (MessageBox.Show("Voulez-vous vraiment supprimer cette entrée ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            var ligne = dataGrid.Rows[e.RowIndex];
            string sujet = ligne.Cells["Sujet"]?.Value?.ToString();
            string date = ligne.Cells["Date"]?.Value?.ToString();

            AlertBox.Show($"Suppression de l'entrée : {sujet} ({date})", MessageBoxIcon.Information);
        }
    }
}
