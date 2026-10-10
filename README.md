# NE.Standard.UI.Icons

Material Symbols for the [NE.Standard](https://github.com/AkiEvansDev/NE.Standard) UI framework. The set is two
packages: the **names**, which are platform-independent, and the **rendering** for a platform, which carries
the font embedded in its assembly and builds a stylesheet from what the application asks for.

## Install

```
dotnet add package NE.Standard.UI.Icons.Material
dotnet add package NE.Standard.UI.Web.Icons.Material
```

Both packages bring their namespaces as global usings, so the code below needs no `using` line for them; a project
that sets `NEStandardUIImplicitUsings` to `false` writes its own.

| Set | Drawing | Glyphs | Names | Web |
|---|---|---|---|---|
| [Material Symbols](https://fonts.google.com/icons) | Rounded, filled and outlined | 3 927 | [`NE.Standard.UI.Icons.Material`](https://www.nuget.org/packages/NE.Standard.UI.Icons.Material) | [`NE.Standard.UI.Web.Icons.Material`](https://www.nuget.org/packages/NE.Standard.UI.Web.Icons.Material) |

The set is complete, and the package ships no stylesheet: the whole of it written out would be over half a megabyte
of CSS per drawing, over a megabyte for both, while an application draws tens of glyphs. The font ships in the
assembly instead — every glyph, 386 KB, preloaded by every page's head and cached for a year under its versioned address, so a
glyph is not a blank box on a cold load — and the stylesheet a browser receives names only what the application registered.

The two drawings are one font, told apart by its `FILL` axis. A filled glyph survives being asked for at 12 or 14
pixels — it has no stroke to run out of pixels — so reach for Fill where an icon sits next to small text, and for
Outlined where it stands on its own at a size that carries it.

## Using the set

The pack asks which glyphs, because it will only write rules for those:

```csharp
services.AddMaterialWebIcons(
    MaterialIconStyle.Fill,
    MaterialIcons.Settings,
    MaterialIcons.Search,
    MaterialIcons.Delete);

// A gallery, or an application whose icon names arrive in data and cannot be listed at startup:
services.AddMaterialWebIcons(MaterialIconScope.All, MaterialIconStyle.Fill);
```

Call it as often as suits the application — a feature can ask for its own icons where it is registered, and
one stylesheet is built from all of it when the host starts. A name the pack does not know throws there and
then, rather than rendering as nothing — beside `MaterialIconScope.All` too.

```csharp
new TextComponent()
    .SetIcon(MaterialIcons.Warning)
    .SetIconColor(UIThemeColor.Danger)
    .SetTitle("Delete project?");

new ButtonComponent()
    .SetIcon(MaterialIcons.Outlined(MaterialIcons.Save))
    .SetTitle("Save changes")
    .OnClick(nameof(Controller.Save));
```

The constants carry Material's own name behind a prefix — `MaterialIcons.Save` is `"ms-save"`, and its outlined
drawing `"ms-save-outlined"`, served for every registered name when the application registered
`MaterialIconStyle.Outlined`. Registering the outlined value itself — `AddMaterialWebIcons(MaterialIcons.Outlined(MaterialIcons.Save))`
— serves that one glyph's outlined drawing and no other name's, so one outlined button beside `MaterialIconScope.All`
adds one rule, not the whole outlined set. A name nothing registered draws nothing. A glyph class is global; the
prefix keeps it apart from the framework's own `ne-` marks and from an application's own classes.

They are `const string`, so the compiler inlines them and a project that only names icons carries no runtime
dependency on the names package at all. A pack names glyphs, not meanings: "the icon for deleting" is the
application's decision, and a `const string` of its own is the place to make it once.

## The demo

Every name in the set, read off the package itself so it cannot fall behind the table it documents, with a
selector for the two drawings; the search box matches the constant and the glyph alike.

Four thousand tiles are not a page, so the gallery is a **windowed** items view: it holds a hundred at a
time and reads the next as they are scrolled to, and the search runs on the server — the browser only ever
had a hundred names to look through. It is also the one case a registration cannot cover, so the demo asks
for the whole set, in both drawings.

The header's language switcher turns the page into Chinese and back; a tile's name and value are what an author
copies, so they stay as written in every language.

```
dotnet run --project examples/DemoApp.Icons.Web     # http://localhost:5110
```

## Licence

**MIT** — see [LICENSE](https://github.com/AkiEvansDev/NE.Standard.UI.Icons/blob/main/LICENSE). The icon set is a table of names and the code that writes its stylesheet, and there is no reason
for it to carry a licence anyone has to read.

**This does not make the framework MIT.** These packages are only useful inside
[NE.Standard](https://github.com/AkiEvansDev/NE.Standard), which is under the Prosperity Public License
3.0.0 — free for noncommercial use, with a thirty-day trial for commercial use. Using the icons commercially
means licensing the framework.

The Material Symbols font itself is Google's, under the Apache License 2.0; the web package carries its text
(`LICENSE-material-symbols.txt`, `THIRD-PARTY-NOTICES.md`).

## Contributing

This repository is a **read-only mirror**. Development happens in a private repository alongside the
framework — that is how the set stays in step with the renderer it plugs into — and everything here is
generated from it, so pull requests are switched off.

Issues are open and welcome.
