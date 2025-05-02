using System;
using Wisej.Web;


partial class DataPage
{
    private DataGridView dataGrid;
    private Button btnAjouter;
    private FlowLayoutPanel panelTop;

    private void InitializeComponent()
    {
        // Panel pour les boutons
        this.panelTop = new FlowLayoutPanel()
        {
            Dock = DockStyle.Top,
            Height = 60,
            Padding = new Padding(10),
            FlowDirection = FlowDirection.LeftToRight,
            BackColor = System.Drawing.Color.WhiteSmoke
        };

        // Bouton Ajouter
        this.btnAjouter = new Button()
        {
            Text = "➕   Ajouter une entrée",
            Height = 40,
            Width = 200,
            BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };
        this.btnAjouter.Click += new EventHandler(this.btnAjouter_Click);

        // DataGrid
        this.dataGrid = new DataGridView()
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            AutoGenerateColumns = true,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
        };
        ChargerDonnees();

        //// Fixer la colonne "Date"
        //dataGrid.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //dataGrid.Columns["Date"].Width = 100;
        //dataGrid.Columns["Date"].DisplayIndex = 0;

        //// Fixer la colonne "TempsAdmin"
        //dataGrid.Columns["TempsAdmin"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //dataGrid.Columns["TempsAdmin"].Width = 80;
        //dataGrid.Columns["TempsAdmin"].DisplayIndex = 1;

        //// Fixer la colonne "Duree"
        //dataGrid.Columns["Duree"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //dataGrid.Columns["Duree"].Width = 80;
        //dataGrid.Columns["Duree"].DisplayIndex = 2;

        //// Fixer la colonne "Type"
        //dataGrid.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //dataGrid.Columns["Type"].Width = 90;
        //dataGrid.Columns["Type"].DisplayIndex = 3;

        //// Étendre automatiquement les colonnes longues
        //dataGrid.Columns["Sujet"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //dataGrid.Columns["Sujet"].DisplayIndex = 4;
        //dataGrid.Columns["Personnes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //dataGrid.Columns["Personnes"].DisplayIndex = 5;
        //dataGrid.Columns["Motivations"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //dataGrid.Columns["Motivations"].DisplayIndex = 6;

        //// Définir un minimum pour éviter l’écrasement si la fenêtre est trop étroite
        //dataGrid.Columns["Sujet"].MinimumWidth = 120;
        //dataGrid.Columns["Personnes"].MinimumWidth = 120;
        //dataGrid.Columns["Motivations"].MinimumWidth = 200;


        //// Centrer les colonnes numériques
        //dataGrid.Columns["Duree"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //dataGrid.Columns["TempsAdmin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //dataGrid.Columns["Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        // Ajout au panel top
        this.panelTop.Controls.Add(this.btnAjouter);

        // Ajout aux contrôles de la page
        this.Controls.Add(this.dataGrid);
        this.Controls.Add(this.panelTop);

        this.Text = "Entrées";
        this.Size = new System.Drawing.Size(800, 500);
    }
}
