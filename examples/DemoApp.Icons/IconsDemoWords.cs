using System;
using System.Collections.Generic;

namespace DemoApp.Icons;

/// <summary>
/// The demo's words in its two languages: its own under <see cref="KeyPrefix"/>, and in the second language every framework word it
/// registers, so the missing-word report in Development names only a real gap (DemoWordsCoverageTests). The icons' names and values
/// are content and stay as written.
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

    // The framework ships English only; an application brings every other language, the framework's words too. Chinese has one
    // plural form, so a plural key has only its ".other".
    private static readonly Dictionary<string, string> Chinese = new(StringComparer.Ordinal)
    {
        ["icons-demo.material.title"] = "Material Symbols",
        ["icons-demo.material.description"] = "图标集中的每个名称，直接读取自包本身。图块的标题是作者编写的常量，其下一行是它解析得到的值。",
        ["icons-demo.search.placeholder"] = "按名称或字形搜索…",
        ["icons-demo.style.filled"] = "填充",
        ["icons-demo.style.outlined"] = "描边",
        ["icons-demo.caption.all.other"] = "Material Symbols 共有 {count} 个名称。",
        ["icons-demo.caption.matches.other"] = "Material Symbols 的 {whole} 个名称中有 {count} 个匹配“{query}”。",
        ["icons-demo.empty"] = "没有匹配的名称。",

        ["ui.picker.today"] = "今天",
        ["ui.picker.now"] = "现在",
        ["ui.picker.clear"] = "清除",
        ["ui.picker.done"] = "完成",
        ["ui.picker.previous"] = "上一个",
        ["ui.picker.next"] = "下一个",
        ["ui.picker.hours"] = "小时",
        ["ui.picker.minutes"] = "分钟",
        ["ui.picker.seconds"] = "秒",
        ["ui.picker.meridiem"] = "上午/下午",
        ["ui.picker.start"] = "开始",
        ["ui.picker.end"] = "结束",
        ["ui.picker.open"] = "打开选择器",
        ["ui.notification.close"] = "关闭",
        ["ui.tabs.more"] = "更多标签页",
        ["ui.commandbar.more"] = "更多命令",
        ["ui.tab.close"] = "关闭",
        ["ui.tab.rename"] = "重命名",
        ["ui.tab.pin"] = "固定",
        ["ui.tab.unpin"] = "取消固定",
        ["ui.tab.delete"] = "删除",
        ["ui.select.clear"] = "清除选择",
        ["ui.select.placeholder"] = "请选择…",
        ["ui.select.remove"] = "移除 {label}",
        ["ui.input.clear"] = "清除",
        ["ui.breadcrumbs.label"] = "面包屑导航",
        ["ui.file.uploading"] = "正在上传… {percent}%",
        ["ui.file.count"] = "{count} 个文件",
        ["ui.file.failed"] = "上传失败。",
        ["ui.file.oversized"] = "文件太大。",
        ["ui.file.choose"] = "选择文件",
        ["ui.color.picker"] = "取色器",
        ["ui.color.palette"] = "调色板",
        ["ui.color.hex"] = "十六进制",
        ["ui.color.red"] = "R",
        ["ui.color.green"] = "G",
        ["ui.color.blue"] = "B",
        ["ui.color.factor"] = "系数",
        ["ui.color.opacity"] = "不透明度",
        ["ui.color.choose"] = "选择颜色",
        ["ui.splitter.label"] = "调整大小",
        ["ui.split.more"] = "更多操作",
        ["ui.image.choose"] = "选择图片",
        ["ui.image.change"] = "更换图片",
        ["ui.image.remove"] = "移除图片",
        ["ui.row.edit"] = "编辑",
        ["ui.row.save"] = "保存",
        ["ui.row.cancel"] = "取消",
        ["ui.table.resize"] = "调整列宽",
        ["ui.tree.toggle"] = "展开或折叠",
        ["ui.tree.loading"] = "正在加载…",
        ["ui.items.empty"] = "没有可显示的内容。",
        ["ui.collapse.toggle"] = "展开或折叠",
        ["ui.side.open"] = "打开侧栏",
        ["ui.menu.search"] = "搜索",
        ["ui.theme.switch"] = "切换主题",
        ["ui.language.switch"] = "切换语言，{language}",
        ["ui.notfound.title"] = "404",
        ["ui.notfound.description"] = "你要找的页面不存在。",
        ["ui.notfound.page"] = "页面不存在",
        ["ui.error.title"] = "出了点问题",
        ["ui.error.message"] = "出了点问题，请重试。",
        ["ui.command.refused"] = "你无权执行此操作。",
        ["ui.command.failed"] = "出了点问题，请重试。",
        ["ui.value.format"] = "值的格式不对。",
        ["ui.connection.lost"] = "与服务器的连接已断开。请重新加载页面以继续。",
        ["ui.connection.reload"] = "重新加载",

        ["ui.color.name.iron-fog"] = "铁雾",
        ["ui.color.name.silver-night"] = "银夜",
        ["ui.color.name.bronze-dusk"] = "青铜暮色",
        ["ui.color.name.stellar-red"] = "星红",
        ["ui.color.name.nebula-rose"] = "星云玫瑰",
        ["ui.color.name.lunar-pink"] = "月光粉",
        ["ui.color.name.solar-amber"] = "太阳琥珀",
        ["ui.color.name.nebula-gold"] = "星云金",
        ["ui.color.name.lunar-yellow"] = "月光黄",
        ["ui.color.name.eclipse-olive"] = "日食橄榄",
        ["ui.color.name.nebula-lime"] = "星云青柠",
        ["ui.color.name.lunar-sage"] = "月光鼠尾草",
        ["ui.color.name.aurora-green"] = "极光绿",
        ["ui.color.name.nebula-mint"] = "星云薄荷",
        ["ui.color.name.lunar-fern"] = "月光蕨",
        ["ui.color.name.astral-teal"] = "星青",
        ["ui.color.name.nebula-cyan"] = "星云青",
        ["ui.color.name.lunar-moss"] = "月光苔",
        ["ui.color.name.quantum-blue"] = "量子蓝",
        ["ui.color.name.nebula-aqua"] = "星云水蓝",
        ["ui.color.name.lunar-azure"] = "月光天蓝",
        ["ui.color.name.nova-purple"] = "新星紫",
        ["ui.color.name.nebula-violet"] = "星云紫罗兰",
        ["ui.color.name.lunar-lavender"] = "月光薰衣草",
        ["ui.color.name.comet"] = "彗星",
        ["ui.color.name.flare"] = "耀斑",
        ["ui.color.name.ember"] = "余烬",
        ["ui.color.name.photon"] = "光子",
        ["ui.color.name.vortex"] = "漩涡",
        ["ui.color.name.halo"] = "光环"
    };

    public static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Build()
        => new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.Ordinal)
        {
            ["en"] = English,
            ["zh-Hans"] = Chinese
        };
}
