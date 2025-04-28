using Wisej.Web;


public partial class DataPage : UserControl
{
    public DataPage()
    {
        InitializeComponent();
        this.Text = "Data";

        var label = new Label()
        {
            Text = "Bienvenue sur le données !",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        this.Controls.Add(label);
    }
}