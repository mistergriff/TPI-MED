using Wisej.Web;


public partial class StatsPage : UserControl
{
    public StatsPage()
    {
        InitializeComponent();
        this.Text = "Stats";

        var label = new Label()
        {
            Text = "Bienvenue sur les stats !",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        this.Controls.Add(label);
    }
}