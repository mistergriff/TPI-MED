using System;
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
        var duree = (int)numDuree.Value;

        // TODO: Enregistrer l'entrée dans la base ou la liste de test
        AlertBox.Show($"Entrée ajoutée :\n - {date:d} - {sujet}", MessageBoxIcon.Information);

        this.Close(); // Ferme la fenêtre après ajout
    }
}
