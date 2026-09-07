using System;

namespace NE.Standard.UI.Icons.Material;

/// <summary>
/// The hand-written half of <see cref="MaterialIcons"/>: how a name says which of the two drawings it wants.
/// </summary>
/// <remarks>
/// Both styles carry the same names, so the pack tells them apart by a suffix on the glyph value rather than
/// by a second set of constants — <c>MaterialIconStylesheet</c> emits <c>ms-edit</c> and <c>ms-edit-outlined</c>
/// from the one registered name, and the two rules differ only in where they sit on the font's <c>FILL</c>
/// axis. An application that registers only <c>MaterialIconStyle.Fill</c> and then asks for an outlined glyph
/// draws nothing, which is the same failure as a misspelt name.
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
