using Wisej.Web;
using System.Linq;

/// <summary>
/// Fournit une méthode d'extension pour attribuer automatiquement des indices de tabulation (TabIndex) aux contrôles d'un conteneur.
/// </summary>
public static class AutoTabIndexHelper
{
    /// <summary>
    /// Attribue automatiquement des indices de tabulation (TabIndex) aux contrôles enfants d'un conteneur parent.
    /// Les contrôles sont triés par leur position verticale (Top) puis horizontale (Left).
    /// </summary>
    /// <param name="parent">Le contrôle parent contenant les contrôles enfants à traiter.</param>
    public static void AutoTabIndex(this Control parent)
    {
        int tabIndex = 0;

        // Parcourt les contrôles enfants triés par position verticale (Top) puis horizontale (Left).
        foreach (var ctrl in parent.Controls.Cast<Control>().OrderBy(c => c.Top).ThenBy(c => c.Left))
        {
            // Attribue un TabIndex uniquement aux contrôles interactifs.
            if (ctrl is TextBox || ctrl is NumericUpDown || ctrl is ComboBox || ctrl is DateTimePicker || ctrl is Button)
            {
                ctrl.TabIndex = tabIndex++;
                //AlertBox.Show($"[TabIndex Debug] {ctrl.Name} => {ctrl.TabIndex}");
            }

            // Gestion récursive : applique également l'AutoTabIndex aux contrôles enfants si le contrôle actuel est un conteneur.
            if (ctrl.HasChildren)
            {
                ctrl.AutoTabIndex();
            }
        }
    }
}
