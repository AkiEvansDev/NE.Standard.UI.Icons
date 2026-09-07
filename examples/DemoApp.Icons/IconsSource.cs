using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using NE.Standard.UI.Abstractions.Binding;
using NE.Standard.UI.Abstractions.Data;
using NE.Standard.UI.Abstractions.Recursive;
using NE.Standard.UI.Data;
using NE.Standard.UI.Primitives.Annotations;

namespace DemoApp.Icons;

/// <summary>
/// One tile: the constant an author writes, and the value that draws it.
/// </summary>
internal sealed partial class IconItem : RecursiveObservable, IBindableItem
{
    /// <summary>The glyph without a drawing's suffix, so a tile keeps its identity when the drawing changes.</summary>
    [RecursiveMember(false)]
    public required string Id { get; init; }

    [RecursiveMember]
    public partial string Name { get; set; } = string.Empty;

    [RecursiveMember]
    public partial string Glyph { get; set; } = string.Empty;

    /// <summary>Which set the tile came from; the host's set filter names this property.</summary>
    [RecursiveMember]
    public partial string Set { get; set; } = string.Empty;

    /// <summary>Which drawing the tile shows; empty for a set that has only one.</summary>
    [RecursiveMember]
    public partial string Style { get; set; } = string.Empty;
}

/// <summary>
/// What the gallery's three rules resolved to, read off the query a window request carries.
/// </summary>
internal readonly record struct IconsQuery(string Set, string Style, string Text)
{
    /// <summary>The gallery as it opens, and what a request with no terms means.</summary>
    public static IconsQuery Default { get; } = new(IconsCatalog.MaterialSet, IconsCatalog.FilledStyle, string.Empty);

    /// <summary>
    /// Reads the terms this source understands; a rule that is not active contributes none, which is what makes
    /// an empty search box mean "everything".
    /// </summary>
    public static IconsQuery Read(UIItemsQuery query)
    {
        ArgumentNullException.ThrowIfNull(query);

        IconsQuery result = Default;

        for (var i = 0; i < query.Filters.Length; i++)
        {
            UIItemFilterTerm term = query.Filters[i];
            var value = Convert.ToString(term.Value, CultureInfo.InvariantCulture) ?? string.Empty;

            result = term.ItemProperty switch
            {
                nameof(IconItem.Set) => result with { Set = value },
                nameof(IconItem.Style) => result with { Style = value },
                nameof(IconItem.Name) => result with { Text = value.Trim() },
                _ => result
            };
        }

        return result;
    }
}

/// <summary>
/// A set's names, a window at a time: the search is resolved on the server, so the browser never holds
/// four thousand tiles to filter.
/// </summary>
internal sealed partial class IconsSource : UIItemSourceBase<IconItem>
{
    /// <summary>What the line under the search box reads. The source writes it because it is what counts.</summary>
    [RecursiveMember]
    public partial string Caption { get; set; } = Describe(IconsQuery.Default, IconsCatalog.Count(IconsCatalog.MaterialSet));

    protected override Task<UIItemWindow<IconItem>> GetWindowAsync(UIItemWindowRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IconsQuery query = IconsQuery.Read(request.Query);
        IconName[] matches = IconsCatalog.Match(query);
        var total = matches.Length;

        var start = request.Anchor.Kind switch
        {
            UIItemAnchorKind.Start => 0,
            UIItemAnchorKind.End => total - request.Count,
            UIItemAnchorKind.Offset => request.Anchor.Offset,
            UIItemAnchorKind.Before => IconsCatalog.PositionOf(matches, request.Anchor.Key!) - request.Count,
            UIItemAnchorKind.After => IconsCatalog.PositionOf(matches, request.Anchor.Key!) + 1,
            _ => 0
        };

        start = Math.Clamp(start, 0, Math.Max(0, total - 1));

        var count = Math.Max(0, Math.Min(request.Count, total - start));
        IconItem[] items = new IconItem[count];

        for (var i = 0; i < count; i++)
            items[i] = Shape(matches[start + i], query);

        Caption = Describe(query, total);

        return Task.FromResult(new UIItemWindow<IconItem>(items)
        {
            Offset = start,
            TotalCount = total,
            HasMoreBefore = start > 0,
            HasMoreAfter = start + count < total
        });
    }

    private static IconItem Shape(IconName name, IconsQuery query)
        => new()
        {
            Id = name.Glyph,
            Name = name.Name,
            Glyph = IconsCatalog.Draw(name, query),
            Set = query.Set,
            // Lucide draws one way, so its tiles carry no drawing; the selector that picks one is hidden with it.
            Style = query.Set == IconsCatalog.MaterialSet ? query.Style : string.Empty
        };

    private static string Describe(IconsQuery query, int total)
    {
        var set = query.Set == IconsCatalog.LucideSet ? "Lucide" : "Material Symbols";
        var whole = IconsCatalog.Count(query.Set);

        return query.Text.Length == 0
            ? string.Create(CultureInfo.InvariantCulture, $"{whole:N0} names in {set}.")
            : string.Create(CultureInfo.InvariantCulture, $"{total:N0} of {whole:N0} names in {set} match “{query.Text}”.");
    }
}
