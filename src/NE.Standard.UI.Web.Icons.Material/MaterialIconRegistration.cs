using System;
using System.Collections.Generic;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// What an application asked the Material pack for, accumulated across every <c>AddMaterialWebIcons</c> call
/// and read once, when the stylesheet is built.
/// </summary>
/// <remarks>
/// Registration decides which glyph *classes* exist, which is what turns a name into a drawing. It stopped
/// deciding what is downloaded when the pack moved to a font — one 382 KB file carries all 3 903 glyphs — but
/// writing every one of them out is half a megabyte of stylesheet for an application that draws forty, so a
/// registration is still asked for and a name that is not in the font is still worth failing on.
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
    /// Adds glyphs to what the application serves. Names are the <c>MaterialIcons</c> constants; a name this
    /// pack does not know is a mistake worth failing on rather than a glyph that silently never draws.
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
        => name.StartsWith("ms-", StringComparison.Ordinal) ? name[3..] : name;
}
