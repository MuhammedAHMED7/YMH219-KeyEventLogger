namespace KeyEventLogger;

public sealed record KeyEntry(DateTime Time, Keys Key, bool Pressed)
{
    public override string ToString() => $"{Time:yyyy-MM-dd HH:mm:ss.fff}      {(Pressed ? "BASILDI  " : "BIRAKILDI")}      {Key}";
}

public sealed class KeyJournal
{
    readonly HashSet<Keys> down = [];
    readonly List<KeyEntry> entries = [];
    public bool Recording { get; private set; }
    public int Count { get; private set; }
    public IReadOnlyList<KeyEntry> Entries => entries;

    public void SetRecording(bool enabled) { Recording = enabled; ReleaseFocus(); }
    public void ReleaseFocus() => down.Clear();
    public void Clear() { entries.Clear(); Count = 0; }
    public KeyEntry? Record(Keys key, bool pressed)
    {
        if (!Recording || !(pressed ? down.Add(key) : down.Remove(key))) return null;
        if (pressed) Count++;
        var entry = new KeyEntry(DateTime.Now,key,pressed);
        entries.Add(entry);
        return entry;
    }
    public void Export(string path) => File.WriteAllLines(path, entries.Select(e => e.ToString()), new System.Text.UTF8Encoding(true));
}