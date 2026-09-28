using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DemoApp.Icons;

/// <summary>
/// One name in the set: the constant an author writes, and the value it holds.
/// </summary>
internal readonly record struct IconName(string Name, string Glyph);

/// <summary>
/// The names Material Symbols offers, and the query the gallery reads them under.
/// </summary>
/// <remarks>
/// Read off the package rather than listed here: the page is only worth having if it cannot fall behind the
/// table it documents.
/// </remarks>
internal static class IconsCatalog
{
    /// <summary>What the drawing selector holds, and what a filter term on an item's drawing compares against.</summary>
    public const string FilledStyle = "filled";

    /// <inheritdoc cref="FilledStyle"/>
    public const string OutlinedStyle = "outlined";

    // The prefix, not just the type: MaterialIcons carries OutlinedSuffix among its constants, which is not a name.
    private static readonly IconName[] Names = [.. typeof(MaterialIcons)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Where(field => field.IsLiteral && field.FieldType == typeof(string))
        .Select(field => new IconName(field.Name, (string)field.GetRawConstantValue()!))
        .Where(icon => icon.Glyph.StartsWith("ms-", StringComparison.Ordinal))
        .OrderBy(icon => icon.Name, StringComparer.Ordinal)];

    /// <summary>How many names the set holds, for the caption under the search box.</summary>
    public static int Count => Names.Length;

    /// <summary>
    /// The names a query leaves, in the order they are shown.
    /// </summary>
    /// <remarks>
    /// A scan, because the set is a few thousand names in an array; a source over a database would translate the
    /// terms instead. The text is matched against the glyph as well as the constant, because a reader who knows
    /// the icon knows it as <c>arrow-right</c> rather than as <c>ArrowRight</c>.
    /// </remarks>
    public static IconName[] Match(IconsQuery query)
    {
        if (query.Text.Length == 0)
            return Names;

        List<IconName> matches = [];

        for (var i = 0; i < Names.Length; i++)
        {
            if (Names[i].Name.Contains(query.Text, StringComparison.OrdinalIgnoreCase)
                || Names[i].Glyph.Contains(query.Text, StringComparison.OrdinalIgnoreCase))
            {
                matches.Add(Names[i]);
            }
        }

        return [.. matches];
    }

    /// <summary>Where a name sits under a query, for a window anchored before or after one.</summary>
    public static int PositionOf(IconName[] matches, string glyph)
    {
        for (var i = 0; i < matches.Length; i++)
        {
            if (string.Equals(matches[i].Glyph, glyph, StringComparison.Ordinal))
                return i;
        }

        return 0;
    }

    /// <summary>What the icon component is given: the constant's value, plus the suffix for the outlined drawing.</summary>
    public static string Draw(IconName name, IconsQuery query)
        => query.Style == OutlinedStyle ? MaterialIcons.Outlined(name.Glyph) : name.Glyph;
}
