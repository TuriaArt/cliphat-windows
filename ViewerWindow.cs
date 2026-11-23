using System.Diagnostics;

namespace ClipHat;

/// A window that shows an item's content for reading: the full copied text
/// with scroll bars, an image in a zoomed view, or a file's details with an
/// Open button. The theme and DPI follow the rest of the app.
internal sealed class ViewerWindow : Form
{
    private readonly Button copy;

    /// Text viewer: shows the whole copied text, scrollable, no wrapping so
    /// long lines are readable without hidden content.
    public ViewerWindow(string title, string text)
    {
        Setup(title);
        var box = new TextBox
        {
            Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, WordWrap = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Font = new Font("Segoe UI", 11), BackColor = Theme.Input, ForeColor = Theme.Ink,
            Margin = new Padding(10, 10, 10, 48)
        };
        box.Text = text;
        copy.Text = Strings.Current.CopyToClipboard;
        copy.Click += (_, _) => Clipboard.SetText(box.Text);
        AddContent(box);
    }

    /// Image viewer: shows the image enlarged to fit the window, preserving
    /// its proportions, and opens it in the default app on demand.
    public ViewerWindow(string title, string imagePath)
    {
        Setup(title);
        var picture = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.Zoom, Dock = DockStyle.Fill,
            BackColor = Theme.Surround, Margin = new Padding(10)
        };
        picture.Image = Image.FromFile(imagePath);
        copy.Text = Strings.Current.Open;
        copy.Click += (_, _) => Process.Start(new ProcessStartInfo(imagePath) { UseShellExecute = true });
        AddContent(picture);
    }

    /// File viewer: shows the file's name, type and size in large text, and
    /// opens it in the default application.
    public ViewerWindow(string title, string filePath, string kind)
    {
        Setup(title);
        var info = new Label
        {
            Dock = DockStyle.Fill, Margin = new Padding(16), AutoSize = false,
            TextAlignment = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11),
            BackColor = Theme.Input, ForeColor = Theme.Ink
        };
        var fi = new FileInfo(filePath);
        var type = kind == "audio" ? Strings.Current.Audio : Strings.Current.Documents;
        info.Text = $"{Path.GetFileName(filePath)}\r\n\r\n{type}\r\n{fi.Length:N0} bytes\r\n\r\n{fi.FullName}";
        copy.Text = Strings.Current.Open;
        copy.Click += (_, _) => Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        AddContent(info);
    }

    private void AddContent(Control content)
    {
        // The copy button docks to the bottom strip; content fills or
        // anchors within the space left above it.
        Controls.Add(content);
        Controls.Add(copy);
    }

    private void Setup(string title)
    {
        Text = title;
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Theme.Surround;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(720, 560);
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        copy = new Button
        {
            Width = 120, Height = 32, FlatStyle = FlatStyle.Flat,
            BackColor = Theme.Card, ForeColor = Theme.Ink, Dock = DockStyle.Bottom,
            Margin = new Padding(0, 0, 10, 10)
        };
        copy.FlatAppearance.BorderColor = Theme.CardBorder;
    }
}
