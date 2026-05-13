namespace TaskHostLocal.WinForms.Models;

/// <summary>
/// Repräsentiert eine einzelne Aufgabe innerhalb einer Aufgabenliste.
/// </summary>
public sealed class TaskItem
{
    /// <summary>
    /// Technische ID aus der SQLite-Datenbank.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// ID der zugehörigen Aufgabenliste.
    /// </summary>
    public long ListId { get; set; }

    /// <summary>
    /// Kurzer Aufgabentitel. Dieses Feld ist Pflicht.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Längere Notiz oder Beschreibung zur Aufgabe.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Optionales Fälligkeitsdatum. Für V1 behandeln wir dies als Datum ohne Uhrzeit.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Einfache Priorität. 0 = normal, höhere Werte = wichtiger.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Gibt an, ob die Aufgabe erledigt ist.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// UTC-Zeitpunkt der Erstellung.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// UTC-Zeitpunkt der letzten Änderung.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// UTC-Zeitpunkt der Erledigung. Null, wenn die Aufgabe offen ist.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Anzeige-Hilfsfeld für die Tabelle.
    /// </summary>
    public string DueDateDisplay => DueDate?.ToString("yyyy-MM-dd") ?? string.Empty;

    /// <summary>
    /// Anzeige-Hilfsfeld für die Tabelle.
    /// </summary>
    public string UpdatedAtDisplay => UpdatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
}
