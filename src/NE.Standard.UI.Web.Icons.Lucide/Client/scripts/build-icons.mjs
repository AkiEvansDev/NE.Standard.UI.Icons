// Builds the Lucide pack: a compressed glyph table for the assembly to embed, and the C# name constants
// beside it. Every name the set draws, not a curated hundred — which of them reaches a browser is what the
// application registers.
//
// Nothing here writes a stylesheet. 1 715 glyphs is about 700 KB of CSS, and an application draws tens of
// them; the host builds the CSS at startup from the glyphs that were asked for.

import { deflateRawSync } from "node:zlib";
import { mkdirSync, readdirSync, writeFileSync } from "node:fs";
import { pathToFileURL } from "node:url";
import { resolve } from "node:path";

const SOURCE = "node_modules/@lucide/icons/dist/esm/icons";
const DIST = "dist";
const NAMES = "../../NE.Standard.UI.Icons.Lucide/LucideIcons.cs";

// Reserved words a glyph name would collide with as a C# identifier. Everything else PascalCases cleanly.
const RESERVED = new Set(["Class", "Object", "String", "Double", "Switch", "Lock", "Default", "Event", "Base", "Checked", "Void"]);

// The `icons` directory is the canonical set: the barrel adds several thousand alias exports on top, all of
// them pointing at these same drawings.
async function read() {
    const files = readdirSync(SOURCE).filter(file => file.endsWith(".mjs") && file !== "index.mjs").sort();
    const table = {};

    for (const file of files) {
        const name = file.slice(0, -".mjs".length);
        const { default: icon } = await import(pathToFileURL(resolve(SOURCE, file)).href);

        if (!icon || !Array.isArray(icon.node))
            throw new Error(`Lucide icon '${name}' has no node data.`);

        table[name] = encodeSvg(renderIcon(icon));
    }

    return table;
}

// No `width`/`height`, only the `viewBox`. A glyph is drawn as a CSS mask sized `contain`, and an SVG that
// states a size has an *intrinsic* one: the browser rasterises it at 24px and scales that bitmap to whatever
// box the icon got, which is where the softness at larger sizes came from. Sizeless, the mask is rasterised at
// the box and the glyph is sharp at every size.
function renderIcon(icon) {
    const attributes = {
        xmlns: "http://www.w3.org/2000/svg",
        viewBox: "0 0 24 24",
        fill: "none",
        stroke: "currentColor",
        "stroke-width": "2",
        "stroke-linecap": "round",
        "stroke-linejoin": "round"
    };

    return `<svg ${renderAttributes(attributes)}>${icon.node.map(renderIconNode).join("")}</svg>`;
}

function renderIconNode(node) {
    const [tag, attributes] = node;

    return `<${tag} ${renderAttributes(attributes)} />`;
}

// `key` is React bookkeeping that ships with the icon data and means nothing in an SVG; it was a third of
// what each glyph cost once percent-encoded.
function renderAttributes(attributes) {
    return Object.entries(attributes)
        .filter(([key]) => key !== "key")
        .map(([key, value]) => `${key}='${escapeAttribute(String(value))}'`)
        .join(" ");
}

// Single-quoted attributes, so the double quote that closes the CSS string is the only one in play and no
// attribute value ever needs escaping for it. `&` and `<` still have to go — the file is parsed as XML.
function escapeAttribute(value) {
    return value
        .replaceAll("&", "&amp;")
        .replaceAll("'", "&apos;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;");
}

// Not encodeURIComponent: inside `url("…")` every character but the closing quote, a backslash and a newline
// is already legal, so escaping spaces, slashes, angle brackets and equals signs only made the file bigger.
// `%` has to lead the list — it is the escape character itself.
function encodeSvg(svg) {
    return svg
        .replaceAll("%", "%25")
        .replaceAll("#", "%23")
        .replaceAll("\"", "%22")
        .replaceAll("\n", "");
}

function toIdentifier(name) {
    const pascal = name
        .split(/[_-]+/)
        .filter(part => part.length > 0)
        .map(part => part[0].toUpperCase() + part.slice(1))
        .join("");

    const safe = /^[0-9]/.test(pascal) ? "N" + pascal : pascal;

    return RESERVED.has(safe) ? safe + "Icon" : safe;
}

const table = await read();

mkdirSync(DIST, { recursive: true });

const json = Buffer.from(JSON.stringify(table), "utf8");
// Raw deflate, not zlib-wrapped: .NET DeflateStream reads exactly this, and the wrapper it does not.
const packed = deflateRawSync(json, { level: 9 });

writeFileSync(resolve(DIST, "ui-icons-lucide.deflate"), packed);
console.log(`glyphs: ${Object.keys(table).length}, ${(json.length / 1024).toFixed(0)} KB -> ${(packed.length / 1024).toFixed(0)} KB`);

const seen = new Map();
const lines = [];

for (const name of Object.keys(table)) {
    const identifier = toIdentifier(name);

    if (seen.has(identifier))
        throw new Error(`Identifier '${identifier}' is produced by both '${seen.get(identifier)}' and '${name}'.`);

    seen.set(identifier, name);
    lines.push(`    public const string ${identifier} = "lu-${name}";`);
}

const source = `// <auto-generated />
// Built by Client/scripts/build-icons.mjs from @lucide/icons. Do not edit by hand.

namespace NE.Standard.UI.Icons.Lucide;

/// <summary>
/// Every Lucide name, as the string an <c>Icon</c> property takes. The <c>lu-</c> prefix is part of the
/// value: a glyph class is global, and Lucide and Material share a hundred names between them.
/// Which of these a page can actually draw is what the application registers — see <c>AddLucideWebIcons</c>.
/// </summary>
public static class LucideIcons
{
${lines.join("\n")}
}
`;

writeFileSync(resolve(NAMES), source, "utf8");
console.log(`names: ${lines.length} constants`);
