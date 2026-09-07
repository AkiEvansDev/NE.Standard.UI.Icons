using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace NE.Standard.UI.Web.Icons.Lucide;

/// <summary>
/// Builds the pack's stylesheet from the glyphs an application registered.
/// </summary>
/// <remarks>
/// The glyph table ships deflated — 646 KB of JSON becomes 78 KB in the assembly — and is decompressed once,
/// when the stylesheet is written.
/// </remarks>
internal static class LucideIconStylesheet
{
    private const string ResourceName = "NE.Standard.UI.Web.Icons.Lucide.Client.dist.ui-icons-lucide.deflate";

    public static string Build(LucideIconRegistration registration)
    {
        ArgumentNullException.ThrowIfNull(registration);

        if (!registration.IncludesEverything && registration.Names.Count == 0)
        {
            throw new InvalidOperationException(
                "The Lucide icon pack was registered without any glyphs. Name them with AddLucideWebIcons(LucideIcons.Settings, …), " +
                "or pass LucideIconScope.All when the names come from data and cannot be listed.");
        }

        Dictionary<string, string> table = ReadTable();
        StringBuilder builder = new();

        _ = builder
            .Append("/* Lucide, ")
            .Append(registration.IncludesEverything ? "every glyph" : registration.Names.Count.ToString(CultureInfo.InvariantCulture) + " registered glyph(s)")
            .AppendLine(". Generated at startup — see LucideIconStylesheet. */");

        if (registration.IncludesEverything)
        {
            foreach (KeyValuePair<string, string> glyph in table)
                AppendRule(builder, glyph.Key, glyph.Value);

            return builder.ToString();
        }

        foreach (var name in registration.Names)
        {
            if (!table.TryGetValue(name, out var svg))
                throw new InvalidOperationException($"Lucide glyph 'lu-{name}' does not exist. Use the LucideIcons constants — a misspelt name would otherwise render as nothing.");

            AppendRule(builder, name, svg);
        }

        return builder.ToString();
    }

    private static void AppendRule(StringBuilder builder, string name, string svg)
        => builder
            .Append(".ui-icon-glyph--lu-")
            .Append(name)
            .Append(" { --ui-icon-url: url(\"data:image/svg+xml,")
            .Append(svg)
            .AppendLine("\"); }");

    private static Dictionary<string, string> ReadTable()
    {
        Assembly assembly = typeof(LucideIconStylesheet).Assembly;

        using Stream? packed = assembly.GetManifestResourceStream(ResourceName)
            ?? throw new InvalidOperationException($"Lucide glyph table '{ResourceName}' was not found in '{assembly.GetName().Name}'.");

        using DeflateStream json = new(packed, CompressionMode.Decompress);

        return JsonSerializer.Deserialize<Dictionary<string, string>>(json)
            ?? throw new InvalidOperationException($"Lucide glyph table '{ResourceName}' is empty.");
    }
}
