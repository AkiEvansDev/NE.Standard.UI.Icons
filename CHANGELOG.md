# Changelog

One section per release of this slice, headed `## X.Y.Z` and named by the tag — `icons/vX.Y.Z`. The release
workflow cuts the matching section out to become the body of the GitHub release, and a tag with no section
fails the release before anything is published.

## 1.0.0-preview.2

The **Material Symbols** set, as `NE.Standard.UI.Icons.Material` (every name) and
`NE.Standard.UI.Web.Icons.Material` (the web rendering).

- Rounded, filled and outlined, Apache-2.0. The filled drawing is what holds up at the sizes an icon beside
  small text actually gets.
- The pack ships **no stylesheet**. One 382 KB variable font travels in the assembly and the host builds the
  stylesheet at startup from the glyphs the application registered — `AddMaterialWebIcons(style, names…)`, or
  `MaterialIconScope.All` for a gallery. The whole set written out would be three megabytes of
  render-blocking CSS.
- `MaterialIcons` carries all 3 903 names as constants. Their values are prefixed `ms-`: a glyph class is
  global and the two sets share about a hundred names.
- **The demo is now a gallery over both sets**, opening on Material. A search box that matches the constant
  and the glyph alike, a selector for the set and one for Material's two drawings — all three resolved on the
  server, because the page is a windowed items view holding a hundred tiles of four thousand and reads the
  rest as they are scrolled to.

### Lucide

**Breaking.** The set now works the way Material does, and its names changed with it.

- The pack ships **no stylesheet**. The glyph table travels deflated in the assembly (646 KB of JSON in
  78 KB) and the host builds the stylesheet from what the application registered —
  `AddLucideWebIcons(names…)`, or `LucideIconScope.All` for a gallery.
- `LucideIcons` is no longer a curated hundred-odd semantic names. It carries **all 1 792** glyphs the set
  draws, generated, named for what Lucide calls them — so `Delete` is now `Trash2` and `Warning` is
  `TriangleAlert`. A pack ships glyphs, not meanings; naming "the icon for deleting" is the application’s
  own decision, and one it should make in one place.
- Values are prefixed `lu-`, as Material’s are prefixed `ms-`.
- The generated stylesheet lost the React `key` attributes it was shipping and the percent-encoding of every
  space and angle bracket: 94 KB → 57 KB for the old curated set.

## 1.0.0-preview.1

The first release: the **Lucide** set, as `NE.Standard.UI.Icons.Lucide` (the names) and
`NE.Standard.UI.Web.Icons.Lucide` (the web rendering, with the generated stylesheet embedded).

- **MIT**, unlike the framework it plugs into. An icon set is a table of names and a stylesheet.
- `LucideIcons` is a curated table of semantic names — `Delete` is `"trash-2"`, `Warning` is the triangle —
  so that "the delete icon" survives a change of mind about which glyph that is. Any Lucide slug can still be
  named directly.
- **A demo**, `examples/DemoApp.Icons`: one page showing every name in the set, enumerated off the package
  rather than listed by hand. It builds against the published framework, so it is also proof that the sets
  work against the version they claim to.
- Pre-release only because the framework is: these packages are useless without it, and shipping a 1.0.0 that
  only works against a preview would be a lie about how settled they are.
