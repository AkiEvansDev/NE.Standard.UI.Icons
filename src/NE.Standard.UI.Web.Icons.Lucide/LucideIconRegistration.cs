using System;
using System.Collections.Generic;
using NE.Standard.UI.Web.Abstractions.Assets;

namespace NE.Standard.UI.Web.Icons.Lucide;

/// <summary>
/// What an application asked the Lucide pack for, accumulated across every <c>AddLucideWebIcons</c> call and
/// read once when the stylesheet is built.
/// </summary>
/// <remarks>
/// Lucide is 1 715 glyphs, roughly 700 KB of stylesheet; only the glyphs asked for reach the browser.
/// </remarks>
public sealed class LucideIconRegistration
{
    private readonly HashSet<string> _names = new(StringComparer.Ordinal);

    /// <summary>Whether the application asked for the whole set rather than naming glyphs.</summary>
    public bool IncludesEverything { get; private set; }

    /// <summary>The glyph names asked for, without the <c>lu-</c> prefix the constants carry.</summary>
    public IReadOnlyCollection<string> Names => _names;

    /// <summary>
    /// Adds glyphs to what the application serves, named by the <c>LucideIcons</c> constants; an unknown name
    /// throws rather than silently drawing nothing.
    /// </summary>
    public LucideIconRegistration Add(params string[] names)
    {
        ArgumentNullException.ThrowIfNull(names);

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
    public LucideIconRegistration AddEverything()
    {
        IncludesEverything = true;

        return this;
    }

    /// <summary>
    /// Strips the <c>lu-</c> the constants carry so the glyph class stays distinct from another pack's while
    /// the table stays keyed by Lucide's own name.
    /// </summary>
    internal static string Normalize(string name)
        => WebPackageRegistration.NormalizeIconName(name, "lu-");
}
