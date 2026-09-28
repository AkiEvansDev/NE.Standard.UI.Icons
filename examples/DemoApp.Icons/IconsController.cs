using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Icons;

/// <summary>
/// What the gallery is showing: what is being looked for, and in which of Material's drawings.
/// </summary>
/// <remarks>
/// The search is bound rather than left in the browser, because a windowed host resolves its rules on the server — the client
/// holds a hundred tiles of four thousand and could only ever have searched those.
/// </remarks>
internal sealed partial class IconsController : UIControllerBase
{
    /// <summary>How many tiles one read hands over.</summary>
    public const int WindowSize = 100;

    [RecursiveMember]
    public partial string Search { get; set; } = string.Empty;

    [RecursiveMember]
    public partial string Style { get; set; } = IconsCatalog.FilledStyle;

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

    [UICommand]
    public void ClearSearch()
        => Search = string.Empty;
}
