using TaskHostLocal.WinForms.Database;

namespace TaskHostLocal.WinForms.Services;

/// <summary>
/// Erstellt einfache Dateisicherungen der lokalen SQLite-Datenbank.
/// </summary>
public sealed class BackupService
{
    private readonly DbConnectionFactory _connectionFactory;

    /// <summary>
    /// Erstellt einen neuen Backup-Service.
    /// </summary>
    public BackupService(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Kopiert die Datenbankdatei in den angegebenen Zielordner.
    /// </summary>
    public string CreateBackup(string targetDirectory)
    {
        if (string.IsNullOrWhiteSpace(targetDirectory))
        {
            throw new ArgumentException("Der Zielordner darf nicht leer sein.", nameof(targetDirectory));
        }

        Directory.CreateDirectory(targetDirectory);

        var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHHmmss");
        var targetFile = Path.Combine(targetDirectory, $"taskhost-backup-{timestamp}.db");

        File.Copy(_connectionFactory.DatabasePath, targetFile, overwrite: false);
        return targetFile;
    }
}
