using TaskHostLocal.WinForms.Models;

namespace TaskHostLocal.WinForms.Forms;

/// <summary>
/// Dialog zum Anlegen oder Bearbeiten einer Aufgabe.
/// </summary>
public sealed class TaskEditForm : Form
{
    private readonly TextBox _txtTitle = new();
    private readonly TextBox _txtDescription = new();
    private readonly CheckBox _chkHasDueDate = new();
    private readonly DateTimePicker _dtpDueDate = new();
    private readonly NumericUpDown _numPriority = new();
    private readonly CheckBox _chkCompleted = new();

    private readonly TaskItem _task;

    /// <summary>
    /// Die bearbeitete Aufgabe. Nach OK enthält sie die neuen Werte.
    /// </summary>
    public TaskItem Task => _task;

    /// <summary>
    /// Erstellt einen Dialog für eine vorhandene oder neue Aufgabe.
    /// </summary>
    public TaskEditForm(TaskItem task)
    {
        _task = task ?? throw new ArgumentNullException(nameof(task));

        Text = task.Id == 0 ? "Neue Aufgabe" : "Aufgabe bearbeiten";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Width = 620;
        Height = 430;

        BuildUi();
        LoadTaskIntoControls();
    }

    private void BuildUi()
    {
        var lblTitle = new Label { Text = "Titel:", Left = 12, Top = 18, Width = 100 };
        _txtTitle.Left = 120;
        _txtTitle.Top = 14;
        _txtTitle.Width = 460;

        var lblDescription = new Label { Text = "Notiz:", Left = 12, Top = 55, Width = 100 };
        _txtDescription.Left = 120;
        _txtDescription.Top = 52;
        _txtDescription.Width = 460;
        _txtDescription.Height = 150;
        _txtDescription.Multiline = true;
        _txtDescription.ScrollBars = ScrollBars.Vertical;

        _chkHasDueDate.Text = "Fällig am:";
        _chkHasDueDate.Left = 120;
        _chkHasDueDate.Top = 220;
        _chkHasDueDate.Width = 100;
        _chkHasDueDate.CheckedChanged += (_, _) => _dtpDueDate.Enabled = _chkHasDueDate.Checked;

        _dtpDueDate.Left = 225;
        _dtpDueDate.Top = 216;
        _dtpDueDate.Width = 150;
        _dtpDueDate.Format = DateTimePickerFormat.Custom;
        _dtpDueDate.CustomFormat = "yyyy-MM-dd";

        var lblPriority = new Label { Text = "Priorität:", Left = 12, Top = 260, Width = 100 };
        _numPriority.Left = 120;
        _numPriority.Top = 256;
        _numPriority.Width = 80;
        _numPriority.Minimum = 0;
        _numPriority.Maximum = 5;

        _chkCompleted.Text = "Erledigt";
        _chkCompleted.Left = 120;
        _chkCompleted.Top = 295;
        _chkCompleted.Width = 120;

        var btnOk = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Left = 400,
            Top = 335,
            Width = 85
        };
        btnOk.Click += (_, _) => SaveControlsIntoTask();

        var btnCancel = new Button
        {
            Text = "Abbrechen",
            DialogResult = DialogResult.Cancel,
            Left = 495,
            Top = 335,
            Width = 85
        };

        Controls.AddRange(new Control[]
        {
            lblTitle,
            _txtTitle,
            lblDescription,
            _txtDescription,
            _chkHasDueDate,
            _dtpDueDate,
            lblPriority,
            _numPriority,
            _chkCompleted,
            btnOk,
            btnCancel
        });

        AcceptButton = btnOk;
        CancelButton = btnCancel;
    }

    private void LoadTaskIntoControls()
    {
        _txtTitle.Text = _task.Title;
        _txtDescription.Text = _task.Description ?? string.Empty;

        _chkHasDueDate.Checked = _task.DueDate.HasValue;
        _dtpDueDate.Value = _task.DueDate ?? DateTime.Today;
        _dtpDueDate.Enabled = _chkHasDueDate.Checked;

        _numPriority.Value = Math.Clamp(_task.Priority, 0, 5);
        _chkCompleted.Checked = _task.IsCompleted;
    }

    private void SaveControlsIntoTask()
    {
        if (string.IsNullOrWhiteSpace(_txtTitle.Text))
        {
            MessageBox.Show(this, "Bitte einen Titel eingeben.", "Eingabe fehlt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult = DialogResult.None;
            return;
        }

        _task.Title = _txtTitle.Text.Trim();
        _task.Description = string.IsNullOrWhiteSpace(_txtDescription.Text) ? null : _txtDescription.Text.Trim();
        _task.DueDate = _chkHasDueDate.Checked ? _dtpDueDate.Value.Date : null;
        _task.Priority = Convert.ToInt32(_numPriority.Value);
        _task.IsCompleted = _chkCompleted.Checked;
    }
}
