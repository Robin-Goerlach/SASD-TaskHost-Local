namespace TaskHostLocal.WinForms.Forms;

/// <summary>
/// Kleiner Dialog zum Anlegen oder Umbenennen einer Aufgabenliste.
/// </summary>
public sealed class ListEditForm : Form
{
    private readonly TextBox _txtName = new();

    /// <summary>
    /// Der vom Benutzer eingegebene Listenname.
    /// </summary>
    public string ListName => _txtName.Text.Trim();

    /// <summary>
    /// Initialisiert den Dialog.
    /// </summary>
    public ListEditForm(string title, string? currentName = null)
    {
        Text = title;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Width = 420;
        Height = 160;

        var label = new Label
        {
            Text = "Name:",
            Left = 12,
            Top = 18,
            Width = 80
        };

        _txtName.Left = 95;
        _txtName.Top = 14;
        _txtName.Width = 290;
        _txtName.Text = currentName ?? string.Empty;

        var btnOk = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Left = 215,
            Top = 65,
            Width = 80
        };

        var btnCancel = new Button
        {
            Text = "Abbrechen",
            DialogResult = DialogResult.Cancel,
            Left = 305,
            Top = 65,
            Width = 80
        };

        Controls.AddRange(new Control[] { label, _txtName, btnOk, btnCancel });
        AcceptButton = btnOk;
        CancelButton = btnCancel;

        Shown += (_, _) => _txtName.Focus();
    }
}
