using TaskHostLocal.WinForms.Models;
using TaskHostLocal.WinForms.Repositories;

namespace TaskHostLocal.WinForms.Services;

/// <summary>
/// Fachlogik für Aufgaben.
/// </summary>
public sealed class TaskService
{
    private readonly TaskRepository _taskRepository;

    /// <summary>
    /// Erstellt einen neuen Service.
    /// </summary>
    public TaskService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Gibt alle Aufgaben einer Liste zurück.
    /// </summary>
    public List<TaskItem> GetTasksForList(TaskList list)
    {
        ArgumentNullException.ThrowIfNull(list);
        return _taskRepository.GetByListId(list.Id);
    }

    /// <summary>
    /// Sucht Aufgaben über alle Listen.
    /// </summary>
    public List<TaskItem> SearchTasks(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return new List<TaskItem>();
        }

        return _taskRepository.Search(searchText);
    }

    /// <summary>
    /// Legt eine neue Aufgabe an.
    /// </summary>
    public TaskItem CreateTask(TaskItem task)
    {
        ValidateTask(task);
        return _taskRepository.Add(task);
    }

    /// <summary>
    /// Aktualisiert eine vorhandene Aufgabe.
    /// </summary>
    public void UpdateTask(TaskItem task)
    {
        ValidateTask(task);
        _taskRepository.Update(task);
    }

    /// <summary>
    /// Löscht eine Aufgabe.
    /// </summary>
    public void DeleteTask(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);
        _taskRepository.Delete(task.Id);
    }

    /// <summary>
    /// Schaltet den Erledigt-Status um.
    /// </summary>
    public void ToggleCompletion(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);
        _taskRepository.SetCompleted(task.Id, !task.IsCompleted);
    }

    private static void ValidateTask(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);

        if (task.ListId <= 0)
        {
            throw new ArgumentException("Die Aufgabe muss einer Liste zugeordnet sein.", nameof(task));
        }

        if (string.IsNullOrWhiteSpace(task.Title))
        {
            throw new ArgumentException("Der Aufgabentitel darf nicht leer sein.", nameof(task));
        }
    }
}
