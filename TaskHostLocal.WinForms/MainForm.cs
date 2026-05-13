using TaskHostLocal.WinForms.Forms;
using TaskHostLocal.WinForms.Models;
using TaskHostLocal.WinForms.Services;

namespace TaskHostLocal.WinForms;

/// <summary>
/// Hauptfenster der lokalen Aufgabenverwaltung.
/// </summary>
public sealed class MainForm : Form
{
    private readonly ListService _listService;
    private readonly TaskService _taskService;
    private readonly BackupService _backupService;

    private readonly ListBox _lstLists = new();
    private readonly DataGridView _gridTasks = new();
    private readonly TextBox _txtSearch = new();
    private readonly Label _lblStatus = new();

    /// <summary>
    /// Erstellt das Hauptfenster.
    /// </summary>
    public MainForm(ListService listService, TaskService taskService, BackupService backupService)
    {
        _listService = listService;
        _taskService = taskService;
        _backupService = backupService;

        Text = "TaskHost Local";
        StartPosition = FormStartPosition.CenterScreen;
        Width = 1100;
        Height = 700;
        MinimumSize = new Size(850, 500);

        BuildUi();
        LoadLists();
    }

    private void BuildUi()
    {
        var menu = BuildMenu();
        menu.Dock = DockStyle.Top;
        MainMenuStrip = menu;

        _lblStatus.Dock = DockStyle.Bottom;
        _lblStatus.Height = 24;
        _lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        _lblStatus.Text = "Bereit";

        var split = new SplitContainer
        {
            Dock = DockStyle.Fill,
            SplitterDistance = 260,
            FixedPanel = FixedPanel.Panel1
        };

        // Reihenfolge bewusst einfach: Menu oben, Status unten, SplitContainer füllt den Rest.
        Controls.Add(split);
        Controls.Add(_lblStatus);
        Controls.Add(menu);

        BuildListPanel(split.Panel1);
        BuildTaskPanel(split.Panel2);
    }

    private MenuStrip BuildMenu()
    {
        var menu = new MenuStrip();

        var fileMenu = new ToolStripMenuItem("Datei");
        fileMenu.DropDownItems.Add("Datenbank sichern...", null, (_, _) => BackupDatabase());
        fileMenu.DropDownItems.Add(new ToolStripSeparator());
        fileMenu.DropDownItems.Add("Beenden", null, (_, _) => Close());

        var listMenu = new ToolStripMenuItem("Listen");
        listMenu.DropDownItems.Add("Neue Liste", null, (_, _) => AddList());
        listMenu.DropDownItems.Add("Liste umbenennen", null, (_, _) => RenameList());
        listMenu.DropDownItems.Add("Liste löschen", null, (_, _) => DeleteList());

        var taskMenu = new ToolStripMenuItem("Aufgaben");
        taskMenu.DropDownItems.Add("Neue Aufgabe", null, (_, _) => AddTask());
        taskMenu.DropDownItems.Add("Aufgabe bearbeiten", null, (_, _) => EditTask());
        taskMenu.DropDownItems.Add("Aufgabe löschen", null, (_, _) => DeleteTask());
        taskMenu.DropDownItems.Add("Erledigt / offen", null, (_, _) => ToggleTaskCompleted());

        var helpMenu = new ToolStripMenuItem("Hilfe");
        helpMenu.DropDownItems.Add("Über TaskHost Local", null, (_, _) => ShowAbout());

        menu.Items.AddRange(new ToolStripItem[] { fileMenu, listMenu, taskMenu, helpMenu });
        return menu;
    }

    private void BuildListPanel(Control parent)
    {
        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
        parent.Controls.Add(panel);

        var label = new Label
        {
            Text = "Listen",
            Dock = DockStyle.Top,
            Height = 28,
            Font = new Font(Font, FontStyle.Bold)
        };
        panel.Controls.Add(label);

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 82,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true
        };
        panel.Controls.Add(buttonPanel);

        buttonPanel.Controls.Add(CreateButton("+ Liste", AddList));
        buttonPanel.Controls.Add(CreateButton("Umbenennen", RenameList));
        buttonPanel.Controls.Add(CreateButton("Löschen", DeleteList));

        _lstLists.Dock = DockStyle.Fill;
        _lstLists.DisplayMember = nameof(TaskList.Name);
        _lstLists.SelectedIndexChanged += (_, _) => LoadTasksForCurrentSelection();
        panel.Controls.Add(_lstLists);
        _lstLists.BringToFront();
    }

    private void BuildTaskPanel(Control parent)
    {
        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
        parent.Controls.Add(panel);

        var searchPanel = new Panel { Dock = DockStyle.Top, Height = 38 };
        panel.Controls.Add(searchPanel);

        var searchLabel = new Label
        {
            Text = "Suche:",
            Left = 0,
            Top = 9,
            Width = 55
        };
        searchPanel.Controls.Add(searchLabel);

        _txtSearch.Left = 60;
        _txtSearch.Top = 5;
        _txtSearch.Width = 320;
        _txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        _txtSearch.TextChanged += (_, _) => LoadTasksForCurrentSelection();
        searchPanel.Controls.Add(_txtSearch);

        var clearSearchButton = new Button
        {
            Text = "X",
            Width = 32,
            Height = 25,
            Left = 390,
            Top = 4,
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
        clearSearchButton.Click += (_, _) => _txtSearch.Clear();
        searchPanel.Controls.Add(clearSearchButton);

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 48,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };
        panel.Controls.Add(buttonPanel);

        buttonPanel.Controls.Add(CreateButton("+ Aufgabe", AddTask));
        buttonPanel.Controls.Add(CreateButton("Bearbeiten", EditTask));
        buttonPanel.Controls.Add(CreateButton("Löschen", DeleteTask));
        buttonPanel.Controls.Add(CreateButton("Erledigt/offen", ToggleTaskCompleted));

        _gridTasks.Dock = DockStyle.Fill;
        _gridTasks.AllowUserToAddRows = false;
        _gridTasks.AllowUserToDeleteRows = false;
        _gridTasks.AllowUserToResizeRows = false;
        _gridTasks.AutoGenerateColumns = false;
        _gridTasks.MultiSelect = false;
        _gridTasks.ReadOnly = true;
        _gridTasks.RowHeadersVisible = false;
        _gridTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridTasks.DoubleClick += (_, _) => EditTask();

        ConfigureTaskGridColumns();

        panel.Controls.Add(_gridTasks);
        _gridTasks.BringToFront();
    }

    private void ConfigureTaskGridColumns()
    {
        _gridTasks.Columns.Clear();

        _gridTasks.Columns.Add(new DataGridViewCheckBoxColumn
        {
            HeaderText = "✓",
            DataPropertyName = nameof(TaskItem.IsCompleted),
            Width = 35
        });

        _gridTasks.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Titel",
            DataPropertyName = nameof(TaskItem.Title),
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        _gridTasks.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Fällig",
            DataPropertyName = nameof(TaskItem.DueDateDisplay),
            Width = 100
        });

        _gridTasks.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Prio",
            DataPropertyName = nameof(TaskItem.Priority),
            Width = 55
        });

        _gridTasks.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Geändert",
            DataPropertyName = nameof(TaskItem.UpdatedAtDisplay),
            Width = 145
        });
    }

    private static Button CreateButton(string text, Action action)
    {
        var button = new Button
        {
            Text = text,
            Width = 110,
            Height = 30,
            Margin = new Padding(3)
        };
        button.Click += (_, _) => action();
        return button;
    }

    private TaskList? SelectedList => _lstLists.SelectedItem as TaskList;

    private TaskItem? SelectedTask => _gridTasks.CurrentRow?.DataBoundItem as TaskItem;

    private void LoadLists(long? preferredListId = null)
    {
        try
        {
            var lists = _listService.GetAllLists();
            _lstLists.DataSource = null;
            _lstLists.DataSource = lists;

            if (lists.Count == 0)
            {
                _gridTasks.DataSource = null;
                return;
            }

            var index = preferredListId.HasValue
                ? lists.FindIndex(list => list.Id == preferredListId.Value)
                : 0;

            _lstLists.SelectedIndex = index >= 0 ? index : 0;
        }
        catch (Exception ex)
        {
            ShowError("Die Listen konnten nicht geladen werden.", ex);
        }
    }

    private void LoadTasksForCurrentSelection()
    {
        try
        {
            var searchText = _txtSearch.Text.Trim();
            List<TaskItem> tasks;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                tasks = _taskService.SearchTasks(searchText);
                _lblStatus.Text = $"Suchergebnis: {tasks.Count} Aufgabe(n)";
            }
            else if (SelectedList is TaskList selectedList)
            {
                tasks = _taskService.GetTasksForList(selectedList);
                _lblStatus.Text = $"Liste: {selectedList.Name} — {tasks.Count} Aufgabe(n)";
            }
            else
            {
                tasks = new List<TaskItem>();
                _lblStatus.Text = "Keine Liste ausgewählt";
            }

            _gridTasks.DataSource = null;
            _gridTasks.DataSource = tasks;
        }
        catch (Exception ex)
        {
            ShowError("Die Aufgaben konnten nicht geladen werden.", ex);
        }
    }

    private void AddList()
    {
        using var form = new ListEditForm("Neue Liste");
        if (form.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            var list = _listService.CreateList(form.ListName);
            LoadLists(list.Id);
        }
        catch (Exception ex)
        {
            ShowError("Die Liste konnte nicht angelegt werden.", ex);
        }
    }

    private void RenameList()
    {
        if (SelectedList is not TaskList list)
        {
            return;
        }

        using var form = new ListEditForm("Liste umbenennen", list.Name);
        if (form.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            _listService.RenameList(list, form.ListName);
            LoadLists(list.Id);
        }
        catch (Exception ex)
        {
            ShowError("Die Liste konnte nicht umbenannt werden.", ex);
        }
    }

    private void DeleteList()
    {
        if (SelectedList is not TaskList list)
        {
            return;
        }

        var result = MessageBox.Show(
            this,
            $"Liste '{list.Name}' wirklich löschen?\n\nHinweis: In V1 können nur leere Listen gelöscht werden.",
            "Liste löschen",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _listService.DeleteList(list);
            LoadLists();
        }
        catch (Exception ex)
        {
            ShowError("Die Liste konnte nicht gelöscht werden.", ex);
        }
    }

    private void AddTask()
    {
        if (SelectedList is not TaskList list)
        {
            MessageBox.Show(this, "Bitte zuerst eine Liste auswählen.", "Keine Liste", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var task = new TaskItem
        {
            ListId = list.Id,
            Priority = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        using var form = new TaskEditForm(task);
        if (form.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            _taskService.CreateTask(form.Task);
            LoadTasksForCurrentSelection();
        }
        catch (Exception ex)
        {
            ShowError("Die Aufgabe konnte nicht angelegt werden.", ex);
        }
    }

    private void EditTask()
    {
        if (SelectedTask is not TaskItem task)
        {
            return;
        }

        // Kopie bearbeiten, damit ein Abbrechen im Dialog nicht versehentlich die Tabellenanzeige verändert.
        var editableTask = new TaskItem
        {
            Id = task.Id,
            ListId = task.ListId,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Priority = task.Priority,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt,
            CompletedAt = task.CompletedAt
        };

        using var form = new TaskEditForm(editableTask);
        if (form.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            _taskService.UpdateTask(form.Task);
            LoadTasksForCurrentSelection();
        }
        catch (Exception ex)
        {
            ShowError("Die Aufgabe konnte nicht gespeichert werden.", ex);
        }
    }

    private void DeleteTask()
    {
        if (SelectedTask is not TaskItem task)
        {
            return;
        }

        var result = MessageBox.Show(
            this,
            $"Aufgabe '{task.Title}' wirklich löschen?",
            "Aufgabe löschen",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _taskService.DeleteTask(task);
            LoadTasksForCurrentSelection();
        }
        catch (Exception ex)
        {
            ShowError("Die Aufgabe konnte nicht gelöscht werden.", ex);
        }
    }

    private void ToggleTaskCompleted()
    {
        if (SelectedTask is not TaskItem task)
        {
            return;
        }

        try
        {
            _taskService.ToggleCompletion(task);
            LoadTasksForCurrentSelection();
        }
        catch (Exception ex)
        {
            ShowError("Der Erledigt-Status konnte nicht geändert werden.", ex);
        }
    }

    private void BackupDatabase()
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Zielordner für die Datenbanksicherung auswählen"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            var backupFile = _backupService.CreateBackup(dialog.SelectedPath);
            MessageBox.Show(this, $"Backup erstellt:\n{backupFile}", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            ShowError("Das Backup konnte nicht erstellt werden.", ex);
        }
    }

    private void ShowAbout()
    {
        MessageBox.Show(
            this,
            "TaskHost Local v0.1.0\n\nLokale Aufgabenverwaltung mit Windows Forms und SQLite.",
            "Über TaskHost Local",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void ShowError(string message, Exception exception)
    {
        MessageBox.Show(
            this,
            $"{message}\n\nDetails:\n{exception.Message}",
            "Fehler",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}
