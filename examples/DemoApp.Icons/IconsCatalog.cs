using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NE.Standard.UI.Icons.Lucide;
using NE.Standard.UI.Icons.Material;

namespace DemoApp.Icons;

/// <summary>
/// One name in a set: the constant an author writes, and the value it holds.
/// </summary>
internal readonly record struct IconName(string Name, string Glyph);

/// <summary>
/// The names both sets offer, and the query the gallery reads them under.
/// </summary>
/// <remarks>
/// Read off the packages rather than listed here: the page is only worth having if it cannot fall behind the
/// tables it documents.
/// </remarks>
internal static class IconsCatalog
{
    /// <summary>What the set selector holds, and what a filter term on an item's set compares against.</summary>
    public const string MaterialSet = "material";

    /// <inheritdoc cref="MaterialSet"/>
    public const string LucideSet = "lucide";

    /// <summary>What the drawing selector holds. Material carries both; Lucide draws one way.</summary>
    public const string FilledStyle = "filled";

    /// <inheritdoc cref="FilledStyle"/>
    public const string OutlinedStyle = "outlined";

    private static readonly IconName[] MaterialNames = Read(typeof(MaterialIcons), "ms-");

    private static readonly IconName[] LucideNames = Read(typeof(LucideIcons), "lu-");

    /// <summary>How many names a set holds, for the caption under the search box.</summary>
    public static int Count(string set)
        => Names(set).Length;

    /// <summary>
    /// The names a query leaves, in the order they are shown.
    /// </summary>
    /// <remarks>
    /// A scan, because a set is a few thousand names in an array; a source over a database would translate the
    /// terms instead. The text is matched against the glyph as well as the constant, because a reader who knows
    /// the icon knows it as <c>arrow-right</c> rather than as <c>ArrowRight</c>.
    /// </remarks>
    public static IconName[] Match(IconsQuery query)
    {
        IconName[] names = Names(query.Set);

        if (query.Text.Length == 0)
            return names;

        List<IconName> matches = [];

        for (var i = 0; i < names.Length; i++)
        {
            if (names[i].Name.Contains(query.Text, StringComparison.OrdinalIgnoreCase)
                || names[i].Glyph.Contains(query.Text, StringComparison.OrdinalIgnoreCase))
            {
                matches.Add(names[i]);
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

    /// <summary>What the icon component is given: the constant's value, plus the suffix where a set has drawings.</summary>
    public static string Draw(IconName name, IconsQuery query)
        => query.Set == MaterialSet && query.Style == OutlinedStyle ? MaterialIcons.Outlined(name.Glyph) : name.Glyph;

    private static IconName[] Names(string set)
        => set == LucideSet ? LucideNames : MaterialNames;

    // The prefix, not just the type: MaterialIcons carries OutlinedSuffix among its constants, which is not a name.
    private static IconName[] Read(Type set, string prefix)
        => [.. set
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(field => field.IsLiteral && field.FieldType == typeof(string))
            .Select(field => new IconName(field.Name, (string)field.GetRawConstantValue()!))
            .Where(icon => icon.Glyph.StartsWith(prefix, StringComparison.Ordinal))
            .OrderBy(icon => icon.Name, StringComparer.Ordinal)];
}
