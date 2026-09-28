namespace DemoApp.Icons;

/// <summary>
/// Material Symbols: every name, in the drawing the selector picks.
/// </summary>
internal sealed class MaterialView : IconsDemoView, IUIViewDefinition
{
    /// <summary>Ids of the fields the gallery's rules name; bound, since a windowed host resolves them server-side.</summary>
    private const string SearchId = "icons-search";
    private const string StyleId = "icons-style";

    public static string ViewKey => "icons.material";

    protected override string Route => MaterialRoute;

    public override string Title => "Material Symbols";

    protected override string Description
        => "Every name in the set, read off the package itself. A tile's caption is the constant an author writes; hover the glyph for the value it resolves to.";

    /// <summary>The controls above the gallery, then the gallery filling what is left.</summary>
    protected override IVisualComponent CreateContent()
        => new ContainerComponent()
            .SetPadding(UIThickness.All(24, 4, 24, 24))
            .SetHeight(UILayoutLength.Fill())
            .SetRow(1, UIGridUnit.Auto())
            .AddRow(UIGridUnit.Star())
            .AddChild(CreateControls().SetPlacement(1, 1, 24, 1))
            .AddChild(CreateGallery().SetPlacement(1, 2, 24, 1));

    /// <summary>The search box, the drawing selector, the clear button and the count.</summary>
    private static StackPanelComponent CreateControls()
        => new StackPanelComponent()
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
            )
            .AddChild(new SelectComponent(StyleId)
                .SetOptions([
                    new OptionItem { Id = IconsCatalog.FilledStyle, Title = "Filled" },
                    new OptionItem { Id = IconsCatalog.OutlinedStyle, Title = "Outlined" }
                ])
                .BindValue(nameof(IconsController.Style))
                .SetWidth(UILayoutLength.Absolute(160))
            )
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

    /// <summary>The windowed gallery: a hundred tiles at a time, the search and the drawing resolved on the server.</summary>
    private static ItemsViewComponent CreateGallery()
        => new ItemsViewComponent()
            .BindSource(nameof(IconsController.Icons))
            .SetWindowSize(IconsController.WindowSize)
            .SetLayoutType(UIItemsLayoutType.Wrap)
            .FilterBy(SearchId, IInputComponent.ValueProperty, nameof(IconItem.Name))
            .VerticalScrollOnly()
            .SetSpacing(12)
            .SetHeight(UILayoutLength.Fill())
            .SetTemplate(CreateTile())
            .ConfigureDefaultEmptyTemplate(empty => empty.SetTitle("No name matches."))
            .FilterBy(StyleId, IInputComponent.ValueProperty, nameof(IconItem.Style), UIComparisonOperator.Equal);

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
