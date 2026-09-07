using NE.Standard.UI.Abstractions.Styling;
using NE.Standard.UI.Authoring.Components;
using NE.Standard.UI.Authoring.Views;
using NE.Standard.UI.Components.BuiltIns.Actions;
using NE.Standard.UI.Components.BuiltIns.Contents;
using NE.Standard.UI.Components.BuiltIns.Inputs;
using NE.Standard.UI.Components.BuiltIns.Items;
using NE.Standard.UI.Components.BuiltIns.Layouts;
using NE.Standard.UI.Components.BuiltIns.Models;
using NE.Standard.UI.Components.Foundation.Inputs;
using NE.Standard.UI.Icons.Material;
using NE.Standard.UI.Primitives.Binding;
using NE.Standard.UI.Primitives.Interaction;
using NE.Standard.UI.Primitives.Styling;

namespace DemoApp.Icons;

/// <summary>
/// Every name both sets offer, searched on the server and read a window at a time.
/// </summary>
internal sealed class IconsView : UIViewBase, IUIViewDefinition
{
    /// <summary>Ids of the three fields the host's rules name; all bound, since a windowed host resolves them server-side.</summary>
    private const string SearchId = "icons-search";
    private const string SetId = "icons-set";
    private const string StyleId = "icons-style";

    public static string ViewKey => "icons.gallery";

    public override UIViewOptions Options { get; } = new() { ScrollContentOnly = true };

    protected override IVisualComponent? CreateHeader()
        => new ContainerComponent()
            .SetPadding(UIThickness.All(24, 20, 24, 4))
            .SetRow(1, UIGridUnit.Auto())
            .AddRow(UIGridUnit.Auto())
            .AddChild(new TextComponent()
                .SetTitle("Icons")
                .SetDescription("Every name in the set, read off the package itself. A tile's caption is the constant an author writes; hover the glyph for the value it resolves to.")
                .SetPlacement(1, 1, 24, 1)
            )
            .AddChild(CreateControls().SetPlacement(1, 2, 24, 1));

    private static StackPanelComponent CreateControls()
        => new StackPanelComponent()
            .SetOrientation(UIOrientation.Horizontal)
            .SetSpacing(16)
            .SetVerticalAlignment(UIAlignment.Center)
            .SetMargin(UIThickness.All(0, 12, 0, 4))
            .AddChild(new TextInputComponent(SearchId)
                .SetPlaceholder("Search by name or glyph…")
                .SetPrefixIcon(MaterialIcons.Search)
                .BindValue(nameof(IconsController.Search))
                .SetDebounceMilliseconds(250)
                .SetWidth(UILayoutLength.Absolute(320))
            )
            .AddChild(new SelectComponent(SetId)
                .SetOptions([
                    new OptionItem { Id = IconsCatalog.MaterialSet, Title = "Material Symbols" },
                    new OptionItem { Id = IconsCatalog.LucideSet, Title = "Lucide" }
                ])
                .BindValue(nameof(IconsController.Set))
                .OnChange(nameof(IconsController.ChangeSet))
                .SetWidth(UILayoutLength.Absolute(200))
            )
            .AddChild(new SelectComponent(StyleId)
                .SetOptions([
                    new OptionItem { Id = IconsCatalog.FilledStyle, Title = "Filled" },
                    new OptionItem { Id = IconsCatalog.OutlinedStyle, Title = "Outlined" }
                ])
                .BindValue(nameof(IconsController.Style))
                .BindVisibility(nameof(IconsController.StyleVisibility))
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

    protected override IVisualComponent CreateContent()
        => new ContainerComponent()
            .SetPadding(UIThickness.All(24, 4, 24, 24))
            .SetHeight(UILayoutLength.Fill())
            .SetRow(1, UIGridUnit.Star())
            .AddChild(new ItemsViewComponent()
                .BindSource(nameof(IconsController.Icons))
                .SetWindowSize(IconsController.WindowSize)
                .SetLayoutType(UIItemsLayoutType.Wrap)
                .FilterBy(SearchId, IInputComponent.ValueProperty, nameof(IconItem.Name))
                .FilterBy(SetId, IInputComponent.ValueProperty, nameof(IconItem.Set), UIComparisonOperator.Equal)
                .FilterBy(StyleId, IInputComponent.ValueProperty, nameof(IconItem.Style), UIComparisonOperator.Equal)
                .VerticalScrollOnly()
                .SetSpacing(12)
                .SetHeight(UILayoutLength.Fill())
                .SetTemplate(CreateTile())
                .ConfigureDefaultEmptyTemplate(empty => empty.SetTitle("No name matches."))
                .SetPlacement(1, 1, 24, 1)
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
