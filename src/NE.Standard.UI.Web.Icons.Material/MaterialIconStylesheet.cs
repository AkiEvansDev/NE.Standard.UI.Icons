using System;
using System.Globalization;
using System.Text;
using NE.Standard.UI.Web.Abstractions.Theming;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// Builds the pack's stylesheet: the <c>@font-face</c> and one rule per registered glyph.
/// </summary>
internal static class MaterialIconStylesheet
{
    /// <summary>The family the <c>@font-face</c> declares. Prefixed, because a page may host several packs.</summary>
    private const string FontFamily = "ui-icons-material";

    public static string Build(MaterialIconRegistration registration, string fontPath)
    {
        ArgumentNullException.ThrowIfNull(registration);
        ArgumentException.ThrowIfNullOrWhiteSpace(fontPath);

        // Only an outlined value draws without a style; the whole set or a plain name beside it would have no rule.
        if (registration.Styles == 0 && (registration.IncludesEverything || registration.HasPlainName))
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

        // `block` rather than `swap`: no fallback could stand in for a glyph, so the choice is between a
        // moment of nothing and a moment of the ligature's letters spelled out.
        _ = builder
            .Append("@font-face { font-family: \"")
            .Append(FontFamily)
            .Append("\"; font-style: normal; font-weight: 400; font-display: block; src: url(\"")
            .Append(fontPath)
            .AppendLine("\") format(\"woff2\"); }");

        foreach (var name in registration.Names)
        {
            if (!MaterialIconNames.Contains(name))
                throw new InvalidOperationException($"Material has no glyph named '{name}'. Use the MaterialIcons constants — a misspelt name would otherwise render as nothing.");

            // Beside the whole set a named glyph is only checked, so a misspelling fails at startup either way; the set writes its rule.
            if (!registration.IncludesEverything)
                AppendGlyph(builder, registration, name);
        }

        if (registration.IncludesEverything)
        {
            foreach (var name in MaterialIconNames.All)
                AppendGlyph(builder, registration, name);
        }

        return builder.ToString();
    }

    /// <summary>
    /// The drawings one glyph is served in, told apart only by a suffix on the class: filled and outlined are the same
    /// ligature at opposite ends of the font's <c>FILL</c> axis.
    /// </summary>
    private static void AppendGlyph(StringBuilder builder, MaterialIconRegistration registration, string name)
    {
        if (registration.Styles.HasFlag(MaterialIconStyle.Fill))
            AppendRule(builder, name, suffix: string.Empty, fill: 1);

        // An outlined value asks for its own outlined drawing only; the whole set in both drawings is the style's to ask for.
        if (registration.Styles.HasFlag(MaterialIconStyle.Outlined) || registration.OutlinedNames.Contains(name))
            AppendRule(builder, name, MaterialIconNames.OutlinedSuffix, fill: 0);
    }

    /// <summary>One glyph's rule: the properties <c>.ui-icon::before</c> reads (MECHANISMS §7).</summary>
    private static void AppendRule(StringBuilder builder, string name, string suffix, int fill)
        => builder
            .Append('.')
            .Append(WebIconClassName.FromIconName(MaterialIconNames.Prefix + name + suffix))
            .Append(" { --ui-icon-font: \"")
            .Append(FontFamily)
            .Append("\"; --ui-icon-glyph: \"")
            // The ligature is the glyph's own name, underscores and all; the class carries the hyphenated
            // spelling because that is what a CSS identifier reads as one word.
            .Append(name.Replace('-', '_'))
            .Append("\"; --ui-icon-fill: ")
            .Append(fill.ToString(CultureInfo.InvariantCulture))
            .AppendLine("; }");
}
