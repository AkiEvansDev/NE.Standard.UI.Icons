using NE.Standard.UI.Abstractions.Styling;
using NE.Standard.UI.Authoring.BuiltIns;
using NE.Standard.UI.Authoring.Components;
using NE.Standard.UI.Authoring.Views;
using NE.Standard.UI.Components.BuiltIns.Actions;
using NE.Standard.UI.Components.BuiltIns.Contents;
using NE.Standard.UI.Components.BuiltIns.Inputs;
using NE.Standard.UI.Components.BuiltIns.Items;
using NE.Standard.UI.Components.BuiltIns.Layouts;
using NE.Standard.UI.Components.BuiltIns.Models;
using NE.Standard.UI.Components.BuiltIns.Navigation;
using NE.Standard.UI.Extensions;
using NE.Standard.UI.Icons.Material;
using NE.Standard.UI.Primitives.Binding;
using NE.Standard.UI.Primitives.Styling;

namespace DemoApp.Icons;

/// <summary>
/// What every page of the demo wears: the title band with the theme switcher, the sidebar naming the sets, and the gallery filling
/// what is left. One page per set, since a set is what a reader comes to look through.
/// </summary>
public abstract class IconsDemoView : UIViewBase
{
    public const string MaterialRoute = "/";
    public const string LucideRoute = "/lucide";

    /// <summary>Ids of the fields the gallery's rules name; bound, since a windowed host resolves them server-side.</summary>
    protected const string SearchId = "icons-search";
    protected const string StyleId = "icons-style";

    /// <summary>The sidebar's authored id, stable across renders.</summary>
    private const string SidebarId = "icons-sidebar";

    private static readonly (string Route, string Label)[] Pages =
    [
        (MaterialRoute, "Material Symbols"),
        (LucideRoute, "Lucide")
    ];

    /// <summary>The title band and the sidebar stand; the content scrolls by itself.</summary>
    public override UIViewOptions Options { get; } = new() { StickyHeader = true, ScrollContentOnly = true };

    protected abstract string Route { get; }
    public abstract override string Title { get; }
    protected abstract string Description { get; }

    /// <summary>The page band from the preset; the theme switcher wears Material's outlined pair, which the gallery registers whole.</summary>
    protected override IVisualComponent? CreateHeader()
        => UIPage.Header(Title, Description, new ThemeSwitcherComponent()
            .SetLightIcon(MaterialIcons.Outlined(MaterialIcons.LightMode))
            .SetDarkIcon(MaterialIcons.Outlined(MaterialIcons.DarkMode))
        );

    /// <summary>The sidebar every page wears: one entry per set, the one being read marked.</summary>
    protected override IVisualComponent? CreateLeftSide()
    {
        MenuItem[] entries = new MenuItem[Pages.Length];

        for (var i = 0; i < Pages.Length; i++)
            entries[i] = new MenuItem { Id = Pages[i].Route, Title = Pages[i].Label, Url = Pages[i].Route, Selected = Pages[i].Route == Route };

        // The width sits on the menu, not the container.
        return new ContainerComponent()
            .SetHorizontalAlignment(UIAlignment.Start)
            .SetPadding(UIThickness.All(16, 0, 16, 24))
            .AddChild(new MenuComponent(SidebarId)
                .SetMinWidth(UILayoutLength.Absolute(180))
                .SetItems(entries)
            );
    }

    /// <summary>The controls above the gallery, then the gallery filling what is left.</summary>
    protected override IVisualComponent CreateContent()
        => new ContainerComponent()
            .SetPadding(UIThickness.All(24, 4, 24, 24))
            .SetHeight(UILayoutLength.Fill())
            .SetRow(1, UIGridUnit.Auto())
            .AddRow(UIGridUnit.Star())
            .AddChild(CreateControls().SetPlacement(1, 1, 24, 1))
            .AddChild(CreateGallery().SetPlacement(1, 2, 24, 1));

    /// <summary>The set's own control between the search box and the clear button — Material's drawing selector; nothing for Lucide.</summary>
    protected abstract IVisualComponent? CreateSetControl();

    /// <summary>The rule the set's own control adds to the gallery's, if it has one.</summary>
    protected abstract ItemsViewComponent FilterBySet(ItemsViewComponent gallery);

    private StackPanelComponent CreateControls()
    {
        StackPanelComponent controls = new StackPanelComponent()
            .SetOrientation(UIOrientation.Horizontal)
            .SetSpacing(16)
            .SetVerticalAlignment(UIAlignment.Center)
            .SetMargin(UIThickness.All(0, 0, 0, 12))
            .AddChild(new TextInputComponent(SearchId)
                .SetPlaceholder("Search by name or glyph…")
                .SetPrefixIcon(MaterialIcons.Search)
                .BindValue(nameof(IconsController.Search))
                .SetDebounceMilliseconds(250)
                .SetWidth(UILayoutLength.Absolute(320))
            );

        if (CreateSetControl() is IVisualComponent own)
            _ = controls.AddChild(own);

        return controls
            .AddChild(new ButtonComponent()
                .SetTitle("Clear")
                .SetType(UIButtonType.Ghost)
                .OnClick(nameof(IconsController.ClearSearch))
                .SetVerticalAlignment(UIAlignment.Center)
            )
            .AddChild(new TextComponent()
                .BindTitle($"{nameof(IconsController.Icons)}.{nameof(IconsSource.Caption)}")
                .SetTitleType(UITextAppearance.Caption)
                .SetTitleColor(UIThemeColor.FromStyle(UIColorStyle.Muted))
                .SetVerticalAlignment(UIAlignment.Center)
            );
    }

    /// <summary>The windowed gallery: a hundred tiles at a time, the search resolved on the server.</summary>
    private ItemsViewComponent CreateGallery()
        => FilterBySet(new ItemsViewComponent()
            .BindSource(nameof(IconsController.Icons))
            .SetWindowSize(IconsController.WindowSize)
            .SetLayoutType(UIItemsLayoutType.Wrap)
            .FilterBy(SearchId, IInputComponent.ValueProperty, nameof(IconItem.Name))
            .VerticalScrollOnly()
            .SetSpacing(12)
            .SetHeight(UILayoutLength.Fill())
            .SetTemplate(CreateTile())
            .ConfigureDefaultEmptyTemplate(empty => empty.SetTitle("No name matches."))
        );

    private static ContainerComponent CreateTile()
        => new ContainerComponent()
            .SetPadding(UIThickness.Uniform(12))
            .SetBackground(UIThemeColor.Surface)
            .SetBorderColor(UIThemeColor.Border)
            .SetBorderThickness(UIThickness.Uniform(1))
            .SetBorderRadius(UICornerRadius.Uniform(8))
            .AddRow(UIGridUnit.Auto())
            .AddChild(new StackPanelComponent()
                .SetOrientation(UIOrientation.Vertical)
                .SetHorizontalAlignment(UIAlignment.Center)
                .SetSpacing(8)
                .SetPlacement(1, 1, 24, 1)
                .AddChild(new IconComponent()
                    .BindIcon(nameof(IconItem.Glyph), UIBindingScope.Relative)
                    .BindTooltip(nameof(IconItem.Glyph), UIBindingScope.Relative)
                    .SetSize(UIIconSize.Large)
                )
                .AddChild(new TextComponent()
                    .BindTitle(nameof(IconItem.Name), UIBindingScope.Relative)
                    .SetTitleType(UITextAppearance.Caption)
                    .SetHorizontalAlignment(UIAlignment.Center)
                )
            )
            .SetPlacement(1, 1, 3, 1);
}
