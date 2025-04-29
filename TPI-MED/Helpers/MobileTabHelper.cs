using System;
using Wisej.Web;

public static class MobileTabHelper
{
    /// <summary>
    /// Applique la navigation automatique "suivant" et "valider" pour les champs dans un conteneur donné.
    /// </summary>
    /// <param name="container">Panel, Form, UserControl...</param>
    /// <param name="validationButton">Bouton à déclencher quand c'est le dernier champ</param>
    public static void ActiverNavigation(Control container, Button validationButton)
    {
        foreach (Control ctrl in container.Controls)
        {
            if (ctrl is TextBox || ctrl is NumericUpDown || ctrl is DateTimePicker)
            {
                ctrl.KeyPress += (s, e) =>
                {
                    if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)13)
                    {
                        Control current = s as Control;
                        Control suivant = container.GetNextControl(current, true);

                        if (suivant != null && (suivant is TextBox || suivant is NumericUpDown || suivant is DateTimePicker))
                        {
                            suivant.Focus();
                        }
                        else
                        {
                            validationButton?.PerformClick();
                        }

                        e.Handled = true;
                    }
                };
            }

            // Gestion récursive si le contrôle a des enfants (Panel imbriqué)
            if (ctrl.HasChildren)
                ActiverNavigation(ctrl, validationButton);
        }
    }
}
