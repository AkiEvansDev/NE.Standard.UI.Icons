using System;
using System.Linq;
using System.Reflection;
using NE.Standard.UI.Abstractions.Styling;
using NE.Standard.UI.Authoring.Components;
using NE.Standard.UI.Components.BuiltIns.Contents;
using NE.Standard.UI.Components.BuiltIns.Layouts;
using NE.Standard.UI.Components.Views;
using NE.Standard.UI.Icons.Lucide;
using NE.Standard.UI.Primitives.Styling;
using NE.Standard.UI.Views;

namespace DemoApp.Icons;

/// <summary>
/// Every name the Lucide set offers, on one page.
/// </summary>
internal sealed class LucideView : UIViewBase, IUIViewDefinition
{
    public static string ViewKey => "icons.lucide";

    // Read off the package rather than listed here: the page is only worth having if it cannot fall behind
    // the table it documents.
    private static readonly (string Name, string Slug)[] Names = ReadNames();

    public override UIViewOptions Options { get; } = new() { StickyHeader = true };

    protected override IVisualComponent? CreateHeader()
        => new ContainerComponent()
            .SetPadding(UIThickness.All(24, 20, 24, 4))
            .AddRow(UIGridUnit.Star())
            .AddChild(new TextComponent()
                .SetTitle("Lucide")
                .SetDescription($"{Names.Length} names from NE.Standard.UI.Icons.Lucide. The caption is the constant; hover for the slug it resolves to.")
                .SetPlacement(1, 1, 24, 1)
            );

    protected override IVisualComponent CreateContent()
    {
        WrapPanelComponent tiles = new WrapPanelComponent()
            .SetPadding(UIThickness.All(24, 4, 24, 24))
            .SetHorizontalGap(12)
            .SetVerticalGap(12);

        foreach ((var name, var slug) in Names)
            _ = tiles.AddChild(CreateTile(name, slug));

        return tiles;
    }

    private static ContainerComponent CreateTile(string name, string slug)
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
                    .SetIcon(slug)
                    .SetSize(UIIconSize.Large)
                    .SetTooltip(slug)
                )
                .AddChild(new TextComponent()
                    .SetTitle(name)
                    .SetTitleType(UITextAppearance.Caption)
                    .SetHorizontalAlignment(UIAlignment.Center)
                )
            )
            .SetPlacement(1, 1, 3, 1);

    private static (string Name, string Slug)[] ReadNames()
        => [.. typeof(LucideIcons)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(field => field.IsLiteral && field.FieldType == typeof(string))
            .Select(field => (field.Name, Slug: (string)field.GetRawConstantValue()!))
            .OrderBy(icon => icon.Name, StringComparer.Ordinal)];
}
