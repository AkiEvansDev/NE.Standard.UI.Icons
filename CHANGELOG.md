# Changelog

One section per release of this slice, headed `## X.Y.Z` and named by the tag — `icons/vX.Y.Z`. The release
workflow cuts the matching section out to become the body of the GitHub release, and a tag with no section
fails the release before anything is published.

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
