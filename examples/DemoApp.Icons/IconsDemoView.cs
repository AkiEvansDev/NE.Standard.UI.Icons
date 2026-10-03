namespace DemoApp.Icons;

/// <summary>
/// What every page of the demo wears: the title band with the language and theme switchers, the sidebar naming the pages, and the
/// page's own content filling what is left.
/// </summary>
public abstract class IconsDemoView : UIViewBase
{
    public const string MaterialRoute = "/";

    /// <summary>The sidebar's authored id, stable across renders.</summary>
    private const string SidebarId = "icons-sidebar";

    private static readonly (string Route, string Label)[] Pages =
    [
        (MaterialRoute, "icons-demo.material.title")
    ];

    /// <summary>The title band and the sidebar stand, the sidebar from the top of the page; the content scrolls by itself.</summary>
    public override UIViewOptions Options { get; } = new() { StickyHeader = true, ScrollContentOnly = true, ShellLayout = UIShellLayout.FullHeightSides };

    protected abstract string Route { get; }
    public abstract override string Title { get; }
    protected abstract string Description { get; }

    /// <summary>
    /// The page band from the preset; the language and theme switchers are on every page, since both are the framework's state. The
    /// theme switcher wears Material's outlined pair, which the gallery registers whole.
    /// </summary>
    protected override IVisualComponent? CreateHeader()
        => UIPage.Header(Title, Description,
            new LanguageSwitcherComponent(),
            new ThemeSwitcherComponent()
                .SetLightIcon(MaterialIcons.Outlined(MaterialIcons.LightMode))
                .SetDarkIcon(MaterialIcons.Outlined(MaterialIcons.DarkMode))
        );

    /// <summary>The sidebar every page wears: one entry per page, the one being read marked.</summary>
    protected override IVisualComponent? CreateLeftSide()
    {
        MenuItem[] entries = new MenuItem[Pages.Length];

        for (var i = 0; i < Pages.Length; i++)
            entries[i] = new MenuItem { Id = Pages[i].Route, Title = Pages[i].Label, Url = Pages[i].Route, Selected = Pages[i].Route == Route };

        // The width sits on the menu, not the container.
        return new ContainerComponent()
            .SetHorizontalAlignment(UIAlignment.Start)
            .SetPadding(UIThickness.All(16, 16, 16, 24))
            .AddChild(new MenuComponent(SidebarId)
                .SetShowCollapseToggle(true)
                .SetSearch()
                .SetMinWidth(UILayoutLength.Absolute(180))
                .SetItems(entries)
            );
    }
}
