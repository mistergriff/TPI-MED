using System;
using System.Drawing;
using Wisej.Web;


partial class NewEntryForm
{
    private DateTimePicker datePicker;
    private TextBox txtSujet;
    private TextBox txtPersonnes;
    private NumericUpDown numDuree;
    private TabControl tabType;
    private TabPage tabSeance;
    private TabPage tabEntretien;
    private RadioButton radioSeul;
    private RadioButton radioGroupe;
    private RadioButton radioClasse;
    private Button btnAjouter;

    private void InitializeComponent()
    {
        this.datePicker = new DateTimePicker()
        {
            Location = new System.Drawing.Point(30, 30),
            Width = 250
        };

        this.txtSujet = new TextBox()
        {
            Location = new System.Drawing.Point(30, 70),
            Watermark = "Sujet",
            Width = 250
        };

        this.txtPersonnes = new TextBox()
        {
            Location = new System.Drawing.Point(30, 110),
            Watermark = "Personnes concernées",
            Width = 250
        };

        this.numDuree = new NumericUpDown()
        {
            Location = new System.Drawing.Point(30, 150),
            LabelText = "Durée (minutes)",
            Width = 100,
            Minimum = 0,
            Maximum = 300,
            Value = 0
        };

        // Créer le contrôle TabControl et les onglets
        this.tabType = new TabControl()
        {
            Location = new System.Drawing.Point(20, 220),
            Size = new System.Drawing.Size(420, 380)
        };

        this.tabSeance = new TabPage()
        {
            Text = "Séance"
        };

        this.tabEntretien = new TabPage()
        {
            Text = "Entretien"
        };

        // Ajouter des contrôles spécifiques à chaque onglet

        // Onglet Séance

        var lblInfoSeance = new Label()
        {
            Text = "Insérez le temps dans chaques champs correspondant (en minutes)",
            Location = new System.Drawing.Point(20, 20),
            AutoSize = true
        };
        this.tabSeance.Controls.Add(lblInfoSeance);

        var txtDirection = new TextBox()
        {
            Location = new System.Drawing.Point(20, 50),
            LabelText = "Direction",
            Width = 150
        };
        this.tabSeance.Controls.Add(txtDirection);

        var txtEnseignant = new TextBox()
        {
            Location = new System.Drawing.Point(20, 110),
            LabelText = "Enseignant-e-s",
            Width = 150
        };
        this.tabSeance.Controls.Add(txtEnseignant);

        var txtEquipePSPS = new TextBox()
        {
            Location = new System.Drawing.Point(20, 170),
            LabelText = "Equipe PSPS",
            Width = 150
        };
        this.tabSeance.Controls.Add(txtEquipePSPS);

        var txtProjets = new TextBox()
        {
            Location = new System.Drawing.Point(20, 230),
            LabelText = "Projets",
            Width = 150
        };
        this.tabSeance.Controls.Add(txtProjets);

        var txtGroupeMPP = new TextBox()
        {
            Location = new System.Drawing.Point(190, 50),
            LabelText = "Groupe MPP",
            Width = 150
        };
        this.tabSeance.Controls.Add(txtGroupeMPP);

        var txtReseauAvecParents = new TextBox()
        {
            Location = new System.Drawing.Point(190, 110),
            LabelText = "Réseau avec parents",
            Width = 150
        };
        this.tabSeance.Controls.Add(txtReseauAvecParents);

        var txtEquipePluriReseau = new TextBox()
        {
            Location = new System.Drawing.Point(190, 170),
            LabelText = "Equipe pluri-réseau",
            Width = 150
        };
        this.tabSeance.Controls.Add(txtEquipePluriReseau);

        var txtAutre = new TextBox()
        {
            Location = new System.Drawing.Point(190, 230),
            LabelText = "Autre",
            Width = 150
        };
        this.tabSeance.Controls.Add(txtAutre);

        var lblWarning = new Label()
        {
            Location = new System.Drawing.Point(20, 300),
            Text = "Au moin un des champs ci-dessus doit être rempli",
            AutoSize = true,
            ForeColor = System.Drawing.Color.FromArgb(255, 0, 0),
        };
        this.tabSeance.Controls.Add(lblWarning);

        // Onglet Entretien

        this.radioSeul = new RadioButton() { Text = "Seul", Location = new Point(20, 20), Checked = true };
        this.radioGroupe = new RadioButton() { Text = "Groupe", Location = new Point(100, 20) };
        this.radioClasse = new RadioButton() { Text = "Classe", Location = new Point(200, 20) };
        NumericUpDown numDureeEntretien = new NumericUpDown()
        {
            Name = "numDureeEntretien",
            Location = new Point(300, 20),
            Minimum = 0,
            Maximum = 240,
            Value = 0,
            Width = 80
        };

        tabEntretien.Controls.AddRange(new Control[] { radioSeul, radioGroupe, radioClasse, numDureeEntretien });

        var table = new TableLayoutPanel()
        {
            Location = new Point(10, 60),
            Size = new Size(410, 280),
            ColumnCount = 2,
            RowCount = 1,
            Dock = DockStyle.None,
            //AutoSize = true,
            //AutoSizeMode = AutoSizeMode.GrowAndShrink,
            BackColor = System.Drawing.Color.Transparent
        };

        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));


        string[] interventions = new string[]
{
    "Conduites addictives", "Incident critique", "Conflit entre élèves",
    "Incivilités / Violences", "Deuil", "Mal-hêtre", "Difficultés Apprentissage",
    "Question orientation\nprofessionnelle", "Difficultés familiales", "Stress", "Difficultés financières",
    "Suspicion maltraitance", "Discrimination", "Difficutés / tensions avec un∙e enseignant∙e enseignant∙e",
    "Harcèlement / Intimidation", "Genre - orientation sexuelle et affective", "Autre"
};

        int half = (int)Math.Ceiling(interventions.Length / 2.0);

        for (int i = 0; i < half; i++)
        {
            table.RowCount++;
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var cb1 = new CheckBox()
            {
                Text = interventions[i],
                Dock = DockStyle.Left,
                AutoSize = true,
                Width = 200
            };
            table.Controls.Add(cb1, 0, i);

            if (i + half < interventions.Length)
            {
                var cb2 = new CheckBox()
                {
                    Text = interventions[i + half],
                    Dock = DockStyle.Left,
                    AutoSize = true
                };
                table.Controls.Add(cb2, 1, i);
            }
        }
        tabEntretien.Controls.Add(table);


        this.tabType.Controls.AddRange(new Control[] { tabSeance, tabEntretien });
        this.Controls.Add(this.tabType);


        this.btnAjouter = new Button()
        {
            Text = "Ajouter",
            Location = new System.Drawing.Point(30, 610),
            Width = 120,
            BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
            ForeColor = System.Drawing.Color.White
        };
        this.btnAjouter.Click += new EventHandler(this.btnAjouter_Click);

        this.Controls.AddRange(new Control[]
        {
                datePicker, txtSujet, txtPersonnes,
                numDuree, btnAjouter
        });

        this.Text = "Nouvelle entrée";
        this.Size = new System.Drawing.Size(470, 710);
        this.StartPosition = FormStartPosition.CenterParent;
    }
}
