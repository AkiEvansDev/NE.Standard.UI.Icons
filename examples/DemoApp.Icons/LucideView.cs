using NE.Standard.UI.Authoring.Components;
using NE.Standard.UI.Authoring.Views;
using NE.Standard.UI.Components.BuiltIns.Items;

namespace DemoApp.Icons;

/// <summary>
/// Lucide: every name, drawn the one way the set draws.
/// </summary>
internal sealed class LucideView : IconsDemoView, IUIViewDefinition
{
    public static string ViewKey => "icons.lucide";

    protected override string Route => LucideRoute;

    public override string Title => "Lucide";

    protected override string Description
        => "Every name in the set, read off the package itself. The set is deprecated and frozen; it stays for the applications on it.";

    protected override IVisualComponent? CreateSetControl()
        => null;

    protected override ItemsViewComponent FilterBySet(ItemsViewComponent gallery)
        => gallery;
}
