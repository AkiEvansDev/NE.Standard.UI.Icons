using System;
using System.Collections.Generic;

namespace DemoApp.Icons;

/// <summary>
/// The demo's words in its two languages, its own under <see cref="KeyPrefix"/>; the framework's are the tables it ships, so the
/// missing-word report in Development names only a real gap (DemoWordsCoverageTests). The icons' names and values are content and
/// stay as written.
/// </summary>
internal static class IconsDemoWords
{
    /// <summary>What every key of the demo starts with; every other string is content (<c>KeyPrefixes</c>).</summary>
    public const string KeyPrefix = "icons-demo.";

    private static readonly Dictionary<string, string> English = new(StringComparer.Ordinal)
    {
        ["icons-demo.material.title"] = "Material Symbols",
        ["icons-demo.material.description"] = "Every name in the set, read off the package itself. A tile's caption is the constant an author writes, and the line under it the value it resolves to.",
        ["icons-demo.search.placeholder"] = "Search by name or glyph…",
        ["icons-demo.style.filled"] = "Filled",
        ["icons-demo.style.outlined"] = "Outlined",
        ["icons-demo.caption.all.one"] = "{count} name in Material Symbols.",
        ["icons-demo.caption.all.other"] = "{count} names in Material Symbols.",
        ["icons-demo.caption.matches.one"] = "{count} of {whole} names in Material Symbols matches “{query}”.",
        ["icons-demo.caption.matches.other"] = "{count} of {whole} names in Material Symbols match “{query}”.",
        ["icons-demo.empty"] = "No name matches."
    };

    // Chinese has one plural form, so a plural key has only its ".other".
    private static readonly Dictionary<string, string> Chinese = new(StringComparer.Ordinal)
    {
        ["icons-demo.material.title"] = "Material Symbols",
        ["icons-demo.material.description"] = "图标集中的每个名称，直接读取自包本身。图块的标题是作者编写的常量，其下一行是它解析得到的值。",
        ["icons-demo.search.placeholder"] = "按名称或字形搜索…",
        ["icons-demo.style.filled"] = "填充",
        ["icons-demo.style.outlined"] = "描边",
        ["icons-demo.caption.all.other"] = "Material Symbols 共有 {count} 个名称。",
        ["icons-demo.caption.matches.other"] = "Material Symbols 的 {whole} 个名称中有 {count} 个匹配“{query}”。",
        ["icons-demo.empty"] = "没有匹配的名称。"
    };

    public static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Build()
        => new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.Ordinal)
        {
            ["en"] = English,
            ["zh-Hans"] = Chinese
        };
}
