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
        => PageHeader(Title, Description,
            new LanguageSwitcherComponent(),
            new ThemeSwitcherComponent()
                .SetLightIcon(MaterialIcons.Outlined(MaterialIcons.LightMode))
                .SetDarkIcon(MaterialIcons.Outlined(MaterialIcons.DarkMode))
        );

    /// <summary>
    /// The page band: the name with the switchers at the far end of its row at every width, and the muted line under them — on a phone
    /// the name a title's size and the line one line, cut, so the band stays about a title's height and leaves the screen to the page;
    /// from a medium screen the name in the display role and the line up to three lines.
    /// </summary>
    /// <remarks>
    /// <c>UIPage.Header</c>'s band, kept on one row on a phone too: there the preset folds the far end under the line, which took a
    /// third of a phone's height.
    /// </remarks>
    private static ContainerComponent PageHeader(string title, string description, params IVisualComponent[] trailing)
        => new ContainerComponent()
            .SetPadding(UIResponsive<UIThickness>.Create(UIThickness.All(24, 8, 16, 4), md: UIThickness.All(24, 20, 24, 4)))
            .SetColumn(24, UIGridUnit.Auto())
            // On a phone a name too long for its row wraps under itself rather than losing its end, its first line level with the
            // switchers, which stand at the row's top.
            .AddChild(PageTitle(title, UIResponsive<UIVisibility>.Create(UIVisibility.Visible, md: UIVisibility.Collapsed))
                .AsTitle()
                .SetTitleWrap(true)
                .SetVerticalAlignment(UIAlignment.Start)
                .SetMargin(UIThickness.All(0, PhoneTitleInset, 0, 0))
            )
            .AddChild(PageTitle(title, UIResponsive<UIVisibility>.Create(UIVisibility.Collapsed, md: UIVisibility.Visible)).AsDisplay())
            .AddChild(PageDescription(description, 1, UIResponsive<UIVisibility>.Create(UIVisibility.Visible, md: UIVisibility.Collapsed)).SetPlacement(1, 2, 24, 1))
            .AddChild(PageDescription(description, 3, UIResponsive<UIVisibility>.Create(UIVisibility.Collapsed, md: UIVisibility.Visible)).SetPlacement(1, 2, 23, 1))
            .AddChild(new StackPanelComponent()
                .SetOrientation(UIOrientation.Horizontal)
                .SetSpacing(UIResponsive<double>.Create(8, md: 12))
                .SetMargin(UIResponsive<UIThickness>.Create(UIThickness.All(8, 0, 0, 0), md: UIThickness.All(12, 0, 0, 0)))
                .SetHorizontalAlignment(UIAlignment.End)
                .SetVerticalAlignment(UIAlignment.Start)
                .AddChildren(trailing)
                .SetPlacement(24, 1, 1, 1)
            );

    // Half the switchers' 40 px less the name's 28 px line: a one-line name stands in their middle, a wrapped one's first line too.
    private const double PhoneTitleInset = 6;

    /// <summary>The page's name, where <paramref name="visibility"/> shows it.</summary>
    private static TextComponent PageTitle(string title, UIResponsive<UIVisibility> visibility)
        => new TextComponent()
            .SetTitle(title)
            .SetTitleColor(UIThemeColor.OnBackground)
            .SetVerticalAlignment(UIAlignment.Center)
            .SetVisibility(visibility)
            .SetPlacement(1, 1, 23, 1);

    /// <summary>The muted line under the name, at most <paramref name="lines"/> lines, where <paramref name="visibility"/> shows it.</summary>
    private static ParagraphComponent PageDescription(string description, int lines, UIResponsive<UIVisibility> visibility)
        => new ParagraphComponent()
            .SetDescription(description)
            .SetMaxLines(lines)
            .SetDescriptionType(UITextAppearance.Body)
            .SetDescriptionColor(UIThemeColor.Muted)
            .SetVisibility(visibility);

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
