using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NE.Standard.UI.Icons.Material;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// Which glyph names the font actually carries, read once out of the <see cref="MaterialIcons"/> constants.
/// </summary>
/// <remarks>
/// Read from the constants rather than shipped as a second generated table: the constants are generated from
/// the same run that builds the font, so they cannot disagree with it, and a second list of 3 903 names would
/// be one more thing to keep in step. The cost is one reflection pass over a static class, once, and only when
/// something is registered.
/// </remarks>
internal static class MaterialIconNames
{
    /// <summary>What the outlined drawing adds to a glyph's class. The same suffix the authoring side uses.</summary>
    public const string OutlinedSuffix = MaterialIcons.OutlinedSuffix;

    private const string Prefix = "ms-";

    private static readonly FrozenSet<string> Known = Read();

    /// <summary>Every name the pack knows, without the <c>ms-</c> prefix the constants carry.</summary>
    public static IReadOnlyCollection<string> All => Known;

    public static bool Contains(string name) => Known.Contains(name);

    [UnconditionalSuppressMessage("Trimming", "IL2072",
        Justification = "MaterialIcons is a generated static class of public const string fields in a package this one references; it is rooted by that reference.")]
    private static FrozenSet<string> Read()
    {
        FieldInfo[] fields = typeof(MaterialIcons).GetFields(BindingFlags.Public | BindingFlags.Static);
        HashSet<string> names = new(fields.Length, StringComparer.Ordinal);

        foreach (FieldInfo field in fields)
        {
            if (!field.IsLiteral || field.FieldType != typeof(string))
                continue;

            if (field.GetRawConstantValue() is string value && value.StartsWith(Prefix, StringComparison.Ordinal))
                _ = names.Add(value[Prefix.Length..]);
        }

        if (names.Count == 0)
            throw new InvalidOperationException($"No Material glyph names were found on {nameof(MaterialIcons)}.");

        return names.ToFrozenSet(StringComparer.Ordinal);
    }
}
