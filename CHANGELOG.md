# Changelog

This slice's changelog. It holds only what is not released yet, under `## X.Y.Z` (the tag is `icons/vX.Y.Z`): the release
workflow cuts that section out as the body of the GitHub release (a tag with no section fails the release), and the notes of every
released version live there — https://github.com/AkiEvansDev/NE.Standard.UI.Icons/releases.

## 1.7.2

- **Built on the framework's 1.7.2.** The packages' own content is unchanged.
- The font builds under `SkipWebClientBuild` as every client does (`SkipWebIconBuild` is gone); it is committed, so a machine
  without Node builds the slice (#153).
- The demo's search box and drawing select are `Tonal`, a filter band over the gallery; on a phone the search box takes the line,
  and a tile's name is in the ink over its muted value.
