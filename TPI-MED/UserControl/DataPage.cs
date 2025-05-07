using System;
using System.Collections.Generic;
using Wisej.Web;

/// <summary>
/// Représente une page pour afficher, ajouter, modifier et supprimer des données d'événements.
/// </summary>
public partial class DataPage : UserControl
{
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="DataPage"/>.
    /// </summary>
    public DataPage()
    {
        InitializeComponent();
        ChargerDonnees();
    }

    /// <summary>
    /// Charge les données des événements et les affiche dans la grille.
    /// </summary>
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
            else
            {
                var seances = new SeanceDAO().GetByEventId(evt.Id);
                duree = 0;
                foreach (var s in seances)
                    duree += s.Temps;
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

        // Configuration des colonnes
        dataGrid.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["Date"].Width = 75;
        dataGrid.Columns["Date"].DisplayIndex = 0;

        dataGrid.Columns["TempsAdmin"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["TempsAdmin"].Width = 100;
        dataGrid.Columns["TempsAdmin"].DisplayIndex = 1;
        dataGrid.Columns["TempsAdmin"].HeaderText = "Temps Admin";

        dataGrid.Columns["Duree"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["Duree"].Width = 60;
        dataGrid.Columns["Duree"].DisplayIndex = 2;

        dataGrid.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGrid.Columns["Type"].Width = 120;
        dataGrid.Columns["Type"].DisplayIndex = 3;

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

        dataGrid.Columns["Sujet"].MinimumWidth = 120;
        dataGrid.Columns["Personnes"].MinimumWidth = 120;
        dataGrid.Columns["Motivations"].MinimumWidth = 250;

        dataGrid.Columns["Duree"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGrid.Columns["TempsAdmin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGrid.Columns["Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton "Ajouter".
    /// Ouvre un formulaire pour ajouter une nouvelle entrée.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void btnAjouter_Click(object sender, EventArgs e)
    {
        var form = new NewEntryForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            // Recharger les données après l'ajout
            ChargerDonnees();
        }
    }

    /// <summary>
    /// Gère les clics sur les colonnes "Modifier" et "Supprimer" de la grille.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (blocageEvenement || e.ColumnIndex < 0 || e.RowIndex < 0)
            return;

        blocageEvenement = true;

        try
        {
            if (dataGrid.Columns[e.ColumnIndex].Name == "Edit")
            {
                var ligne = dataGrid.Rows[e.RowIndex];
                if (ligne.DataBoundItem is EventAffichage item)
                {
                    var evt = new EventDAO().GetById(item.Id);
                    if (evt != null)
                    {
                        Interview interview = null;
                        List<Seance> seances = null;

                        if (evt.InterviewId.HasValue)
                            interview = new InterviewDAO().GetById(evt.InterviewId.Value);
                        else
                            seances = new SeanceDAO().GetByEventId(evt.Id);

                        var form = new NewEntryForm(evt, interview, seances);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            ChargerDonnees();
                        }
                    }
                }
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

                        if (new EventDAO().SupprimerEvenement(id))
                        {
                            dataGrid.CellClick -= dataGrid_CellContentClick;
                            ChargerDonnees();
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
            blocageEvenement = false;
        }
    }

    /// <summary>
    /// Indique si l'événement de clic est temporairement bloqué pour éviter les appels multiples.
    /// </summary>
    private bool blocageEvenement = false;
}