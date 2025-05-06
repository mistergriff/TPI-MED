using Wisej.Web;
using Wisej.Web.Ext.ChartJS;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Drawing.Printing;

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
            Location = new Point(10, 40),
            FlowDirection = FlowDirection.LeftToRight,
            AutoScroll = true,
            Padding = new Padding(20)
        };

        var btnExport = new Button
        {
            Text = "Export en PDF",
            Width = 100,
            Height = 20,
            Location = new Point(10, 10),
            BackColor = Color.FromArgb(0, 255, 0),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter
        };
        btnExport.Click += btnExport_Click;

        layout.Controls.Add(CreateChart_Repartition());
        layout.Controls.Add(CreateChart_Contexte());
        layout.Controls.Add(CreateChart_Interventions());

        this.Controls.Add(layout);
    }

    private ChartJS CreateChart_Repartition()
    {
        int userId = (int)Application.Session["userId"];
        var data = new SeanceDAO().GetDureeTotaleParType(userId);

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

    private ChartJS CreateChart_Contexte()
    {
        int userId = (int)Application.Session["userId"];
        var data = new InterviewDAO().GetDureeParType(userId);

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

    private ChartJS CreateChart_Interventions()
    {
        int userId = (int)Application.Session["userId"];
        var data = new InterviewDAO().GetStatsMotivations(userId);

        var labels = new List<string>();
        var values = new List<object>();

        foreach (var entry in data)
        {
            if (entry.Value > 0) // Filtrer les données inutiles
            {
                labels.Add(entry.Key);
                values.Add(entry.Value);
            }
        }

        var chart = new ChartJS
        {
            Width = 450,
            Height = 600,
            Padding = new Padding(10),
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


    public static class ChartColorHelper
    {
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

    private void btnExport_Click(object sender, EventArgs e)
    {
        AlertBox.Show("Export du document en PDF à venir.");
    }
}
