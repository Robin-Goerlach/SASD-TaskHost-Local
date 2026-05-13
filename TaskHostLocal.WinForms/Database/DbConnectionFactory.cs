using Microsoft.Data.Sqlite;

namespace TaskHostLocal.WinForms.Database;

/// <summary>
/// Erstellt SQLite-Verbindungen und kapselt den Speicherort der lokalen Datenbank.
/// </summary>
public sealed class DbConnectionFactory
{
    private const string VendorFolderName = "SASD";
    private const string ApplicationFolderName = "TaskHostLocal";
    private const string DatabaseFileName = "taskhost.db";

    /// <summary>
    /// Verzeichnis, in dem die lokale SQLite-Datenbank abgelegt wird.
    /// </summary>
    public string DataDirectory { get; }

    /// <summary>
    /// Vollständiger Pfad zur lokalen SQLite-Datenbankdatei.
    /// </summary>
    public string DatabasePath { get; }

    /// <summary>
    /// Initialisiert eine neue Factory und berechnet den Datenbankpfad unter %AppData%.
    /// </summary>
    public DbConnectionFactory()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        DataDirectory = Path.Combine(appData, VendorFolderName, ApplicationFolderName);
        DatabasePath = Path.Combine(DataDirectory, DatabaseFileName);
    }

    /// <summary>
    /// Erstellt eine geöffnete SQLite-Verbindung.
    /// </summary>
    public SqliteConnection CreateOpenConnection()
    {
        Directory.CreateDirectory(DataDirectory);

        var connectionStringBuilder = new SqliteConnectionStringBuilder
        {
            DataSource = DatabasePath,
            ForeignKeys = true
        };

        var connection = new SqliteConnection(connectionStringBuilder.ToString());
        connection.Open();
        return connection;
    }
}
