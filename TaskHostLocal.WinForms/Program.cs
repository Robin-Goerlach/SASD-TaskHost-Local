using TaskHostLocal.WinForms.Database;
using TaskHostLocal.WinForms.Repositories;
using TaskHostLocal.WinForms.Services;

namespace TaskHostLocal.WinForms;

/// <summary>
/// Einstiegspunkt der Anwendung.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Startet TaskHost Local.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // Initialisiert Windows-Forms-Standardeinstellungen wie DPI, Fonts und visuelle Styles.
        ApplicationConfiguration.Initialize();

        // Für V1 bauen wir die Abhängigkeiten bewusst direkt hier zusammen.
        // Später könnte dies durch Dependency Injection ersetzt werden.
        var connectionFactory = new DbConnectionFactory();
        var databaseInitializer = new DatabaseInitializer(connectionFactory);
        databaseInitializer.EnsureDatabase();

        var listRepository = new ListRepository(connectionFactory);
        var taskRepository = new TaskRepository(connectionFactory);

        var listService = new ListService(listRepository, taskRepository);
        var taskService = new TaskService(taskRepository);
        var backupService = new BackupService(connectionFactory);

        Application.Run(new MainForm(listService, taskService, backupService));
    }
}
