using NE.Standard.UI.Abstractions.Styling;
using NE.Standard.UI.Authoring.Components;
using NE.Standard.UI.Authoring.Views;
using NE.Standard.UI.Components.BuiltIns.Inputs;
using NE.Standard.UI.Components.BuiltIns.Items;
using NE.Standard.UI.Components.BuiltIns.Models;
using NE.Standard.UI.Primitives.Interaction;

namespace DemoApp.Icons;

/// <summary>
/// Material Symbols: every name, in the drawing the selector picks.
/// </summary>
internal sealed class MaterialView : IconsDemoView, IUIViewDefinition
{
    public static string ViewKey => "icons.material";

    protected override string Route => MaterialRoute;

    public override string Title => "Material Symbols";

    protected override string Description
        => "Every name in the set, read off the package itself. A tile's caption is the constant an author writes; hover the glyph for the value it resolves to.";

    protected override IVisualComponent? CreateSetControl()
        => new SelectComponent(StyleId)
            .SetOptions([
                new OptionItem { Id = IconsCatalog.FilledStyle, Title = "Filled" },
                new OptionItem { Id = IconsCatalog.OutlinedStyle, Title = "Outlined" }
            ])
            .BindValue(nameof(MaterialIconsController.Style))
            .SetWidth(UILayoutLength.Absolute(160));

    protected override ItemsViewComponent FilterBySet(ItemsViewComponent gallery)
        => gallery.FilterBy(StyleId, IInputComponent.ValueProperty, nameof(IconItem.Style), UIComparisonOperator.Equal);
}
