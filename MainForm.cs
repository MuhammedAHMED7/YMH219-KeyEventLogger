namespace KeyEventLogger;

public sealed class MainForm : Form
{
    readonly KeyJournal journal = new();
    readonly ListBox events = new() { Dock=DockStyle.Fill, BorderStyle=BorderStyle.None,
        Font=new Font("Consolas",12), IntegralHeight=false, HorizontalScrollbar=true };
    readonly Label status=Layout.Status();
    readonly Button start,stop;
    public MainForm()
    {
        Layout.Window(this,"Key Event Studio  /  Klavye olayları");
        KeyPreview=true;
        var bar=Layout.Bar();
        start=Layout.Button("Başlat",(_,_)=>SetRecording(true));
        stop=Layout.Button("Durdur",(_,_)=>SetRecording(false));
        bar.Controls.AddRange([start,stop,Layout.Button("Temizle",(_,_)=>{journal.Clear();events.Items.Clear();RefreshStatus();}),
            Layout.Button("TXT olarak kaydet",(_,_)=>Save())]);
        var heading=new Label { Text="TARİH VE SAAT                          OLAY                TUŞ", Dock=DockStyle.Top,
            Height=39, Font=new Font("Consolas",10,FontStyle.Bold), Padding=new Padding(16,10,0,0) };
        var panel=new Panel { Dock=DockStyle.Fill, Padding=new Padding(16,9,16,12),BackColor=Color.White };
        panel.Controls.Add(events);
        Controls.AddRange([panel,heading,bar,Layout.Header("Key Event Studio"),status]);
        KeyDown+=(_,e)=>HandleKey(e,true);
        KeyUp+=(_,e)=>HandleKey(e,false);
        Deactivate+=(_,_)=>journal.ReleaseFocus();
        foreach(Control c in bar.Controls.Cast<Control>().Append(events))
            c.PreviewKeyDown+=(_,e)=>{if(journal.Recording && e.KeyCode != Keys.Tab)e.IsInputKey=true;};
        SetRecording(false);
    }
    void SetRecording(bool on)
    {
        journal.SetRecording(on); start.Enabled=!on; stop.Enabled=on;
        if(on) events.Focus();
        RefreshStatus();
    }
    void HandleKey(KeyEventArgs e,bool pressed)
    {
        if(!ContainsFocus || journal.Record(e.KeyCode,pressed) is not {} entry) return;
        events.Items.Add(entry.ToString()); events.TopIndex=events.Items.Count-1; RefreshStatus();
    }
    void RefreshStatus() => status.Text = $"{(journal.Recording ? "●  Kayıt açık" : "○  Kayıt durdu")}     |     {journal.Count} basış     ·     {journal.Entries.Count} olay";
    void Save()
    {
        using var dlg=new SaveFileDialog {Filter="Metin dosyası (*.txt)|*.txt",FileName="tus-olaylari.txt"};
        if(dlg.ShowDialog(this)!=DialogResult.OK)return;
        try {journal.Export(dlg.FileName); status.Text="Kaydedildi  ·  "+Path.GetFileName(dlg.FileName);}
        catch(Exception ex) when(ex is IOException or UnauthorizedAccessException) {status.Text="Dosya yazılamadı  ·  "+ex.Message;}
    }
}