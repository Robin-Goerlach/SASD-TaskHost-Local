using TaskHostLocal.WinForms.Models;
using TaskHostLocal.WinForms.Repositories;

namespace TaskHostLocal.WinForms.Services;

/// <summary>
/// Fachlogik für Aufgabenlisten.
/// </summary>
public sealed class ListService
{
    private readonly ListRepository _listRepository;
    private readonly TaskRepository _taskRepository;

    /// <summary>
    /// Erstellt einen neuen Service.
    /// </summary>
    public ListService(ListRepository listRepository, TaskRepository taskRepository)
    {
        _listRepository = listRepository;
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Gibt alle Listen zurück.
    /// </summary>
    public List<TaskList> GetAllLists() => _listRepository.GetAll();

    /// <summary>
    /// Legt eine neue Liste an.
    /// </summary>
    public TaskList CreateList(string name)
    {
        ValidateName(name);
        return _listRepository.Add(name);
    }

    /// <summary>
    /// Benennt eine Liste um.
    /// </summary>
    public void RenameList(TaskList list, string newName)
    {
        ArgumentNullException.ThrowIfNull(list);
        ValidateName(newName);
        _listRepository.Rename(list.Id, newName);
    }

    /// <summary>
    /// Löscht eine Liste, sofern dies gefahrlos möglich ist.
    /// </summary>
    public void DeleteList(TaskList list)
    {
        ArgumentNullException.ThrowIfNull(list);

        if (_listRepository.Count() <= 1)
        {
            throw new InvalidOperationException("Die letzte Liste kann nicht gelöscht werden.");
        }

        if (_taskRepository.CountByListId(list.Id) > 0)
        {
            throw new InvalidOperationException("Diese Liste enthält noch Aufgaben und kann in V1 nicht gelöscht werden.");
        }

        _listRepository.Delete(list.Id);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Der Listenname darf nicht leer sein.", nameof(name));
        }
    }
}
