using Wisej.Web;


public partial class AccueilPage : UserControl
{
    public AccueilPage()
    {
        InitializeComponent();

        this.Text = "Accueil";

        var label = new Label()
        {
            Text = "Bienvenue sur l'accueil !",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        this.Controls.Add(label);
    }
}