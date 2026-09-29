using System;
using System.Collections.Generic;
using NE.Standard.UI.Web.Abstractions.Assets;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// What an application asked the Material pack for, across every <c>AddMaterialWebIcons</c> call.
/// </summary>
/// <remarks>
/// Registration decides which glyph classes exist, not what's downloaded; a rule for every glyph would bloat the stylesheet,
/// so an unregistered name is worth failing on.
/// </remarks>
public sealed class MaterialIconRegistration
{
    private readonly HashSet<string> _names = new(StringComparer.Ordinal);
    private readonly HashSet<string> _outlinedNames = new(StringComparer.Ordinal);

    /// <summary>Whether the application asked for the whole set rather than naming glyphs.</summary>
    public bool IncludesEverything { get; private set; }

    /// <summary>Which drawings every registered name is served in, combined across every call.</summary>
    public MaterialIconStyle Styles { get; private set; }

    /// <summary>The glyph names asked for, as Material names them: no <c>ms-</c> prefix, no outlined suffix.</summary>
    public IReadOnlyCollection<string> Names => _names;

    /// <summary>The names asked for by their outlined value, each served outlined whatever <see cref="Styles"/> says.</summary>
    public IReadOnlySet<string> OutlinedNames => _outlinedNames;

    /// <summary>Whether some name was asked for by its plain value, which draws only in <see cref="Styles"/>.</summary>
    internal bool HasPlainName { get; private set; }

    /// <summary>
    /// Adds glyphs named by the <c>MaterialIcons</c> constants (an unknown one fails the startup); an outlined value serves that
    /// glyph's outlined drawing, not every name's.
    /// </summary>
    public MaterialIconRegistration Add(MaterialIconStyle styles, params string[] names)
    {
        ArgumentNullException.ThrowIfNull(names);

        Styles |= styles;

        foreach (var name in names)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            var glyph = Normalize(name, out var outlined);
            _ = _names.Add(glyph);

            if (outlined)
                _ = _outlinedNames.Add(glyph);
            else
                HasPlainName = true;
        }

        return this;
    }

    /// <summary>
    /// Serves every glyph the pack knows — for a gallery or a demo, and for an application whose icon names
    /// come from data and so cannot be listed at startup.
    /// </summary>
    public MaterialIconRegistration AddEverything(MaterialIconStyle styles)
    {
        Styles |= styles;
        IncludesEverything = true;

        return this;
    }

    /// <summary>
    /// Strips the <c>ms-</c> prefix and the outlined suffix, so the table is keyed by Material's own name; says whether
    /// the suffix was there.
    /// </summary>
    internal static string Normalize(string name, out bool outlined)
    {
        var glyph = WebPackageRegistration.NormalizeIconName(name, MaterialIconNames.Prefix);
        outlined = glyph.EndsWith(MaterialIconNames.OutlinedSuffix, StringComparison.Ordinal);

        return outlined ? glyph[..^MaterialIconNames.OutlinedSuffix.Length] : glyph;
    }
}
