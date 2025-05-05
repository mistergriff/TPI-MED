using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Wisej.Web;

public partial class DataPage : UserControl
{
    public DataPage()
    {
        InitializeComponent();
        ChargerDonnees();
    }

    private void ChargerDonnees()
    {
        // Désactiver temporairement l'événement CellClick pour éviter les appels récursifs
        dataGrid.CellClick -= dataGrid_CellContentClick;

        int userId = (int)Application.Session["userId"];

        var events = new EventDAO().GetByUserId(userId);
        var listeAffichage = new List<EventAffichage>();

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

            string typeAffichage;
            string typeEntretien = "";

            if (evt.InterviewId.HasValue)
            {
                var interview = new InterviewDAO().GetById(evt.InterviewId.Value);

                switch (interview.InterviewTypeId)
                {
                    case 1:
                        typeEntretien = "Seul";
                        break;
                    case 2:
                        typeEntretien = "Groupe";
                        break;
                    case 3:
                        typeEntretien = "Classe";
                        break;
                    default:
                        typeEntretien = "Inconnu";
                        break;
                }

                typeAffichage = $"Entretien ({typeEntretien})";
            }
            else
            {
                typeAffichage = "Séance";
            }


            listeAffichage.Add(new EventAffichage()
            {
                Id = evt.Id,
                Date = evt.Date.ToString("dd/MM/yyyy"),
                Sujet = evt.Sujet,
                Personnes = evt.Personne,
                Duree = duree,
                TempsAdmin = evt.TempsAdmin,
                Type = typeAffichage,
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
            DisplayIndex = 8
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
            DisplayIndex = 9,
            UseColumnTextForButtonValue = true
        };

        dataGrid.Columns.Add(editButtonColumn);
        dataGrid.Columns.Add(btnSupprimer);
        this.dataGrid.CellClick += dataGrid_CellContentClick;

        if (dataGrid.Columns.Contains("Id"))
        {
            dataGrid.Columns["Id"].Visible = false;
        }

        // Fixer la colonne "Date"
        dataGrid.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["Date"].Width = 75;
        dataGrid.Columns["Date"].DisplayIndex = 0;

        // Fixer la colonne "TempsAdmin"
        dataGrid.Columns["TempsAdmin"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["TempsAdmin"].Width = 100;
        dataGrid.Columns["TempsAdmin"].DisplayIndex = 1;
        dataGrid.Columns["TempsAdmin"].HeaderText = "Temps Admin";

        // Fixer la colonne "Duree"
        dataGrid.Columns["Duree"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["Duree"].Width = 60;
        dataGrid.Columns["Duree"].DisplayIndex = 2;

        // Fixer la colonne "Type"
        dataGrid.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["Type"].Width = 120;
        dataGrid.Columns["Type"].DisplayIndex = 3;

        // Étendre automatiquement les colonnes longues
        dataGrid.Columns["Sujet"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGrid.Columns["Sujet"].Width = 150;
        dataGrid.Columns["Sujet"].DisplayIndex = 4;
        dataGrid.Columns["Personnes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGrid.Columns["Personnes"].Width = 150;
        dataGrid.Columns["Personnes"].DisplayIndex = 5;
        dataGrid.Columns["Motivations"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGrid.Columns["Motivations"].Width = 250;
        dataGrid.Columns["Motivations"].DisplayIndex = 6;
        dataGrid.Columns["Edit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["Edit"].Width = 50;
        dataGrid.Columns["Edit"].HeaderText = "";
        dataGrid.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["Delete"].Width = 50;
        dataGrid.Columns["Delete"].HeaderText = "";

        // Définir un minimum pour éviter l’écrasement si la fenêtre est trop étroite
        dataGrid.Columns["Sujet"].MinimumWidth = 120;
        dataGrid.Columns["Personnes"].MinimumWidth = 120;
        dataGrid.Columns["Motivations"].MinimumWidth = 250;


        // Centrer les colonnes numériques
        dataGrid.Columns["Duree"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGrid.Columns["TempsAdmin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGrid.Columns["Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

    }



    private void btnAjouter_Click(object sender, EventArgs e)
    {
        var form = new NewEntryForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            // Recharger les données après l'ajout
            ChargerDonnees();
        }
    }

    private bool blocageEvenement = false;

    private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        // Ignore les clics sur la colonne de sélection ou si le blocage est actif
        if (blocageEvenement || e.ColumnIndex < 0 || e.RowIndex < 0)
            return;

        // Activer le blocage pour éviter les appels multiples
        blocageEvenement = true;

        try
        {
            if (dataGrid.Columns[e.ColumnIndex].Name == "Edit")
            {
                var ligne = dataGrid.Rows[e.RowIndex];
                string sujet = ligne.Cells["Sujet"]?.Value?.ToString();
                string date = ligne.Cells["Date"]?.Value?.ToString();

                AlertBox.Show($"Édition de l'entrée : {sujet} ({date})", MessageBoxIcon.Information);
            }
            else if (dataGrid.Columns[e.ColumnIndex].Name == "Delete")
            {
                var result = MessageBox.Show("Voulez-vous vraiment supprimer cette entrée ?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    var ligne = dataGrid.Rows[e.RowIndex];
                    if (ligne.DataBoundItem is EventAffichage item)
                    {
                        int id = item.Id;

                        // Stocker l'ID avant de recharger la grille
                        if (new EventDAO().SupprimerEvenement(id))
                        {
                            // Désactiver temporairement le gestionnaire pendant la mise à jour
                            dataGrid.CellClick -= dataGrid_CellContentClick;

                            // Recharger les données
                            ChargerDonnees();

                            // Afficher le message de succès après le rechargement
                            AlertBox.Show("Événement supprimé avec succès.", MessageBoxIcon.Information);
                        }
                        else
                        {
                            AlertBox.Show("Erreur lors de la suppression.", MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        finally
        {
            // Réinitialiser le blocage d'événement à la fin
            blocageEvenement = false;
        }
    }
}
