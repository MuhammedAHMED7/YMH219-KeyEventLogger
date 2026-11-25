namespace KeyEventLogger;

public sealed class MainForm : Form
{
    public MainForm()
    {
        Text = "KeyEventLogger";
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
    }
}
