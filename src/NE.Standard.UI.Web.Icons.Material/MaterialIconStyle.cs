using System;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// Which drawing of a Material symbol the application serves. Both are the same names.
/// </summary>
[Flags]
public enum MaterialIconStyle
{
    /// <summary>The solid drawing — the one that survives being asked for at 12 or 14 pixels.</summary>
    Fill = 1,

    /// <summary>The line drawing, for glyphs standing on their own at a size that carries them.</summary>
    Outlined = 2
}
