using Wisej.Web;
using Wisej.Web.Ext.ChartJS;
using System.Drawing;

public partial class StatsPage : UserControl
{
    public StatsPage()
    {
        InitializeComponent();
        CreateAllCharts();
    }

    private void CreateAllCharts()
    {
        // Crée un conteneur horizontal pour les 3 graphiques
        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            AutoScroll = true,
            Padding = new Padding(20)
        };

        layout.Controls.Add(CreateChart_Repartition());
        layout.Controls.Add(CreateChart_Contexte());
        layout.Controls.Add(CreateChart_Interventions());

        this.Controls.Add(layout);
    }

    private ChartJS CreateChart_Repartition()
    {
        var chart = new ChartJS
        {
            Width = 450,
            Height = 600,
            ChartType = ChartType.Pie,
            BackColor = Color.LightBlue,
            Labels = new[]
            {
            "Temps dévolu autour de la situation / séance",
            "Direction",
            "Groupe MPP/Pikas",
            "Enseignant-e-s",
            "Réseau avec parents",
            "Equipe PSPS",
            "Equipe pluridis. Réseau",
            "Projets",
            "Autre"
        }
        };

        chart.

        var dataSet = new PieDataSet
        {
            Label = "Répartition du travail",
            Data = new object[] { 20, 15, 10, 12, 8, 10, 5, 10, 10 },
            BackgroundColor = new[]
            {
            Color.FromArgb(62, 149, 205),  // Bleu
            Color.FromArgb(255, 159, 64),  // Orange
            Color.FromArgb(176, 58, 72),   // Rouge
            Color.FromArgb(169, 209, 142), // Vert clair
            Color.FromArgb(153, 102, 255), // Violet clair
            Color.FromArgb(255, 205, 86),  // Jaune
            Color.FromArgb(75, 192, 192),  // Vert turquoise
            Color.FromArgb(201, 203, 207), // Gris clair
            Color.FromArgb(54, 162, 235)   // Bleu clair
        }
        };

        chart.DataSets.Add(dataSet);
        return chart;
    }


    private ChartJS CreateChart_Contexte()
    {
        var chart = new ChartJS
        {
            Width = 450,
            Height = 600,
            ChartType = ChartType.Pie,
            BackColor = Color.LightBlue,
            Labels = new[] { "Seul (min)", "Groupe (min)", "Classe (min)" }
        };

        var dataSet = new PieDataSet
        {
            Label = "Contexte",
            Data = new object[] { 56, 18, 26 },
            BackgroundColor = new[]
            {
                    Color.FromArgb(62, 149, 205),  // Bleu
                    Color.FromArgb(176, 58, 72),   // Rouge
                    Color.FromArgb(169, 209, 142)  // Vert clair
                }
        };

        chart.DataSets.Add(dataSet);
        return chart;
    }

    private ChartJS CreateChart_Interventions()
    {
        var chart = new ChartJS
        {
            Width = 450,
            Height = 600,
            ChartType = ChartType.Pie,
            BackColor = Color.LightBlue,
            Labels = new[]
            {
            "Conduites addictives",
            "Incident critique",
            "Conflit entre élèves",
            "Incivilités / Violences",
            "Deuil",
            "Mal-être",
            "Difficultés Apprentissage",
            "Question orientation professionnelle",
            "Difficultés familiales",
            "Stress",
            "Difficultés financières",
            "Suspicion maltraitance",
            "Discrimination",
            "Difficultés / tensions avec un∙e enseignant∙e",
            "Harcèlement / Intimidation",
            "Genre - orientation sexuelle et affective",
            "Autre"
        }
        };

        var dataSet = new PieDataSet
        {
            Label = "Types d'interventions",
            Data = new object[]
            {
            10, 5, 8, 12, 3, 15, 7, 4, 6, 20, 2, 1, 3, 5, 9, 4, 6
            },
            BackgroundColor = new[]
            {
            Color.FromArgb(128, 100, 162), // Violet
            Color.FromArgb(47, 85, 151),   // Bleu foncé
            Color.FromArgb(169, 209, 142), // Vert clair
            Color.FromArgb(255, 159, 64),  // Orange
            Color.FromArgb(176, 58, 72),   // Rouge
            Color.FromArgb(62, 149, 205),  // Bleu
            Color.FromArgb(255, 205, 86),  // Jaune
            Color.FromArgb(153, 102, 255), // Violet clair
            Color.FromArgb(201, 203, 207), // Gris clair
            Color.FromArgb(54, 162, 235),  // Bleu clair
            Color.FromArgb(255, 99, 132),  // Rouge clair
            Color.FromArgb(75, 192, 192),  // Vert turquoise
            Color.FromArgb(255, 206, 86),  // Jaune clair
            Color.FromArgb(153, 102, 255), // Violet clair
            Color.FromArgb(201, 203, 207), // Gris clair
            Color.FromArgb(54, 162, 235),  // Bleu clair
            Color.FromArgb(255, 99, 132)   // Rouge clair
        }
        };

        chart.DataSets.Add(dataSet);
        return chart;
    }
}