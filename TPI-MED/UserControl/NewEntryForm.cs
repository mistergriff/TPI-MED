//////////////////////////////////////////////////////////////////////////
//      Auteur: Renaud Grégory                                          //
//      Date de création: 29.04.2025                                    //
//      Description: Formulaire d'ajout et de modification d'évènement  //
//      Date de dernière révision: 12.05.2025                           //
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using Wisej.Web;

/// <summary>
/// Formulaire pour ajouter ou modifier une nouvelle entrée (événement, entretien ou séance).
/// </summary>
public partial class NewEntryForm : Form
{
    /// <summary>
    /// Événement à éditer (si applicable).
    /// </summary>
    private Event _eventToEdit;

    /// <summary>
    /// Entretien à éditer (si applicable).
    /// </summary>
    private Interview _interviewToEdit;

    /// <summary>
    /// Liste des séances à éditer (si applicable).
    /// </summary>
    private List<Seance> _seancesToEdit;

    /// <summary>
    /// Dictionnaire associant les libellés des motivations aux propriétés correspondantes.
    /// </summary>
    private readonly Dictionary<string, string> _labelToProperty = new Dictionary<string, string>()
    {
        { "Conduites addictives", "addictive_behaviors" },
        { "Incident critique", "critical_incident" },
        { "Conflit entre élèves", "student_conflict" },
        { "Incivilités / Violences", "incivility_violence" },
        { "Deuil", "grief" },
        { "Mal-être", "unhappiness" },
        { "Difficultés Apprentissage", "learning_difficulties" },
        { "Question orientation professionnelle", "career_guidance_issues" },
        { "Difficultés familiales", "family_difficulties" },
        { "Stress", "stress" },
        { "Difficultés financières", "financial_difficulties" },
        { "Suspicion maltraitance", "suspected_abuse" },
        { "Discrimination", "discrimination" },
        { "Difficutés / tensions avec un∙e enseignant∙e", "difficulties_tensions_with_a_teacher" },
        { "Harcèlement / Intimidation", "harassment_intimidation" },
        { "Genre - orientation sexuelle et affective", "gender_sexual_orientation" },
        { "Autre", "other" }
    };

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="NewEntryForm"/>.
    /// </summary>
    /// <param name="evt">Événement à éditer (ou null pour un nouvel événement).</param>
    /// <param name="interview">Entretien à éditer (ou null).</param>
    /// <param name="seances">Liste des séances à éditer (ou null).</param>
    public NewEntryForm(Event evt, Interview interview = null, List<Seance> seances = null)
    {
        _eventToEdit = evt;
        _interviewToEdit = interview;
        _seancesToEdit = seances;

        InitializeComponent();
        FillInTheFields();
    }

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="NewEntryForm"/> sans paramètres.
    /// </summary>
    public NewEntryForm() : this(null, null, null)
    {
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton "Ajouter".
    /// Valide les données saisies et enregistre l'entrée (événement, entretien ou séance).
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement associées.</param>
    private void btnAjouter_Click(object sender, EventArgs e)
    {
        var date = datePicker.Value;
        var sujet = txtSujet.Text;
        var personnes = txtPersonnes.Text;
        var dureeAdmin = (int)numDuree.Value;
        var type = tabType.SelectedTab.Text;

        if (!DateHelper.IsInCurrentSchoolYear(date))
        {
            datePicker.Value = DateTime.Today;
            AlertBox.Show("La date doit être comprise dans l'année scolaire courante.", MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrEmpty(sujet))
        {
            AlertBox.Show("Veuillez entrer un sujet.", MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrEmpty(personnes))
        {
            AlertBox.Show("Veuillez entrer des personnes.", MessageBoxIcon.Warning);
            return;
        }

        int? interviewId = null;
        int eventId = 0;

        int userId = (int)Application.Session["userId"];

        if (type == "Entretien")
        {
            if(selectedRadioButton == "")
            {
                AlertBox.Show("Veuillez sélectionner un type d'entretien.", MessageBoxIcon.Warning);
                return;
            }

            string radioButtonSelection = selectedRadioButton;

            // Déterminer l'ID du type d'entretien en fonction de la sélection
            int interviewTypeId;
            switch (radioButtonSelection)
            {
                case "Groupe":
                    interviewTypeId = 2;
                    break;
                case "Classe":
                    interviewTypeId = 3;
                    break;
                default:
                    interviewTypeId = 1;
                    break;
            }

            var cbList = GetCheckedInterventionKeys(table);

            if (cbList.Count == 0)
            {
                AlertBox.Show("Veuillez sélectionner au moins une motivation.", MessageBoxIcon.Warning);
                return;
            }

            var interview = new Interview()
            {
                Time = (int)numDureeEntretien.Value,
                InterviewTypeId = interviewTypeId,
                addictive_behaviors = cbList.Contains("Conduites addictives"),
                critical_incident = cbList.Contains("Incident critique"),
                student_conflict = cbList.Contains("Conflit entre élèves"),
                incivility_violence = cbList.Contains("Incivilités / Violences"),
                grief = cbList.Contains("Deuil"),
                unhappiness = cbList.Contains("Mal-être"),
                learning_difficulties = cbList.Contains("Difficultés Apprentissage"),
                career_guidance_issues = cbList.Contains("Question orientation professionnelle"),
                family_difficulties = cbList.Contains("Difficultés familiales"),
                stress = cbList.Contains("Stress"),
                financial_difficulties = cbList.Contains("Difficultés financières"),
                suspected_abuse = cbList.Contains("Suspicion maltraitance"),
                discrimination = cbList.Contains("Discrimination"),
                difficulties_tensions_with_a_teacher = cbList.Contains("Difficutés / tensions avec un∙e enseignant∙e"),
                harassment_intimidation = cbList.Contains("Harcèlement / Intimidation"),
                gender_sexual_orientation = cbList.Contains("Genre - orientation sexuelle et affective"),
                other = cbList.Contains("Autre")
            };

            if (_eventToEdit == null)
            {
                interviewId = new InterviewDAO().AddInterview(interview);
            }
            else
            {
                interview.Id = _eventToEdit.InterviewId ?? 0;
                new InterviewDAO().EditInterview(interview);
                interviewId = interview.Id;
            }
        }

        // Création de l'événement
        if (_eventToEdit == null)
        {
            var evenement = new Event()
            {
                Date = date,
                Sujet = sujet,
                Personne = personnes,
                TempsAdmin = dureeAdmin,
                UserId = userId,
                InterviewId = interviewId
            };

            eventId = new EventDAO().AddEvent(evenement);
        }
        else
        {
            _eventToEdit.Date = date;
            _eventToEdit.Sujet = sujet;
            _eventToEdit.Personne = personnes;
            _eventToEdit.TempsAdmin = dureeAdmin;
            _eventToEdit.InterviewId = interviewId;

            new EventDAO().EditEvent(_eventToEdit);
            eventId = _eventToEdit.Id;
        }

        // Enregistrer les séances si onglet Séance
        if (type == "Séance")
        {
            var seances = new List<Seance>();

            void TryAddSeance(int temps, int sessionTypeId)
            {
                if (temps > 0)
                {
                    seances.Add(new Seance() { TypeId = sessionTypeId, Temps = temps });
                }
            }

            TryAddSeance((int)txtDirection.Value, 1);
            TryAddSeance((int)txtEnseignant.Value, 2);
            TryAddSeance((int)txtEquipePSPS.Value, 3);
            TryAddSeance((int)txtProjets.Value, 4);
            TryAddSeance((int)txtGroupeMPP.Value, 5);
            TryAddSeance((int)txtReseauAvecParents.Value, 6);
            TryAddSeance((int)txtEquipePluriReseau.Value, 7);
            TryAddSeance((int)txtAutre.Value, 8);

            if (seances.Count == 0)
            {
                AlertBox.Show("Veuillez entrer au moins un temps de séance.", MessageBoxIcon.Warning);
                return;
            }

            new SeanceDAO().EditSessionsForEvent(eventId, seances);
        }

        AlertBox.Show("Entrée enregistrée avec succès !", MessageBoxIcon.Information);
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    /// <summary>
    /// Remplit les champs du formulaire avec les données de l'événement, de l'entretien ou des séances à éditer.
    /// </summary>
    private void FillInTheFields()
    {
        if (_eventToEdit == null) return;

        this.Text = "Editer l'entrée";
        this.btnAjouter.Text = "Modifier";

        // Champs communs
        datePicker.Value = _eventToEdit.Date;
        txtSujet.Text = _eventToEdit.Sujet;
        txtPersonnes.Text = _eventToEdit.Personne;
        numDuree.Value = _eventToEdit.TempsAdmin;

        if (_eventToEdit.InterviewId.HasValue && _interviewToEdit != null)
        {
            tabType.SelectedTab = tabEntretien;
            tabType.TabPages.Remove(tabSeance);
            numDureeEntretien.Value = _interviewToEdit.Time;

            switch (_interviewToEdit.InterviewTypeId)
            {
                case 1: radioSeul.Checked = true; break;
                case 2: radioGroupe.Checked = true; break;
                case 3: radioClasse.Checked = true; break;
            }

            // Check des motivations
            var boolProps = typeof(Interview).GetProperties();
            foreach (var prop in boolProps)
            {
                if (prop.PropertyType == typeof(bool) && (bool)prop.GetValue(_interviewToEdit))
                {
                    foreach (Control ctrl in table.Controls)
                    {
                        if (ctrl is CheckBox cb && _labelToProperty.TryGetValue(cb.Text, out var propName))
                        {
                            var propInfo = typeof(Interview).GetProperty(propName);
                            if (propInfo != null && propInfo.PropertyType == typeof(bool) && (bool)propInfo.GetValue(_interviewToEdit))
                            {
                                cb.Checked = true;
                            }
                        }
                    }
                }
            }
        }
        else if (_seancesToEdit != null)
        {
            tabType.SelectedTab = tabSeance;
            tabType.TabPages.Remove(tabEntretien);
            foreach (var seance in _seancesToEdit)
            {
                switch (seance.TypeId)
                {
                    case 1: txtDirection.Text = seance.Temps.ToString(); break;
                    case 2: txtEnseignant.Text = seance.Temps.ToString(); break;
                    case 3: txtEquipePSPS.Text = seance.Temps.ToString(); break;
                    case 4: txtProjets.Text = seance.Temps.ToString(); break;
                    case 5: txtGroupeMPP.Text = seance.Temps.ToString(); break;
                    case 6: txtReseauAvecParents.Text = seance.Temps.ToString(); break;
                    case 7: txtEquipePluriReseau.Text = seance.Temps.ToString(); break;
                    case 8: txtAutre.Text = seance.Temps.ToString(); break;
                }
            }
        }
    }
}