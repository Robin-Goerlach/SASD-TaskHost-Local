using Microsoft.Data.Sqlite;

namespace TaskHostLocal.WinForms.Database;

/// <summary>
/// Legt die SQLite-Datenbankstruktur an, falls sie noch nicht existiert.
/// </summary>
public sealed class DatabaseInitializer
{
    private readonly DbConnectionFactory _connectionFactory;

    /// <summary>
    /// Erstellt einen neuen Initializer.
    /// </summary>
    public DatabaseInitializer(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Stellt sicher, dass alle benötigten Tabellen vorhanden sind und eine Standardliste existiert.
    /// </summary>
    public void EnsureDatabase()
    {
        using var connection = _connectionFactory.CreateOpenConnection();

        ExecuteNonQuery(connection, """
            CREATE TABLE IF NOT EXISTS task_lists (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                sort_order INTEGER NOT NULL DEFAULT 0,
                created_at TEXT NOT NULL,
                updated_at TEXT NOT NULL
            );
            """);

        ExecuteNonQuery(connection, """
            CREATE TABLE IF NOT EXISTS tasks (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                list_id INTEGER NOT NULL,
                title TEXT NOT NULL,
                description TEXT NULL,
                due_date TEXT NULL,
                priority INTEGER NOT NULL DEFAULT 0,
                is_completed INTEGER NOT NULL DEFAULT 0,
                created_at TEXT NOT NULL,
                updated_at TEXT NOT NULL,
                completed_at TEXT NULL,
                FOREIGN KEY (list_id) REFERENCES task_lists(id)
            );
            """);

        ExecuteNonQuery(connection, """
            CREATE INDEX IF NOT EXISTS idx_tasks_list_id ON tasks(list_id);
            """);

        ExecuteNonQuery(connection, """
            CREATE INDEX IF NOT EXISTS idx_tasks_due_date ON tasks(due_date);
            """);

        EnsureDefaultList(connection);
    }

    private static void EnsureDefaultList(SqliteConnection connection)
    {
        using var countCommand = connection.CreateCommand();
        countCommand.CommandText = "SELECT COUNT(*) FROM task_lists;";
        var count = Convert.ToInt64(countCommand.ExecuteScalar());

        if (count > 0)
        {
            return;
        }

        var now = DateTime.UtcNow.ToString("O");

        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = """
            INSERT INTO task_lists (name, sort_order, created_at, updated_at)
            VALUES ($name, $sortOrder, $createdAt, $updatedAt);
            """;
        insertCommand.Parameters.AddWithValue("$name", "Eingang");
        insertCommand.Parameters.AddWithValue("$sortOrder", 0);
        insertCommand.Parameters.AddWithValue("$createdAt", now);
        insertCommand.Parameters.AddWithValue("$updatedAt", now);
        insertCommand.ExecuteNonQuery();
    }

    private static void ExecuteNonQuery(SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}
