namespace ClipHat;

/// The color scheme for WinForms menus (the tray menu and the cards' right-click
/// menu), drawn dark in the dark theme and light otherwise, with the accent for
/// the selected and pressed items.
internal sealed class ThemeColorTable : ProfessionalColorTable
{
    private static bool Dark => Theme.Mode == ThemeMode.Dark;
    private static Color Tint(Color color, int alpha) => Theme.WithAlpha(color, alpha);

    public override Color MenuStripGradientBegin => Theme.Card;
    public override Color MenuStripGradientEnd => Theme.Card;
    public override Color ToolStripDropDownBackground => Theme.Card;
    public override Color MenuBorder => Theme.CardBorder;
    public override Color MenuItemBorder => Theme.CardBorder;
    public override Color MenuItemSelected => Tint(Theme.Accent, Dark ? 80 : 48);
    public override Color MenuItemSelectedGradientBegin => Tint(Theme.Accent, Dark ? 80 : 48);
    public override Color MenuItemSelectedGradientEnd => Tint(Theme.Accent, Dark ? 80 : 48);
    public override Color MenuItemPressedGradientBegin => Tint(Theme.Accent, Dark ? 128 : 96);
    public override Color MenuItemPressedGradientMiddle => Tint(Theme.Accent, Dark ? 128 : 96);
    public override Color MenuItemPressedGradientEnd => Tint(Theme.Accent, Dark ? 128 : 96);
    public override Color ImageMarginGradientBegin => Theme.Card;
    public override Color ImageMarginGradientMiddle => Theme.Card;
    public override Color ImageMarginGradientEnd => Theme.Card;
    public override Color SeparatorDark => Theme.CardBorder;
    public override Color SeparatorLight => Theme.Divider;
}

/// The renderer for the app's menus. The color table paints the backgrounds;
/// this also keeps the item text readable in the current theme.
internal sealed class ThemeRenderer : ToolStripProfessionalRenderer
{
    public ThemeRenderer() : base(new ThemeColorTable()) { }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        e.Item.ForeColor = Theme.Ink;
        base.OnRenderItemText(e);
    }
}
