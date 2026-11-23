namespace ClipHat;

/// A simple window that shows an item's text content for reading, with a
/// copy button. The theme and DPI follow the rest of the app.
internal sealed class ViewerWindow : Form
{
    private readonly TextBox box;
    public ViewerWindow(string title, string text)
    {
        Text = title;
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Theme.Surround;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(640, 480);
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        box = new TextBox
        {
            Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, WordWrap = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, Font = new Font("Segoe UI", 10),
            BackColor = Theme.Input, ForeColor = Theme.Ink, Margin = new Padding(10, 10, 10, 44)
        };
        box.Text = text;
        var copy = new Button
        {
            Text = Strings.Current.CopyToClipboard, Width = 110, Height = 30, FlatStyle = FlatStyle.Flat,
            BackColor = Theme.Card, ForeColor = Theme.Ink, Dock = DockStyle.Bottom, Margin = new Padding(0, 0, 10, 10)
        };
        copy.FlatAppearance.BorderColor = Theme.CardBorder;
        copy.Click += (_, _) => { Clipboard.SetText(box.Text); };
        Controls.Add(box);
        Controls.Add(copy);
    }
}
