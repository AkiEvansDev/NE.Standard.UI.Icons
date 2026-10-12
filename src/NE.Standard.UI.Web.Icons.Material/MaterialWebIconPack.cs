using System;
using System.Text;
using NE.Standard.UI.Web.Abstractions.Theming;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// The Material pack's answer to how one of its glyphs is drawn, for a framework glyph an application maps onto it: any name the font
/// carries, registered or not, filled or by its outlined value outlined.
/// </summary>
internal sealed class MaterialWebIconPack : IWebIconPack
{
    public bool TryWriteDeclarations(string icon, StringBuilder css)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(icon);
        ArgumentNullException.ThrowIfNull(css);

        if (!icon.StartsWith(MaterialIconNames.Prefix, StringComparison.Ordinal))
            return false;

        var name = MaterialIconRegistration.Normalize(icon, out var outlined);

        if (!MaterialIconNames.Contains(name))
            return false;

        MaterialIconStylesheet.AppendDeclarations(css, name, outlined ? 0 : 1);
        return true;
    }
}
