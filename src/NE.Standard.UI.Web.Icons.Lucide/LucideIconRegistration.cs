using System;
using System.Collections.Generic;

namespace NE.Standard.UI.Web.Icons.Lucide;

/// <summary>
/// What an application asked the Lucide pack for, accumulated across every <c>AddLucideWebIcons</c> call and
/// read once, when the stylesheet is built.
/// </summary>
/// <remarks>
/// Lucide is 1 715 glyphs and roughly 700 KB of stylesheet; an application draws tens of them. The table
/// ships in the assembly so every name stays available and discoverable, and what reaches the browser is what
/// was asked for.
/// </remarks>
public sealed class LucideIconRegistration
{
    private readonly HashSet<string> _names = new(StringComparer.Ordinal);

    /// <summary>Whether the application asked for the whole set rather than naming glyphs.</summary>
    public bool IncludesEverything { get; private set; }

    /// <summary>The glyph names asked for, without the <c>lu-</c> prefix the constants carry.</summary>
    public IReadOnlyCollection<string> Names => _names;

    /// <summary>
    /// Adds glyphs to what the application serves. Names are the <c>LucideIcons</c> constants; a name this
    /// pack does not know is a mistake worth failing on rather than a glyph that silently never draws.
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
        => name.StartsWith("lu-", StringComparison.Ordinal) ? name[3..] : name;
}
