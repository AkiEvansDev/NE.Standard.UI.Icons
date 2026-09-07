using System;
using System.Globalization;
using System.Text;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// Builds the pack's stylesheet from the glyphs an application registered: the <c>@font-face</c> the font is
/// reached through, and one rule per glyph naming the ligature that draws it.
/// </summary>
/// <remarks>
/// The font carries every glyph, so registration no longer decides what is *downloaded* — it decides which
/// classes exist. That is still worth doing and still worth failing on: a class per glyph is what turns
/// <c>ms-check</c> into a drawing, an unregistered name draws nothing, and all 3 903 of them written out
/// would be a stylesheet the size of the font itself.
/// <para>
/// Nothing here is a data URI any more. The font is served as its own asset because a 382 KB <c>woff2</c>
/// inlined into CSS is 509 KB of base64 that no browser can cache separately from the rules around it.
/// </para>
/// </remarks>
internal static class MaterialIconStylesheet
{
    /// <summary>The family the <c>@font-face</c> declares. Prefixed, because a page may host several packs.</summary>
    private const string FontFamily = "ui-icons-material";

    public static string Build(MaterialIconRegistration registration, string fontPath)
    {
        ArgumentNullException.ThrowIfNull(registration);
        ArgumentException.ThrowIfNullOrWhiteSpace(fontPath);

        if (registration.Styles == 0)
            throw new InvalidOperationException("The Material icon pack was registered without a style. Pass MaterialIconStyle.Fill, Outlined, or both.");

        if (!registration.IncludesEverything && registration.Names.Count == 0)
        {
            throw new InvalidOperationException(
                "The Material icon pack was registered without any glyphs. Name them with AddMaterialWebIcons(style, MaterialIcons.Settings, …), " +
                "or pass MaterialIconScope.All when the names come from data and cannot be listed.");
        }

        StringBuilder builder = new();

        _ = builder
            .Append("/* Material Symbols Rounded, ")
            .Append(registration.IncludesEverything ? "every glyph" : registration.Names.Count.ToString(CultureInfo.InvariantCulture) + " registered glyph(s)")
            .AppendLine(". Generated at startup — see MaterialIconStylesheet. */");

        // `block` rather than `swap`: there is no fallback that could stand in for a glyph, so the choice is
        // between a moment of nothing and a moment of the ligature's own letters spelled out.
        _ = builder
            .Append("@font-face { font-family: \"")
            .Append(FontFamily)
            .Append("\"; font-style: normal; font-weight: 400; font-display: block; src: url(\"")
            .Append(fontPath)
            .AppendLine("\") format(\"woff2\"); }");

        if (registration.IncludesEverything)
        {
            foreach (var name in MaterialIconNames.All)
                AppendGlyph(builder, registration, name);

            return builder.ToString();
        }

        foreach (var name in registration.Names)
        {
            if (!MaterialIconNames.Contains(name))
                throw new InvalidOperationException($"Material glyph 'ms-{name}' does not exist. Use the MaterialIcons constants — a misspelt name would otherwise render as nothing.");

            AppendGlyph(builder, registration, name);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Both drawings of one glyph, told apart by a suffix on the class and by nothing else: the filled and
    /// the outlined one are the same ligature at the two ends of the font's <c>FILL</c> axis.
    /// </summary>
    private static void AppendGlyph(StringBuilder builder, MaterialIconRegistration registration, string name)
    {
        if (registration.Styles.HasFlag(MaterialIconStyle.Fill))
            AppendRule(builder, name, suffix: string.Empty, fill: 1);

        if (registration.Styles.HasFlag(MaterialIconStyle.Outlined))
            AppendRule(builder, name, MaterialIconNames.OutlinedSuffix, fill: 0);
    }

    private static void AppendRule(StringBuilder builder, string name, string suffix, int fill)
        => builder
            .Append(".ui-icon-glyph--ms-")
            .Append(name)
            .Append(suffix)
            .Append(" { --ui-icon-font: \"")
            .Append(FontFamily)
            .Append("\"; --ui-icon-glyph: \"")
            // The ligature is the glyph's own name, underscores and all; the class carries the hyphenated
            // spelling because that is what a CSS identifier reads as one word.
            .Append(name.Replace('-', '_'))
            .Append("\"; --ui-icon-fill: ")
            .Append(fill.ToString(CultureInfo.InvariantCulture))
            // A glyph is text and takes the element's colour; the box behind it paints nothing. Painting is
            // what the mask forms want and stays their default, so it is this form that opts out.
            .AppendLine("; --ui-icon-paint: transparent; }");
}
