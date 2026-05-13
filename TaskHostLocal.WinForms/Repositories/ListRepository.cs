using Microsoft.Data.Sqlite;
using TaskHostLocal.WinForms.Database;
using TaskHostLocal.WinForms.Models;

namespace TaskHostLocal.WinForms.Repositories;

/// <summary>
/// Datenzugriff für Aufgabenlisten.
/// </summary>
public sealed class ListRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    /// <summary>
    /// Erstellt ein neues Repository.
    /// </summary>
    public ListRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Gibt alle Aufgabenlisten sortiert zurück.
    /// </summary>
    public List<TaskList> GetAll()
    {
        var result = new List<TaskList>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT id, name, sort_order, created_at, updated_at
            FROM task_lists
            ORDER BY sort_order, name;
            """;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(MapList(reader));
        }

        return result;
    }

    /// <summary>
    /// Legt eine neue Aufgabenliste an und gibt sie zurück.
    /// </summary>
    public TaskList Add(string name)
    {
        var now = DateTime.UtcNow;

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO task_lists (name, sort_order, created_at, updated_at)
            VALUES ($name, $sortOrder, $createdAt, $updatedAt)
            RETURNING id, name, sort_order, created_at, updated_at;
            """;
        command.Parameters.AddWithValue("$name", name.Trim());
        command.Parameters.AddWithValue("$sortOrder", GetNextSortOrder(connection));
        command.Parameters.AddWithValue("$createdAt", now.ToString("O"));
        command.Parameters.AddWithValue("$updatedAt", now.ToString("O"));

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Die Liste konnte nicht angelegt werden.");
        }

        return MapList(reader);
    }

    /// <summary>
    /// Benennt eine vorhandene Aufgabenliste um.
    /// </summary>
    public void Rename(long listId, string newName)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE task_lists
            SET name = $name,
                updated_at = $updatedAt
            WHERE id = $id;
            """;
        command.Parameters.AddWithValue("$name", newName.Trim());
        command.Parameters.AddWithValue("$updatedAt", DateTime.UtcNow.ToString("O"));
        command.Parameters.AddWithValue("$id", listId);
        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Löscht eine Aufgabenliste. Die fachliche Prüfung erfolgt im Service.
    /// </summary>
    public void Delete(long listId)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM task_lists WHERE id = $id;";
        command.Parameters.AddWithValue("$id", listId);
        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Gibt die Anzahl der vorhandenen Aufgabenlisten zurück.
    /// </summary>
    public long Count()
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM task_lists;";
        return Convert.ToInt64(command.ExecuteScalar());
    }

    private static int GetNextSortOrder(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COALESCE(MAX(sort_order), 0) + 10 FROM task_lists;";
        return Convert.ToInt32(command.ExecuteScalar());
    }

    private static TaskList MapList(SqliteDataReader reader)
    {
        return new TaskList
        {
            Id = reader.GetInt64(0),
            Name = reader.GetString(1),
            SortOrder = reader.GetInt32(2),
            CreatedAt = DateTime.Parse(reader.GetString(3), null, System.Globalization.DateTimeStyles.RoundtripKind),
            UpdatedAt = DateTime.Parse(reader.GetString(4), null, System.Globalization.DateTimeStyles.RoundtripKind)
        };
    }
}
