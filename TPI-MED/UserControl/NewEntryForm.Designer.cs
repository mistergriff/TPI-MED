///////////////////////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                                       //
//      Date de création: 29.04.2025                                                 //
//      Description: Designer du formulaire pour entrer et modifier des évènements   //
//      Date de dernière révision: 12.05.2025                                        //
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using Wisej.Web;
using ZstdSharp.Unsafe;


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
    private String selectedRadioButton = null;
    private NumericUpDown numDureeEntretien;
    private NumericUpDown txtDirection;
    private NumericUpDown txtEnseignant;
    private NumericUpDown txtEquipePSPS;
    private NumericUpDown txtProjets;
    private NumericUpDown txtGroupeMPP;
    private NumericUpDown txtReseauAvecParents;
    private NumericUpDown txtEquipePluriReseau;
    private NumericUpDown txtAutre;
    private Label lblWarning;
    private TableLayoutPanel table;
    private Button btnAjouter;

    private void InitializeComponent()
    {
        // Crée les composants de saisie à gauche
        this.datePicker = new DateTimePicker() { Width = 250 };
        this.txtSujet = new TextBox() { Watermark = "Sujet", Width = 250 };
        this.txtPersonnes = new TextBox() { Watermark = "Personnes concernées", Width = 250 };
        this.numDuree = new NumericUpDown()
        {
            LabelText = "Durée (minutes)",
            Width = 120,
            Minimum = 0,
            Maximum = 300,
            Value = 0
        };
        this.btnAjouter = new Button()
        {
            Text = "Ajouter",
            Width = 200,
            BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
            ForeColor = System.Drawing.Color.White
        };
        this.btnAjouter.Click += new EventHandler(this.btnAjouter_Click);

        var panelGauche = new TableLayoutPanel()
        {
            Dock = DockStyle.Fill,
            RowCount = 6,
            ColumnCount = 1,
            Padding = new Padding(20),
        };
        panelGauche.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // date
        panelGauche.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // sujet
        panelGauche.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // personnes
        panelGauche.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // durée
        panelGauche.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // espace extensible
        panelGauche.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // bouton

        panelGauche.Controls.Add(datePicker, 0, 0);
        panelGauche.Controls.Add(txtSujet, 0, 1);
        panelGauche.Controls.Add(txtPersonnes, 0, 2);
        panelGauche.Controls.Add(numDuree, 0, 3);

        // Centrage du bouton "Ajouter"
        btnAjouter.Anchor = AnchorStyles.None;
        panelGauche.Controls.Add(btnAjouter, 0, 5);

        // ---- Onglet Séance ----
        this.tabSeance = new TabPage() { Text = "Séance" };

        // Crée un tableau à deux colonnes pour 4 champs à gauche et 4 à droite
        var gridSeance = new TableLayoutPanel()
        {
            Dock = DockStyle.Top,
            ColumnCount = 2,
            AutoSize = true,
            Padding = new Padding(20),
        };

        gridSeance.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        gridSeance.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        // Colonne de gauche
        this.txtDirection = new NumericUpDown() { LabelText = "Direction", Width = 250, Minimum = 0, Maximum = 240 };
        this.txtEnseignant = new NumericUpDown() { LabelText = "Enseignant-e-s", Width = 250, Minimum = 0, Maximum = 240 };
        this.txtEquipePSPS = new NumericUpDown() { LabelText = "Équipe PSPS", Width = 250, Minimum = 0, Maximum = 240 };
        this.txtProjets = new NumericUpDown() {LabelText = "Projets", Width = 250, Minimum = 0, Maximum = 240 };

        // Colonne de droite
        this.txtGroupeMPP = new NumericUpDown() {LabelText = "Groupe MPP", Width = 250, Minimum = 0, Maximum = 240 };
        this.txtReseauAvecParents = new NumericUpDown() {LabelText = "Réseau avec parents", Width = 250, Minimum = 0, Maximum = 240 };
        this.txtEquipePluriReseau = new NumericUpDown() {LabelText = "Équipe pluri-réseau", Width = 250, Minimum = 0, Maximum = 240 };
        this.txtAutre = new NumericUpDown() {LabelText = "Autre", Width = 250, Minimum = 0, Maximum = 240 };

        // Ajout ligne par ligne dans le tableau
        gridSeance.RowCount = 4;
        for (int i = 0; i < 4; i++)
            gridSeance.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        gridSeance.Controls.Add(txtDirection, 0, 0);
        gridSeance.Controls.Add(txtGroupeMPP, 1, 0);
        gridSeance.Controls.Add(txtEnseignant, 0, 1);
        gridSeance.Controls.Add(txtReseauAvecParents, 1, 1);
        gridSeance.Controls.Add(txtEquipePSPS, 0, 2);
        gridSeance.Controls.Add(txtEquipePluriReseau, 1, 2);
        gridSeance.Controls.Add(txtProjets, 0, 3);
        gridSeance.Controls.Add(txtAutre, 1, 3);

        // Label d’avertissement
        this.lblWarning = new Label()
        {
            Text = "Au moins un des champs ci-dessus doit être rempli",
            AutoSize = true,
            ForeColor = Color.Red,
            Margin = new Padding(20, 10, 0, 0)
        };

        // Ajouter dans un panel vertical
        var panelSeance = new FlowLayoutPanel()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            AutoScroll = true,
            Padding = new Padding(10)
        };

        panelSeance.Controls.Add(gridSeance);
        panelSeance.Controls.Add(lblWarning);
        this.tabSeance.Controls.Add(panelSeance);


        // ---- Onglet Entretien ----
        this.tabEntretien = new TabPage() { Text = "Entretien" };
        this.radioSeul = new RadioButton() { Text = "Seul" };
        this.radioGroupe = new RadioButton() { Text = "Groupe" };
        this.radioClasse = new RadioButton() { Text = "Classe" };
        this.radioSeul.CheckedChanged += (s, e) => { selectedRadioButton = radioSeul.Text; };
        this.radioGroupe.CheckedChanged += (s, e) => { selectedRadioButton = radioGroupe.Text; };
        this.radioClasse.CheckedChanged += (s, e) => { selectedRadioButton = radioClasse.Text; };

        this.numDureeEntretien = new NumericUpDown()
        {
            Minimum = 0,
            Maximum = 240,
            Value = 0,
            Width = 150
        };

        var groupeType = new FlowLayoutPanel()
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true
        };
        groupeType.Controls.AddRange(new Control[] { radioSeul, radioGroupe, radioClasse, numDureeEntretien });

        this.table = new TableLayoutPanel()
        {
            ColumnCount = 2,
            AutoSize = true
        };
        this.table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        this.table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        string[] interventions = new string[]
        {
        "Conduites addictives", "Incident critique", "Conflit entre élèves",
        "Incivilités / Violences", "Deuil", "Mal-être", "Difficultés Apprentissage",
        "Question orientation professionnelle", "Difficultés familiales", "Stress", "Difficultés financières",
        "Suspicion maltraitance", "Discrimination", "Difficutés / tensions avec un∙e enseignant∙e enseignant∙e",
        "Harcèlement / Intimidation", "Genre - orientation sexuelle et affective", "Autre"
        };
        int half = (int)Math.Ceiling(interventions.Length / 2.0);
        for (int i = 0; i < half; i++)
        {
            var cb1 = new CheckBox() { Text = interventions[i], Name = interventions[i], Dock = DockStyle.Left };
            table.Controls.Add(cb1, 0, i);
            if (i + half < interventions.Length)
            {
                var cb2 = new CheckBox() { Text = interventions[i + half], Name = interventions[i + half], Dock = DockStyle.Left };
                table.Controls.Add(cb2, 1, i);
            }
        }

        var panelEntretien = new FlowLayoutPanel()
        {
            FlowDirection = FlowDirection.TopDown,
            AutoScroll = true,
            Dock = DockStyle.Fill
        };
        panelEntretien.Controls.Add(groupeType);
        panelEntretien.Controls.Add(table);
        this.tabEntretien.Controls.Add(panelEntretien);

        // TabControl
        this.tabType = new TabControl()
        {
            Dock = DockStyle.Fill
        };
        this.tabType.Controls.AddRange(new Control[] { tabSeance, tabEntretien });

        var panelDroite = new Panel()
        {
            Dock = DockStyle.Fill
        };
        panelDroite.Controls.Add(tabType);

        // Mise en page horizontale
        var layout = new TableLayoutPanel()
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.Controls.Add(panelGauche, 0, 0);
        layout.Controls.Add(panelDroite, 1, 0);

        this.Controls.Add(layout);
        this.Text = "Nouvelle entrée";
        this.Size = new Size(900, 460);
        this.StartPosition = FormStartPosition.CenterParent;
    }

    public List<string> GetCheckedInterventionKeys(Control container)
    {
        var checkedItems = new List<string>();

        foreach (Control ctrl in container.Controls)
        {
            if (ctrl is CheckBox cb && cb.Checked)
            {
                checkedItems.Add(cb.Name);
            }

            if (ctrl.HasChildren)
            {
                checkedItems.AddRange(GetCheckedInterventionKeys(ctrl));
            }
        }

        return checkedItems;
    }

}
