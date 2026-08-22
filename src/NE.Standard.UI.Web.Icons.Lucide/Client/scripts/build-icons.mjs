import { mkdirSync, writeFileSync } from "node:fs";
import { resolve } from "node:path";
import {
    // Actions

    Plus, Minus, X, Check, Ban,
    Pencil, Trash2, Save, Copy,
    Undo2, Redo2, RefreshCw,

    // Navigation

    ArrowUp, ArrowDown, ArrowLeft, ArrowRight, ArrowUpDown,
    ChevronUp, ChevronDown, ChevronLeft, ChevronRight,
    Expand, Shrink, Maximize2, Minimize2,
    ExternalLink, Link,

    // Search & Filter

    Search, SearchX, SearchCheck,
    Filter, FilterX,
    Funnel, SlidersHorizontal, ListFilter,

    // Status

    Info, TriangleAlert, CircleX, CircleCheck, CircleHelp,
    AlertCircle, CircleAlert,
    LoaderCircle, CircleDashed,
    BadgeCheck, BadgeX,

    // Selection

    CheckCheck,
    Circle, CircleDot,
    Square, SquareCheck, SquareX,
    PlusCircle, MinusCircle,

    // Files

    File, FileText, Files,
    FileInput, FileOutput, FileSearch,
    Folder, FolderOpen, FolderSearch,
    Image, Paperclip,
    Upload, Download,

    // Time

    Calendar, Clock, History,

    // Users & Security

    User, UserRound, UserCheck, UserX,
    Users,
    Lock, LockOpen,
    Key, Fingerprint,
    Shield,

    // Visibility

    Eye, EyeOff,

    // Communication

    Mail, Phone, MessageSquare, Bell,
    Send, Inbox,

    // Media

    Play, Pause,

    // Layout

    Menu,
    PanelLeft, PanelRight, PanelTop, PanelBottom,
    Sidebar,
    Rows3, Columns3, Grid3X3,
    Table, List, ListChecks,
    LayoutDashboard,

    // Charts

    ChartBar, ChartLine, ChartPie,

    // Data

    Database, Server,

    // Network

    Globe, Wifi, WifiOff,

    // Auth

    LogIn, LogOut,

    // Packages

    Package, Archive, Boxes,

    // Clipboard

    Clipboard, ClipboardCheck, ClipboardX,

    // Development

    Code, Terminal, Bug, Wrench,

    // Locations

    MapPin, Navigation,

    // Commerce

    CreditCard, Wallet,

    // Devices

    Monitor, Smartphone, Tablet,

    // Appearance

    Settings, Settings2,
    Palette, Languages,
    Sun, Moon,

    // Misc

    House,
    Ellipsis, EllipsisVertical,
    Bookmark, Tag,
    Heart, Star, Sparkles,
    Printer
} from "@lucide/icons";

const icons = {
    // Actions

    "plus": Plus,
    "minus": Minus,
    "x": X,
    "check": Check,
    "ban": Ban,

    "pencil": Pencil,
    "trash-2": Trash2,
    "save": Save,
    "copy": Copy,

    "undo-2": Undo2,
    "redo-2": Redo2,
    "refresh-cw": RefreshCw,

    // Navigation

    "arrow-up": ArrowUp,
    "arrow-down": ArrowDown,
    "arrow-left": ArrowLeft,
    "arrow-right": ArrowRight,
    "arrow-up-down": ArrowUpDown,

    "chevron-up": ChevronUp,
    "chevron-down": ChevronDown,
    "chevron-left": ChevronLeft,
    "chevron-right": ChevronRight,

    "expand": Expand,
    "shrink": Shrink,
    "maximize-2": Maximize2,
    "minimize-2": Minimize2,

    "external-link": ExternalLink,
    "link": Link,

    // Search & Filter

    "search": Search,
    "search-x": SearchX,
    "search-check": SearchCheck,

    "filter": Filter,
    "filter-x": FilterX,

    "funnel": Funnel,
    "sliders-horizontal": SlidersHorizontal,
    "list-filter": ListFilter,

    // Status

    "info": Info,
    "triangle-alert": TriangleAlert,
    "circle-x": CircleX,
    "circle-check": CircleCheck,
    "circle-help": CircleHelp,

    "alert-circle": AlertCircle,
    "circle-alert": CircleAlert,

    "loader-circle": LoaderCircle,
    "circle-dashed": CircleDashed,

    "badge-check": BadgeCheck,
    "badge-x": BadgeX,

    // Selection

    "check-check": CheckCheck,

    "circle": Circle,
    "circle-dot": CircleDot,

    "square": Square,
    "square-check": SquareCheck,
    "square-x": SquareX,

    "plus-circle": PlusCircle,
    "minus-circle": MinusCircle,

    // Files

    "file": File,
    "file-text": FileText,
    "files": Files,

    "file-input": FileInput,
    "file-output": FileOutput,
    "file-search": FileSearch,

    "folder": Folder,
    "folder-open": FolderOpen,
    "folder-search": FolderSearch,

    "image": Image,
    "paperclip": Paperclip,

    "upload": Upload,
    "download": Download,

    // Time

    "calendar": Calendar,
    "clock": Clock,
    "history": History,

    // Users & Security

    "user": User,
    "user-round": UserRound,
    "user-check": UserCheck,
    "user-x": UserX,

    "users": Users,

    "lock": Lock,
    "lock-open": LockOpen,

    "key": Key,
    "fingerprint": Fingerprint,

    "shield": Shield,

    // Visibility

    "eye": Eye,
    "eye-off": EyeOff,

    // Communication

    "mail": Mail,
    "phone": Phone,
    "message-square": MessageSquare,
    "bell": Bell,

    "send": Send,
    "inbox": Inbox,

    // Media

    "play": Play,
    "pause": Pause,

    // Layout

    "menu": Menu,

    "panel-left": PanelLeft,
    "panel-right": PanelRight,
    "panel-top": PanelTop,
    "panel-bottom": PanelBottom,

    "sidebar": Sidebar,

    "rows-3": Rows3,
    "columns-3": Columns3,
    "grid-3x3": Grid3X3,

    "table": Table,
    "list": List,
    "list-checks": ListChecks,

    "layout-dashboard": LayoutDashboard,

    // Charts

    "chart-bar": ChartBar,
    "chart-line": ChartLine,
    "chart-pie": ChartPie,

    // Data

    "database": Database,
    "server": Server,

    // Network

    "globe": Globe,
    "wifi": Wifi,
    "wifi-off": WifiOff,

    // Auth

    "log-in": LogIn,
    "log-out": LogOut,

    // Packages

    "package": Package,
    "archive": Archive,
    "boxes": Boxes,

    // Clipboard

    "clipboard": Clipboard,
    "clipboard-check": ClipboardCheck,
    "clipboard-x": ClipboardX,

    // Development

    "code": Code,
    "terminal": Terminal,
    "bug": Bug,
    "wrench": Wrench,

    // Locations

    "map-pin": MapPin,
    "navigation": Navigation,

    // Commerce

    "credit-card": CreditCard,
    "wallet": Wallet,

    // Devices

    "monitor": Monitor,
    "smartphone": Smartphone,
    "tablet": Tablet,

    // Appearance

    "settings": Settings,
    "settings-2": Settings2,

    "palette": Palette,
    "languages": Languages,

    "sun": Sun,
    "moon": Moon,

    // Misc

    "house": House,

    "ellipsis": Ellipsis,
    "ellipsis-vertical": EllipsisVertical,

    "bookmark": Bookmark,
    "tag": Tag,

    "heart": Heart,
    "star": Star,
    "sparkles": Sparkles,

    "printer": Printer
};

function renderIcon(icon) {
    if (!icon || !Array.isArray(icon.node))
        throw new Error("Invalid Lucide icon data.");

    const attributes = {
        xmlns: "http://www.w3.org/2000/svg",
        width: "24",
        height: "24",
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

function renderAttributes(attributes) {
    return Object.entries(attributes)
        .map(([key, value]) => `${key}="${escapeHtml(String(value))}"`)
        .join(" ");
}

function escapeHtml(value) {
    return value
        .replaceAll("&", "&amp;")
        .replaceAll("\"", "&quot;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;");
}

function toDataUri(svg) {
    return `url("data:image/svg+xml,${encodeURIComponent(svg)}")`;
}

const lines = [
    "/* Generated Lucide icon pack for NE.Standard.UI.Web. */",
    ""
];

for (const [name, icon] of Object.entries(icons)) {
    if (!icon)
        throw new Error(`Lucide icon '${name}' was not imported correctly.`);

    lines.push(`.ui-icon-glyph--${name} { --ui-icon-url: ${toDataUri(renderIcon(icon))}; }`);
}

const dist = resolve("dist");
mkdirSync(dist, { recursive: true });
writeFileSync(resolve(dist, "ui-icons-lucide.css"), lines.join("\n"), "utf8");
