using Wisej.Web;
using System.Linq;
using System;

public static class AutoTabIndexHelper
{
    public static void AutoTabIndex(this Control parent)
    {
        int tabIndex = 0;

        foreach (var ctrl in parent.Controls.Cast<Control>().OrderBy(c => c.Top).ThenBy(c => c.Left))
        {
            if (ctrl is TextBox || ctrl is NumericUpDown || ctrl is ComboBox || ctrl is DateTimePicker || ctrl is Button)
            {
                ctrl.TabIndex = tabIndex++;
                //AlertBox.Show($"[TabIndex Debug] {ctrl.Name} => {ctrl.TabIndex}");
            }

            // Gestion récursive : si un Panel ou autre container contient des champs
            if (ctrl.HasChildren)
            {
                ctrl.AutoTabIndex();
            }
        }
    }
}