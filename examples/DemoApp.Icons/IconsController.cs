using System;
using System.Threading;
using System.Threading.Tasks;
using NE.Standard.UI.Abstractions.Data;
using NE.Standard.UI.Controllers;
using NE.Standard.UI.Primitives.Annotations;
using NE.Standard.UI.Primitives.Styling;

namespace DemoApp.Icons;

/// <summary>
/// What the gallery is showing: which set, which drawing, and what is being looked for.
/// </summary>
/// <remarks>
/// All three are bound rather than left in the browser, because a windowed host resolves its rules on the
/// server — the client holds a hundred tiles of four thousand and could only ever have searched those.
/// </remarks>
internal sealed partial class IconsController : UIControllerBase
{
    /// <summary>How many tiles one read hands over.</summary>
    public const int WindowSize = 100;

    [RecursiveMember]
    public partial string Search { get; set; } = string.Empty;

    [RecursiveMember]
    public partial string Set { get; set; } = IconsCatalog.MaterialSet;

    [RecursiveMember]
    public partial string Style { get; set; } = IconsCatalog.FilledStyle;

    [RecursiveMember]
    public partial UIVisibility StyleVisibility { get; set; } = UIVisibility.Visible;

    [RecursiveMember(false)]
    public IconsSource Icons { get; } = new();

    /// <summary>
    /// Reads the first window here rather than leaving it to the client, so the page paints with tiles in it.
    /// </summary>
    /// <remarks>
    /// No query: the rules are not resolved yet at this point, and their defaults are what an empty one means.
    /// </remarks>
    protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        => await Icons.LoadWindowAsync(new UIItemWindowRequest(UIItemAnchor.Start, WindowSize), cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// The set changed. Nothing here re-reads the window — the host's rules read <c>Set</c>, so the runtime
    /// does — only the drawing selector, which means nothing outside Material and goes away with it.
    /// </summary>
    [UICommand]
    public void ChangeSet()
        => StyleVisibility = string.Equals(Set, IconsCatalog.MaterialSet, StringComparison.Ordinal)
            ? UIVisibility.Visible
            : UIVisibility.Collapsed;

    [UICommand]
    public void ClearSearch()
        => Search = string.Empty;
}
