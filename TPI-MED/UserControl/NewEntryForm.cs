using System;
using System.Collections.Generic;
using Wisej.Web;

public partial class NewEntryForm : Form
{
    public NewEntryForm()
    {
        InitializeComponent();
    }

    private void btnAjouter_Click(object sender, EventArgs e)
    {
        var date = datePicker.Value;
        var sujet = txtSujet.Text;
        var personnes = txtPersonnes.Text;
        var dureeAdmin = (int)numDuree.Value;
        var type = tabType.SelectedTab.Text;

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
        int eventId;

        int userId = (int)Application.Session["userId"];

        if (type == "Entretien")
        {
            string radioButtonSelection = selectedRadioButton.Text;

            if (string.IsNullOrEmpty(radioButtonSelection))
            {
                AlertBox.Show("Veuillez sélectionner un type d'entretien.", MessageBoxIcon.Warning);
                return;
            }

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
                difficulties_tensions_with_a_teacher = cbList.Contains("Difficutés / tensions avec un∙e enseignant∙e enseignant∙e"),
                harassment_intimidation = cbList.Contains("Harcèlement / Intimidation"),
                gender_sexual_orientation = cbList.Contains("Genre - orientation sexuelle et affective"),
                other = cbList.Contains("Autre")
            };

            interviewId = new InterviewDAO().AjouterInterview(interview);
        }

        // Création de l'événement
        var evenement = new Event()
        {
            Date = date,
            Sujet = sujet,
            Personne = personnes,
            TempsAdmin = dureeAdmin,
            UserId = userId,
            InterviewId = interviewId
        };

        eventId = new EventDAO().AjouterEvenement(evenement);

        // Enregistrer les séances si onglet Séance
        if (type == "Séance")
        {
            var seances = new List<Seance>();

            void TryAddSeance(string nomChamp, string texte, int sessionTypeId)
            {
                if (int.TryParse(texte, out int t) && t > 0)
                {
                    seances.Add(new Seance() { TypeId = sessionTypeId, Temps = t });
                }
            }

            TryAddSeance("Direction", txtDirection.Text, 1);
            TryAddSeance("Enseignant", txtEnseignant.Text, 2);
            TryAddSeance("Équipe PSPS", txtEquipePSPS.Text, 3);
            TryAddSeance("Projets", txtProjets.Text, 4);
            TryAddSeance("Groupe MPP", txtGroupeMPP.Text, 5);
            TryAddSeance("Réseau avec parents", txtReseauAvecParents.Text, 6);
            TryAddSeance("Équipe pluri-réseau", txtEquipePluriReseau.Text, 7);
            TryAddSeance("Autre", txtAutre.Text, 8);

            if (seances.Count == 0)
            {
                AlertBox.Show("Veuillez entrer au moins un temps de séance.", MessageBoxIcon.Warning);
                return;
            }

            if (seances.Count > 0)
                new SeanceDAO().AjouterSeancesPourEvenement(eventId, seances);
        }

        AlertBox.Show("Entrée enregistrée avec succès !", MessageBoxIcon.Information);
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

}