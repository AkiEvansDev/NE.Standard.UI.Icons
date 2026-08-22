# NE.Standard.UI.Icons

Icon sets for the [NE.Standard](https://github.com/AkiEvansDev/NE.Standard) UI framework. One set is two
packages: the **names**, which are platform-independent, and the **rendering** for a platform, which carries
the generated stylesheet embedded in its assembly.

## Install

```
dotnet add package NE.Standard.UI.Icons.Lucide --prerelease
dotnet add package NE.Standard.UI.Web.Icons.Lucide --prerelease
```

| Set | Names | Web |
|---|---|---|
| [Lucide](https://lucide.dev) | [`NE.Standard.UI.Icons.Lucide`](https://www.nuget.org/packages/NE.Standard.UI.Icons.Lucide) | [`NE.Standard.UI.Web.Icons.Lucide`](https://www.nuget.org/packages/NE.Standard.UI.Web.Icons.Lucide) |

## Using a set

Register the platform package once, then name icons from the set:

```csharp
services.AddLucideWebIcons();
```

```csharp
new TextComponent()
    .SetIcon(LucideIcons.Warning)
    .SetIconColor(UIThemeColor.Danger)
    .SetTitle("Delete project?");

new ButtonComponent()
    .ConfigureDefaultContent(c => c.SetIcon(LucideIcons.Save).SetTitle("Save changes"))
    .OnClick(nameof(Controller.Save));
```

`LucideIcons` is a curated table of **semantic** names over Lucide's own slugs — `LucideIcons.Delete` is
`"trash-2"`, `LucideIcons.Warning` is the triangle. Anything Lucide ships can also be named by its slug
directly; the constants exist so that "the delete icon" survives a change of mind about which glyph that is.

They are `const string`, so the compiler inlines them and a project that only names icons carries no runtime
dependency on the names package at all.

## The demo

One page, every name in the set, read off the package itself so it cannot fall behind the table it documents.

```
dotnet run --project examples/DemoApp.Icons     # http://localhost:5000
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
