using System.Drawing;

namespace KeyEventLogger;

internal static class StudioUi
{
    public static readonly Color Ink = ColorTranslator.FromHtml("#172B36");
    public static readonly Color Accent = ColorTranslator.FromHtml("#51417E");
    public static void Window(Form form, string title)
    {
        form.Text = title;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.Font = new Font("Segoe UI", 10);
        form.BackColor = Color.FromArgb(246,247,248);
        form.ForeColor = Ink;
        form.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        form.ClientSize = new Size(900,600);
        form.MinimumSize = new Size(790,540);
    }
    public static Label Header(string text) => new()
    {
        Text = text, Dock = DockStyle.Top, Height = 79, Padding = new Padding(22,0,0,0),
        TextAlign = ContentAlignment.MiddleLeft, BackColor = Accent, ForeColor = Color.White,
        Font = new Font("Segoe UI", 23, FontStyle.Bold)
    };
    public static Label Status() => new()
    {
        Dock = DockStyle.Bottom, Height = 48, Padding = new Padding(16,0,12,0),
        TextAlign = ContentAlignment.MiddleLeft, BackColor = Ink, ForeColor = Color.White
    };
    public static FlowLayoutPanel Bar(int height = 55) => new()
    {
        Dock = DockStyle.Top, Height = height, AutoScroll = true, WrapContents = true,
        Padding = new Padding(9,6,4,4), BackColor = Color.White
    };
    public static Button Button(string text, EventHandler click)
    {
        var b = new Button {Text=text, AutoSize=true, Height=33, Padding=new Padding(7,1,7,1),
            FlatStyle=FlatStyle.Flat, ForeColor=Accent, BackColor=Color.White};
        b.FlatAppearance.BorderColor = Color.FromArgb(195,204,209);
        b.Click += click;
        return b;
    }
    public static Label Tag(string text) => new()
    {
        Text=text, AutoSize=true, Margin=new Padding(8,9,3,0), ForeColor=Ink
    };
}