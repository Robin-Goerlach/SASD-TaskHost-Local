using Microsoft.Data.Sqlite;
using TaskHostLocal.WinForms.Database;
using TaskHostLocal.WinForms.Models;

namespace TaskHostLocal.WinForms.Repositories;

/// <summary>
/// Datenzugriff für Aufgaben.
/// </summary>
public sealed class TaskRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    /// <summary>
    /// Erstellt ein neues Repository.
    /// </summary>
    public TaskRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Gibt alle Aufgaben einer Liste zurück.
    /// </summary>
    public List<TaskItem> GetByListId(long listId)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = BaseSelectSql + Environment.NewLine + """
            WHERE list_id = $listId
            ORDER BY is_completed, due_date IS NULL, due_date, priority DESC, updated_at DESC;
            """;
        command.Parameters.AddWithValue("$listId", listId);
        return ReadTasks(command);
    }

    /// <summary>
    /// Sucht Aufgaben in Titel und Beschreibung über alle Listen.
    /// </summary>
    public List<TaskItem> Search(string searchText)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = BaseSelectSql + Environment.NewLine + """
            WHERE title LIKE $pattern
            OR COALESCE(description, '') LIKE $pattern
            ORDER BY is_completed, due_date IS NULL, due_date, priority DESC, updated_at DESC;
            """;
        command.Parameters.AddWithValue("$pattern", $"%{searchText.Trim()}%");
        return ReadTasks(command);
    }

    /// <summary>
    /// Legt eine neue Aufgabe an.
    /// </summary>
    public TaskItem Add(TaskItem task)
    {
        var now = DateTime.UtcNow;

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO tasks
                (list_id, title, description, due_date, priority, is_completed, created_at, updated_at, completed_at)
            VALUES
                ($listId, $title, $description, $dueDate, $priority, $isCompleted, $createdAt, $updatedAt, $completedAt)
            RETURNING id, list_id, title, description, due_date, priority, is_completed, created_at, updated_at, completed_at;
            """;

        AddTaskParameters(command, task, now, now);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Die Aufgabe konnte nicht angelegt werden.");
        }

        return MapTask(reader);
    }

    /// <summary>
    /// Aktualisiert eine vorhandene Aufgabe.
    /// </summary>
    public void Update(TaskItem task)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE tasks
            SET list_id = $listId,
                title = $title,
                description = $description,
                due_date = $dueDate,
                priority = $priority,
                is_completed = $isCompleted,
                updated_at = $updatedAt,
                completed_at = $completedAt
            WHERE id = $id;
            """;

        var now = DateTime.UtcNow;
        AddTaskParameters(command, task, task.CreatedAt == default ? now : task.CreatedAt, now);
        command.Parameters.AddWithValue("$id", task.Id);
        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Löscht eine Aufgabe endgültig.
    /// </summary>
    public void Delete(long taskId)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM tasks WHERE id = $id;";
        command.Parameters.AddWithValue("$id", taskId);
        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Setzt eine Aufgabe auf erledigt oder offen.
    /// </summary>
    public void SetCompleted(long taskId, bool isCompleted)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE tasks
            SET is_completed = $isCompleted,
                completed_at = $completedAt,
                updated_at = $updatedAt
            WHERE id = $id;
            """;
        command.Parameters.AddWithValue("$isCompleted", isCompleted ? 1 : 0);
        command.Parameters.AddWithValue("$completedAt", isCompleted ? DateTime.UtcNow.ToString("O") : DBNull.Value);
        command.Parameters.AddWithValue("$updatedAt", DateTime.UtcNow.ToString("O"));
        command.Parameters.AddWithValue("$id", taskId);
        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Zählt die Aufgaben in einer Liste.
    /// </summary>
    public long CountByListId(long listId)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM tasks WHERE list_id = $listId;";
        command.Parameters.AddWithValue("$listId", listId);
        return Convert.ToInt64(command.ExecuteScalar());
    }

    private const string BaseSelectSql = """
        SELECT id, list_id, title, description, due_date, priority, is_completed, created_at, updated_at, completed_at
        FROM tasks
        """;

    private static List<TaskItem> ReadTasks(SqliteCommand command)
    {
        var result = new List<TaskItem>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(MapTask(reader));
        }

        return result;
    }

    private static TaskItem MapTask(SqliteDataReader reader)
    {
        return new TaskItem
        {
            Id = reader.GetInt64(0),
            ListId = reader.GetInt64(1),
            Title = reader.GetString(2),
            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
            DueDate = reader.IsDBNull(4) ? null : DateTime.Parse(reader.GetString(4)),
            Priority = reader.GetInt32(5),
            IsCompleted = reader.GetInt32(6) != 0,
            CreatedAt = DateTime.Parse(reader.GetString(7), null, System.Globalization.DateTimeStyles.RoundtripKind),
            UpdatedAt = DateTime.Parse(reader.GetString(8), null, System.Globalization.DateTimeStyles.RoundtripKind),
            CompletedAt = reader.IsDBNull(9)
                ? null
                : DateTime.Parse(reader.GetString(9), null, System.Globalization.DateTimeStyles.RoundtripKind)
        };
    }

    private static void AddTaskParameters(SqliteCommand command, TaskItem task, DateTime createdAt, DateTime updatedAt)
    {
        command.Parameters.AddWithValue("$listId", task.ListId);
        command.Parameters.AddWithValue("$title", task.Title.Trim());
        command.Parameters.AddWithValue("$description", string.IsNullOrWhiteSpace(task.Description) ? DBNull.Value : task.Description.Trim());
        command.Parameters.AddWithValue("$dueDate", task.DueDate.HasValue ? task.DueDate.Value.ToString("yyyy-MM-dd") : DBNull.Value);
        command.Parameters.AddWithValue("$priority", task.Priority);
        command.Parameters.AddWithValue("$isCompleted", task.IsCompleted ? 1 : 0);
        command.Parameters.AddWithValue("$createdAt", createdAt.ToString("O"));
        command.Parameters.AddWithValue("$updatedAt", updatedAt.ToString("O"));
        command.Parameters.AddWithValue("$completedAt", task.IsCompleted ? DateTime.UtcNow.ToString("O") : DBNull.Value);
    }
}
