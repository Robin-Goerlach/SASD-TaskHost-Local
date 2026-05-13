namespace TaskHostLocal.WinForms.Models;

/// <summary>
/// Repräsentiert eine Aufgabenliste, zum Beispiel "Eingang", "SASD" oder "Privat".
/// </summary>
public sealed class TaskList
{
    /// <summary>
    /// Technische ID aus der SQLite-Datenbank.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Anzeigename der Liste.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Sortierposition. Für V1 wird diese nur einfach gespeichert.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// UTC-Zeitpunkt der Erstellung.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// UTC-Zeitpunkt der letzten Änderung.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Sorgt dafür, dass die ListBox ohne weitere Konfiguration einen sinnvollen Text anzeigt.
    /// </summary>
    public override string ToString() => Name;
}
