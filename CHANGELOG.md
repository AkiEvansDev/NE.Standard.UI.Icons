# Changelog

One section per release of this slice, headed `## X.Y.Z` and named by the tag — `icons/vX.Y.Z`. The release
workflow cuts the matching section out to become the body of the GitHub release, and a tag with no section
fails the release before anything is published.

## 1.4.0-rc.2

- **Built on the framework's 1.4.0-rc.2.** Nothing of this package's own changed; it moves with the framework.

## 1.4.0-rc.1

- **Built on the framework's 1.4.0-rc.1.** Nothing of this package's own changed; it moves with the framework. Its client
  declares `engines.node >= 24`, the version the builds use.

## 1.3.0

- **An outlined value registers as it is.** `AddMaterialWebIcons(MaterialIcons.Outlined(MaterialIcons.Save))` failed the host's
  start with "does not exist"; the pack now takes the suffix off and serves that glyph's outlined drawing — that one name's
  only, so one outlined button beside `MaterialIconScope.All` adds one rule rather than the whole outlined set (over half a
  megabyte). `MaterialIconRegistration.OutlinedNames` lists them; `MaterialIconStyle.Outlined` still serves every name's.
- **A name no registration covers draws nothing**, as the README always said — an outlined value whose drawing was not
  registered, a name from data, a misspelt `ne-` mark — rather than a filled square in the text colour: the core's icon
  now paints a tinted picture alone.
- **A glyph's rule is the class the framework's icon value wears**, built by `WebIconClassName.FromIconName` rather than a prefix
  of the pack's own, and a test holds each of the three properties it sets (`--ui-icon-font`, `--ui-icon-glyph`,
  `--ui-icon-fill`) to one the core's `.ui-icon::before` reads. It no longer writes `--ui-icon-paint`, which the core stopped
  reading.
- **The font is preloaded by every page's head** (the framework's 1.3.0), fetched once and cached for a year under its
  versioned address, so a glyph is not a blank box for an extra round trip on a cold load.
- **A registration with no drawing fails beside an outlined value too.** `Add(0, MaterialIcons.Settings,
  MaterialIcons.Outlined(MaterialIcons.Save))`, or the whole set with no style beside an outlined value, started the host and
  left every name but the outlined one without a rule; only an outlined value now draws without a style.
- A glyph is silent to a screen reader: a titled button is named by its title, not "save Save changes".
- A misspelt name's startup error names the glyph as Material names it (`'settingz'`), whatever prefix or suffix it was written
  with.
- The generator refuses a Material glyph whose own name ends in `_outlined`, which the outlined suffix would take for its own.
- The README's size of the whole set is the measured one — over half a megabyte of CSS per drawing, not three megabytes — and
  a test holds the font's size in it to the embedded font.
- The demo's search box clears with the field's own cross instead of a button of its own, and a tile writes the value its
  constant holds under the name instead of in a tooltip on the glyph; both lines wrap, so a long name or value reads whole.
- The demo speaks Chinese as well: a language switcher in the header and a whole zh-Hans table of its own words and the
  framework's; a tile's name and value are content, shown as written in every language, and the count under the search box is
  a phrase of the page's language.
- The demo's gallery lays six tiles to a line on a wide screen rather than eight (four, then two, narrower), so a long name's
  caption no longer breaks in the middle of a word; the search, the style select and the count wrap to a second line on a narrow
  screen rather than running off it.

## 1.2.0

- **Built on the framework's 1.2.0.** Nothing of this package's own changed; it moves with the framework.

## 1.1.0

- **Built on the framework's 1.1.0.** Nothing of this package's own changed; it moves with the framework, which now
  releases every package on the next minor version whenever it changes.

## 1.0.1

- **The first stable release.** No `--prerelease` is needed any more. Until 2.0.0 the public surface may still move
  between versions; every such change is marked **Breaking:** in this file.
- **Breaking: the Lucide packages are no longer released.** `NE.Standard.UI.Icons.Lucide` and
  `NE.Standard.UI.Web.Icons.Lucide` stop at `1.0.0-rc.3`, deprecated and frozen since then; this slice is Material Symbols
  alone. An application on them moves to Material: `AddMaterialWebIcons` in place of `AddLucideWebIcons`, and the
  `MaterialIcons` constants in place of `LucideIcons` — the names differ, so each glyph is chosen again. The demo's `/lucide` page goes with them.
- **The icon stylesheet and the Material font are served under `/_ne/`** — `/_ne/css/ui-icons-material.css` and
  `/_ne/fonts/ui-icons-material.woff2` — with every path of the framework's own (see the core's
  changelog), so an application's routes and a proxy's rules have one prefix to leave alone. A page links them itself; a host
  that named the old `/css/` or `/fonts/` paths, in a CSP or a cache rule, names the new ones.
- **The Material packages bring their namespaces as global usings.** Installing them is enough to write
  `MaterialIcons.Settings` and `AddMaterialWebIcons(...)`; a project that would rather write its own `using` lines sets
  `NEStandardUIImplicitUsings` to `false`.
- **The mirror's demo builds against the framework's packages.** It reached this slice's own namespaces only through
  the monorepo's usings, and this slice's sources wrote `using` lines the framework's packages now bring, which is
  IDE0005; `Directory.Build.targets` travels to the mirror and a package's sources keep their own lines.
- The README's licence link names the mirror, so it resolves on nuget.org too.
- **The packages carry their symbols and sources inside their assemblies**, so a debugger steps into them.
- **Material Symbols 0.47.5**: 24 new glyphs in `MaterialIcons` and the font (`AppsPlus`, `FilterPlus`, `MarkdownDocument`…); none removed.
- **A misspelt glyph fails at startup beside the whole set too.** A name registered alongside `MaterialIconScope.All`
  was never checked; it now throws when the stylesheet is built, as it does without `All`.

## 1.0.0-rc.3

The glyphs are unchanged since `1.0.0-preview.2`; what moved is how a set registers itself.

- Both web packages keep what an application registered through the framework's own package registration
  (`WebPackageRegistration`), one scaffold in place of a copy in each set, so they need the framework's `1.0.0-rc.3`.
- **The Lucide packages are deprecated and frozen.** Lucide ships no font, and a library that draws its icons two ways is
  a divergence; they stay published for the applications already on them and follow no new Lucide release. A new
  application takes Material Symbols.
- The demo is an application project and a web host, a page per set, in the shell every add-on demo wears.

## 1.0.0-rc.2

Nothing but the number: the sets are unchanged since `1.0.0-preview.2`. The slices go out together, and a set
left a number behind reads as if it had been dropped from the release.

## 1.0.0-rc.1

Nothing but the number: the sets are unchanged since `1.0.0-preview.2`. The framework and the packages that
plug into it go out as one release candidate, and a set left a number behind reads as if it had been dropped.

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
