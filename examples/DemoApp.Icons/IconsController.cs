using System.Threading;
using System.Threading.Tasks;
using NE.Standard.UI.Abstractions.Data;
using NE.Standard.UI.Controllers;
using NE.Standard.UI.Primitives.Annotations;

namespace DemoApp.Icons;

/// <summary>
/// What a set's gallery is showing: the set the page stands for, and what is being looked for in it.
/// </summary>
/// <remarks>
/// The search is bound rather than left in the browser, because a windowed host resolves its rules on the server — the client
/// holds a hundred tiles of four thousand and could only ever have searched those.
/// </remarks>
internal abstract partial class IconsController(string set) : UIControllerBase
{
    /// <summary>How many tiles one read hands over.</summary>
    public const int WindowSize = 100;

    [RecursiveMember]
    public partial string Search { get; set; } = string.Empty;

    [RecursiveMember(false)]
    public IconsSource Icons { get; } = new(set);

    /// <summary>
    /// Reads the first window here rather than leaving it to the client, so the page paints with tiles in it.
    /// </summary>
    /// <remarks>
    /// No query: the rules are not resolved yet at this point, and their defaults are what an empty one means.
    /// </remarks>
    protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        => await Icons.LoadWindowAsync(new UIItemWindowRequest(UIItemAnchor.Start, WindowSize), cancellationToken).ConfigureAwait(false);

    [UICommand]
    public void ClearSearch()
        => Search = string.Empty;
}

/// <summary>Material Symbols, in the drawing the page's selector picks.</summary>
internal sealed partial class MaterialIconsController() : IconsController(IconsCatalog.MaterialSet)
{
    [RecursiveMember]
    public partial string Style { get; set; } = IconsCatalog.FilledStyle;
}

/// <summary>Lucide, which draws one way.</summary>
internal sealed partial class LucideIconsController() : IconsController(IconsCatalog.LucideSet);
