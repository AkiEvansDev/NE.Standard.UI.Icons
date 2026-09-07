# NE.Standard.UI.Icons

Icon sets for the [NE.Standard](https://github.com/AkiEvansDev/NE.Standard) UI framework. One set is two
packages: the **names**, which are platform-independent, and the **rendering** for a platform, which carries
the glyph table embedded in its assembly and builds a stylesheet from what the application asks for.

## Install

```
dotnet add package NE.Standard.UI.Icons.Lucide --prerelease
dotnet add package NE.Standard.UI.Web.Icons.Lucide --prerelease
```

| Set | Drawing | Glyphs | Names | Web |
|---|---|---|---|---|
| [Lucide](https://lucide.dev) | Lines, 2 px on a 24 grid | 1 792 | [`NE.Standard.UI.Icons.Lucide`](https://www.nuget.org/packages/NE.Standard.UI.Icons.Lucide) | [`NE.Standard.UI.Web.Icons.Lucide`](https://www.nuget.org/packages/NE.Standard.UI.Web.Icons.Lucide) |
| [Material Symbols](https://fonts.google.com/icons) | Rounded, filled and outlined | 3 903 | [`NE.Standard.UI.Icons.Material`](https://www.nuget.org/packages/NE.Standard.UI.Icons.Material) | [`NE.Standard.UI.Web.Icons.Material`](https://www.nuget.org/packages/NE.Standard.UI.Web.Icons.Material) |

Both sets are complete, and neither ships a stylesheet: the whole of Lucide is 700 KB of CSS and the whole of
Material three megabytes, while an application draws tens of glyphs. The glyph table ships in the assembly
instead, and what reaches a browser is what the application registered.

The choice between them is the drawing. A filled glyph survives being asked for at 12 or 14 pixels — it has
no stroke to run out of pixels — so reach for Material Fill where an icon sits next to small text, and for a
line set where it stands on its own.

## Using a set

A pack asks which glyphs, because it will only serve those:

```csharp
services.AddLucideWebIcons(LucideIcons.Settings, LucideIcons.Search, LucideIcons.Trash2);

services.AddMaterialWebIcons(
    MaterialIconStyle.Fill,
    MaterialIcons.Settings,
    MaterialIcons.Search,
    MaterialIcons.Delete);

// A gallery, or an application whose icon names arrive in data and cannot be listed at startup:
services.AddLucideWebIcons(LucideIconScope.All);
services.AddMaterialWebIcons(MaterialIconScope.All, MaterialIconStyle.Fill);
```

Call it as often as suits the application — a feature can ask for its own icons where it is registered, and
one stylesheet is built from all of it when the host starts. A name the pack does not know throws there and
then, rather than rendering as nothing.

```csharp
new TextComponent()
    .SetIcon(LucideIcons.TriangleAlert)
    .SetIconColor(UIThemeColor.Danger)
    .SetTitle("Delete project?");

new ButtonComponent()
    .ConfigureDefaultContent(c => c.SetIcon(MaterialIcons.Save).SetTitle("Save changes"))
    .OnClick(nameof(Controller.Save));
```

The constants carry the set's own name behind a prefix — `LucideIcons.TriangleAlert` is
`"lu-triangle-alert"`, `MaterialIcons.Save` is `"ms-save"`. A glyph class is global and the two sets share a
hundred names between them; the prefix is what lets one page draw both.

They are `const string`, so the compiler inlines them and a project that only names icons carries no runtime
dependency on the names package at all. A pack names glyphs, not meanings: "the icon for deleting" is the
application's decision, and a `const string` of its own is the place to make it once.

## The demo

One page over both sets, read off the packages themselves so it cannot fall behind the tables it documents.
Material is what it opens on; a selector switches the set, another switches Material between its two
drawings, and the search box matches the constant and the glyph alike.

Four thousand tiles are not a page, so the gallery is a **windowed** items view: it holds a hundred at a
time and reads the next as they are scrolled to, and the search runs on the server — the browser only ever
had a hundred names to look through. It is also the one case a registration cannot cover, so the demo asks
for both sets whole, and for both of Material's drawings.

```
dotnet run --project examples/DemoApp.Icons     # http://localhost:5300
```

## License

**MIT** — see [LICENSE](LICENSE). The icon sets are a table of names and a stylesheet, and there is no reason
for them to carry a licence anyone has to read.

**This does not make the framework MIT.** These packages are only useful inside
[NE.Standard](https://github.com/AkiEvansDev/NE.Standard), which is under the Prosperity Public License
3.0.0 — free for noncommercial use, with a thirty-day trial for commercial use. Using the icons commercially
means licensing the framework.

The Lucide glyphs themselves are Lucide's, under the
[ISC license](https://github.com/lucide-icons/lucide/blob/main/LICENSE).

## Contributing

This repository is a **read-only mirror**. Development happens in a private repository alongside the
framework — that is how a set stays in step with the renderer it plugs into — and everything here is
generated from it, so pull requests are switched off.

Issues are open and welcome, and a request for a set that is not here yet is a good one.
