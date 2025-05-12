//////////////////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                                  //
//      Date de création: 29.04.2025                                            //
//      Description: Designer du UserControl qui affiche la page des données    //
//      Date de dernière révision: 12.05.2025                                   //
//////////////////////////////////////////////////////////////////////////////////

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
            ScrollBars = ScrollBars.Vertical,
        };

        // Ajout au panel top
        this.panelTop.Controls.Add(this.btnAjouter);

        // Ajout aux contrôles de la page
        this.Controls.Add(this.dataGrid);
        this.Controls.Add(this.panelTop);

        this.Text = "Entrées";
        this.Size = new System.Drawing.Size(800, 500);
    }
}
