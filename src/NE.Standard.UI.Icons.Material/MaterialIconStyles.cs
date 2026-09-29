using System;

namespace NE.Standard.UI.Icons.Material;

/// <summary>
/// The hand-written half of <see cref="MaterialIcons"/>: how a name says which of the two drawings it wants.
/// </summary>
/// <remarks>
/// The outlined drawing (<c>ms-edit-outlined</c>) draws only where its value or <c>MaterialIconStyle.Outlined</c> is registered;
/// otherwise it draws nothing, like a misspelt name.
/// </remarks>
public static partial class MaterialIcons
{
    /// <summary>What the outlined drawing adds to a glyph's name.</summary>
    public const string OutlinedSuffix = "-outlined";

    /// <summary>The outlined drawing of a glyph named by one of the constants. Idempotent.</summary>
    public static string Outlined(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return name.EndsWith(OutlinedSuffix, StringComparison.Ordinal) ? name : name + OutlinedSuffix;
    }
}
