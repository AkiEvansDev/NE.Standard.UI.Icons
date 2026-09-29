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

    public override string Title => "icons-demo.material.title";

    protected override string Description => "icons-demo.material.description";

    /// <summary>The controls above the gallery, then the gallery filling what is left.</summary>
    protected override IVisualComponent CreateContent()
        => new ContainerComponent()
            .SetPadding(UIThickness.All(24, 4, 24, 24))
            .SetHeight(UILayoutLength.Fill())
            .SetRow(1, UIGridUnit.Auto())
            .AddRow(UIGridUnit.Star())
            .AddChild(CreateControls().SetPlacement(1, 1, 24, 1))
            .AddChild(CreateGallery().SetPlacement(1, 2, 24, 1));

    /// <summary>The search box, the drawing selector and the count, wrapping onto a second line where the three do not fit.</summary>
    private static StackPanelComponent CreateControls()
        => new StackPanelComponent()
            .SetOrientation(UIOrientation.Horizontal)
            .SetSpacing(16)
            .SetWrap(true)
            .SetVerticalAlignment(UIAlignment.Center)
            .SetMargin(UIThickness.All(0, 0, 0, 12))
            .AddChild(new TextInputComponent(SearchId)
                .SetPlaceholder("icons-demo.search.placeholder")
                .SetPrefixIcon(MaterialIcons.Search)
                .SetShowClearButton()
                .BindValue(nameof(IconsController.Search))
                .SetDebounceMilliseconds(250)
                .SetWidth(UILayoutLength.Absolute(320))
            )
            .AddChild(new SelectComponent(StyleId)
                .SetOptions([
                    new OptionItem { Id = IconsCatalog.FilledStyle, Title = "icons-demo.style.filled" },
                    new OptionItem { Id = IconsCatalog.OutlinedStyle, Title = "icons-demo.style.outlined" }
                ])
                .BindValue(nameof(IconsController.Style))
                .SetWidth(UILayoutLength.Absolute(160))
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
            .ConfigureDefaultEmptyTemplate(empty => empty.SetTitle("icons-demo.empty"))
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
                    .SetSize(UIIconSize.Large)
                )
                // Paragraphs, which wrap: a name or a value cut behind an ellipsis could not be read whole, and it is what an author copies.
                .AddChild(new ParagraphComponent()
                    .BindDescription(nameof(IconItem.Name), UIBindingScope.Relative)
                    .SetDescriptionType(UITextAppearance.Caption)
                    .SetTextAlignment(UITextAlignment.Center)
                )
                // Written out rather than in a tooltip, so the value is there for a keyboard, a touch and a reader too.
                .AddChild(new ParagraphComponent()
                    .BindDescription(nameof(IconItem.Glyph), UIBindingScope.Relative)
                    .SetDescriptionType(UITextAppearance.Caption)
                    .SetDescriptionColor(UIThemeColor.FromStyle(UIColorStyle.Muted))
                    .SetTextAlignment(UITextAlignment.Center)
                )
            )
            // Six to a line on a wide screen rather than eight: at an eighth a name's caption broke mid-word, and it cannot break at its
            // case boundaries without a character that would travel with the name when an author copies it.
            .SetPlacement(1, 1, 12, 1, sm: UIGridPlacement.At(1, 1, 6, 1), xl: UIGridPlacement.At(1, 1, 4, 1));
}
