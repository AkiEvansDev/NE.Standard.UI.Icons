using System;
using System.Collections.Generic;
using NE.Standard.UI.Web.Abstractions.Assets;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// What an application asked the Material pack for, accumulated across every <c>AddMaterialWebIcons</c> call
/// and read once when the stylesheet is built.
/// </summary>
/// <remarks>
/// Registration decides which glyph *classes* exist, not what's downloaded — the font already carries every
/// glyph — but writing all 3 903 classes would still bloat the stylesheet, so an unregistered name is still
/// worth failing on.
/// </remarks>
public sealed class MaterialIconRegistration
{
    private readonly HashSet<string> _names = new(StringComparer.Ordinal);

    /// <summary>Whether the application asked for the whole set rather than naming glyphs.</summary>
    public bool IncludesEverything { get; private set; }

    /// <summary>Which drawings are served, combined across every call.</summary>
    public MaterialIconStyle Styles { get; private set; }

    /// <summary>The glyph names asked for, without the <c>ms-</c> prefix the constants carry.</summary>
    public IReadOnlyCollection<string> Names => _names;

    /// <summary>
    /// Adds glyphs to what the application serves, named by the <c>MaterialIcons</c> constants; an unknown
    /// name throws rather than silently drawing nothing.
    /// </summary>
    public MaterialIconRegistration Add(MaterialIconStyle styles, params string[] names)
    {
        ArgumentNullException.ThrowIfNull(names);

        Styles |= styles;

        foreach (var name in names)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            _ = _names.Add(Normalize(name));
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
    /// Strips the <c>ms-</c> the constants carry so the glyph class stays distinct from another pack's while
    /// the table stays keyed by Material's own name.
    /// </summary>
    internal static string Normalize(string name)
        => WebPackageRegistration.NormalizeIconName(name, "ms-");
}
