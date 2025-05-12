///////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                       //
//      Date de création: 29.04.2025                                 //
//      Description: UserControl qui affiche la page de statistiques //
//      Date de dernière révision: 12.05.2025                        //
///////////////////////////////////////////////////////////////////////

using Wisej.Web;
using Wisej.Web.Ext.ChartJS;
using System.Drawing;
using System.Collections.Generic;
using System;

/// <summary>
/// Représente une page affichant des statistiques sous forme de graphiques.
/// </summary>
public partial class StatsPage : UserControl
{
    private ComboBox dropdown;
    private FlowLayoutPanel panelCharts;

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="StatsPage"/>.
    /// </summary>
    public StatsPage()
    {
        InitializeComponent();
        CreateAllCharts();
    }

    /// <summary>
    /// Crée et ajoute tous les graphiques à la page.
    /// </summary>
    private void CreateAllCharts()
    {
        // Panel du bouton export en haut
        var panelBouton = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 50,
            Padding = new Padding(10),
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true
        };

        var btnExport = new Button
        {
            Text = "Export en PDF",
            Width = 140,
            Height = 50,
            BackColor = Color.FromArgb(0, 122, 204),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Dock = DockStyle.Left,
        };
        btnExport.Click += btnExport_Click;
        panelBouton.Controls.Add(btnExport);

        dropdown = new ComboBox
        {
            Width = 140,
            Height = 50,
            BackColor = Color.FromArgb(0, 122, 204),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Dock = DockStyle.Left,
            Margin = new Padding(10, 0, 0, 0),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        panelBouton.Controls.Add(dropdown);

        // Conteneur scrollable horizontal pour les charts
        panelCharts = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            AutoScroll = true,
            Padding = new Padding(20),
        };

        LoadSchoolYear();

        dropdown.SelectedIndexChanged += dropdownSelection_Changed; // après le load des années pour éviter le double ajout des charts.

        string anneeSelectionnee = dropdown.SelectedItem.ToString();

        if(string.IsNullOrEmpty(anneeSelectionnee))
        {
            MessageBox.Show("Aucune année scolaire disponible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        panelCharts.Controls.Add(CreateChart_Repartition(anneeSelectionnee));
        panelCharts.Controls.Add(CreateChart_Contexte(anneeSelectionnee));
        panelCharts.Controls.Add(CreateChart_Interventions(anneeSelectionnee));

        // Layout principal (vertical)
        var layoutPrincipal = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2
        };
        layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // Pour le bouton
        layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100));   // Pour les charts
        layoutPrincipal.Controls.Add(panelBouton, 0, 0);
        layoutPrincipal.Controls.Add(panelCharts, 0, 1);

        this.Controls.Add(layoutPrincipal);
    }

    private void LoadSchoolYear()
    {
        // Récupérer les années disponibles depuis EventDAO
        var eventDAO = new EventDAO();
        int userId = (int)Application.Session["userId"];
        var annees = eventDAO.GetAvailableYears(userId);

        // Ajouter les années au dropdown
        dropdown.Items.Clear();
        foreach (var annee in annees)
        {
            AlertBox.Show($"Année trouvée : {annee}"); // Debug
            dropdown.Items.Add($"{annee}");
        }

        if (dropdown.Items.Count <= 0)
        {
            MessageBox.Show("Aucune année scolaire disponible.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Sélectionner la première année par défaut
        if (dropdown.Items.Count > 0)
        {
            dropdown.SelectedIndex = dropdown.Items.Count - 1; // Sélectionner la première années
        }
    }

    /// <summary>
    /// Crée un graphique en camembert représentant la répartition du travail.
    /// </summary>
    /// <returns>Un objet <see cref="ChartJS"/> représentant le graphique.</returns>
    private ChartJS CreateChart_Repartition(string anneeScolaire)
    {
        int userId = (int)Application.Session["userId"];
        var (start, end) = DateHelper.GetBeachSchoolYear(anneeScolaire);
        var data = new SeanceDAO().GetTotalDurationByType(userId, start, end);


        var labels = new List<string>();
        var values = new List<object>();

        foreach (var entry in data)
        {
            labels.Add(entry.Key);
            values.Add(entry.Value);
        }

        var chart = new ChartJS
        {
            Width = 450,
            Text = "Répartition du travail",
            Height = 600,
            Padding = new Padding(10),
            ChartType = ChartType.Pie,
            BackColor = Color.LightBlue,
            Labels = labels.ToArray()
        };

        var dataSet = new PieDataSet
        {
            Label = "Répartition du travail",
            Data = values.ToArray(),
            BackgroundColor = ChartColorHelper.GenerateColors(labels.Count)
        };

        chart.DataSets.Add(dataSet);
        return chart;
    }

    /// <summary>
    /// Crée un graphique en camembert représentant le contexte des entretiens.
    /// </summary>
    /// <returns>Un objet <see cref="ChartJS"/> représentant le graphique.</returns>
    private ChartJS CreateChart_Contexte(string anneeScolaire)
    {
        int userId = (int)Application.Session["userId"];
        var (start, end) = DateHelper.GetBeachSchoolYear(anneeScolaire);
        var data = new InterviewDAO().GetDurationByType(userId, start, end);

        var labels = new List<string>();
        var values = new List<object>();

        foreach (var entry in data)
        {
            labels.Add(entry.Key);
            values.Add(entry.Value);
        }

        var chart = new ChartJS
        {
            Width = 450,
            Height = 600,
            Text = "Contexte",
            Padding = new Padding(10),
            ChartType = ChartType.Pie,
            BackColor = Color.LightBlue,
            Labels = labels.ToArray()
        };

        var dataSet = new PieDataSet
        {
            Label = "Contexte",
            Data = values.ToArray(),
            BackgroundColor = ChartColorHelper.GenerateColors(labels.Count)
        };

        chart.DataSets.Add(dataSet);
        return chart;
    }

    /// <summary>
    /// Crée un graphique en camembert représentant les types d'interventions.
    /// </summary>
    /// <returns>Un objet <see cref="ChartJS"/> représentant le graphique.</returns>
    private ChartJS CreateChart_Interventions(string anneeScolaire)
    {
        int userId = (int)Application.Session["userId"];
        var (start, end) = DateHelper.GetBeachSchoolYear(anneeScolaire);
        var data = new InterviewDAO().GetStatsMotivations(userId, start, end);

        var labels = new List<string>();
        var values = new List<object>();

        foreach (var entry in data)
        {
            if (entry.Value > 0 && _propertyToLabel.TryGetValue(entry.Key, out var label))
            {
                labels.Add(label); // Utiliser le label traduit
                values.Add(entry.Value);
            }
        }

        var chart = new ChartJS
        {
            Width = 450,
            Height = 600,
            Text = "Types d'interventions",
            ChartType = ChartType.Pie,
            BackColor = Color.LightBlue,
            Labels = labels.ToArray()
        };

        var dataSet = new PieDataSet
        {
            Label = "Types d'interventions",
            Data = values.ToArray(),
            BackgroundColor = ChartColorHelper.GenerateColors(labels.Count)
        };

        chart.DataSets.Add(dataSet);
        return chart;
    }

    /// <summary>
    /// Fournit des méthodes utilitaires pour générer des couleurs pour les graphiques.
    /// </summary>
    public static class ChartColorHelper
    {
        /// <summary>
        /// Génère une palette de couleurs pour un nombre donné d'éléments.
        /// </summary>
        /// <param name="count">Le nombre de couleurs à générer.</param>
        /// <returns>Un tableau de couleurs.</returns>
        public static Color[] GenerateColors(int count)
        {
            var palette = new Color[]
            {
                Color.FromArgb(62, 149, 205), Color.FromArgb(255, 159, 64), Color.FromArgb(176, 58, 72),
                Color.FromArgb(169, 209, 142), Color.FromArgb(153, 102, 255), Color.FromArgb(255, 205, 86),
                Color.FromArgb(75, 192, 192), Color.FromArgb(201, 203, 207), Color.FromArgb(54, 162, 235),
                Color.FromArgb(255, 99, 132), Color.FromArgb(128, 100, 162), Color.FromArgb(47, 85, 151)
            };

            var result = new List<Color>();
            for (int i = 0; i < count; i++)
            {
                result.Add(palette[i % palette.Length]);
            }
            return result.ToArray();
        }
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton d'exportation en PDF.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void btnExport_Click(object sender, EventArgs e)
    {
        AlertBox.Show("Export du document en PDF à venir.");
    }

    private void dropdownSelection_Changed(object sender, EventArgs e)
    {
        if (dropdown.SelectedItem == null)
            return;

        string anneeSelectionnee = dropdown.SelectedItem.ToString();
        int userId = (int)Application.Session["userId"];

        // Trouve le FlowLayoutPanel contenant les charts
        if (panelCharts == null)
            return;

        panelCharts.Controls.Clear(); // Supprimer les anciens graphiques

        // --- Crée et ajoute les nouveaux charts filtrés par année ---
        panelCharts.Controls.Add(CreateChart_Repartition(anneeSelectionnee));
        panelCharts.Controls.Add(CreateChart_Contexte(anneeSelectionnee));
        panelCharts.Controls.Add(CreateChart_Interventions(anneeSelectionnee));
    }


    /// <summary>
    /// Dictionnaire associant les propriétés des motivations aux libellés lisibles.
    /// </summary>
    private readonly Dictionary<string, string> _propertyToLabel = new Dictionary<string, string>()
    {
        { "addictive_behaviors", "Conduites addictives" },
        { "critical_incident", "Incident critique" },
        { "student_conflict", "Conflit entre élèves" },
        { "incivility_violence", "Incivilités / Violences" },
        { "grief", "Deuil" },
        { "unhappiness", "Mal-être" },
        { "learning_difficulties", "Difficultés Apprentissage" },
        { "career_guidance_issues", "Question orientation professionnelle" },
        { "family_difficulties", "Difficultés familiales" },
        { "stress", "Stress" },
        { "financial_difficulties", "Difficultés financières" },
        { "suspected_abuse", "Suspicion maltraitance" },
        { "discrimination", "Discrimination" },
        { "difficulties_tensions_with_a_teacher", "Difficutés / tensions avec un∙e enseignant∙e" },
        { "harassment_intimidation", "Harcèlement / Intimidation" },
        { "gender_sexual_orientation", "Genre - orientation sexuelle et affective" },
        { "other", "Autre" }
    };
}