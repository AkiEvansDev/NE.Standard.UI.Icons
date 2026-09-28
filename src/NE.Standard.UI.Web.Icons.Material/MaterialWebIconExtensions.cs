using System;
using Microsoft.Extensions.DependencyInjection;
using NE.Standard.UI.Web.Abstractions.Assets;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// Asks for the whole set rather than naming glyphs.
/// </summary>
public enum MaterialIconScope
{
    /// <summary>Every glyph the pack knows — around half a megabyte of stylesheet per style.</summary>
    All
}

/// <summary>Registers the Material Symbols pack with a web host.</summary>
public static class MaterialWebIconExtensions
{
    private const string FontKey = "ui-icons-material.woff2";

    private const string FontPath = "/_ne/fonts/ui-icons-material.woff2";

    /// <summary>
    /// Serves the named Material glyphs. Callable multiple times — each feature can register its own icons,
    /// and the pack builds one stylesheet from all of them.
    /// </summary>
    public static IServiceCollection AddMaterialWebIcons(this IServiceCollection services, MaterialIconStyle style, params string[] names)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = Register(services).Add(style, names);

        return services;
    }

    /// <inheritdoc cref="AddMaterialWebIcons(IServiceCollection, MaterialIconStyle, string[])"/>
    public static IServiceCollection AddMaterialWebIcons(this IServiceCollection services, params string[] names)
        => services.AddMaterialWebIcons(MaterialIconStyle.Fill, names);

    /// <summary>
    /// Serves the whole set — for a gallery, or when icon names come from data and can't be named at
    /// registration time.
    /// </summary>
    public static IServiceCollection AddMaterialWebIcons(this IServiceCollection services, MaterialIconScope scope, MaterialIconStyle style = MaterialIconStyle.Fill)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = scope == MaterialIconScope.All
            ? Register(services).AddEverything(style)
            : throw new ArgumentOutOfRangeException(nameof(scope));

        return services;
    }

    /// <summary>
    /// One registration and two assets however many times the application calls in — the font, and the
    /// stylesheet that reaches it.
    /// </summary>
    private static MaterialIconRegistration Register(IServiceCollection services)
    {
        MaterialIconRegistration registration = WebPackageRegistration.GetOrAdd(services, static () => new MaterialIconRegistration(), out var added);

        if (!added)
            return registration;

        // The font is its own asset, not a data URI in the stylesheet — 386 KB of woff2 becomes 515 KB of
        // base64, uncacheable apart from rules that change with every registration.
        WebAssetDescriptor font = new()
        {
            Key = FontKey,
            Kind = UIWebAssetKind.Font,
            SourceKind = UIWebAssetSourceKind.EmbeddedResource,
            Source = "NE.Standard.UI.Web.Icons.Material.Client.dist.ui-icons-material.woff2",
            ResourceAssemblyName = typeof(MaterialWebIconExtensions).Assembly.GetName().Name,
            PublicPath = FontPath,
            Order = 100
        };

        _ = services.AddSingleton(font);

        // A factory, so the stylesheet is written after every AddMaterialWebIcons call has had its say rather
        // than at the first.
        _ = services.AddSingleton(_ => new WebAssetDescriptor
        {
            Key = "ui-icons-material.css",
            Kind = UIWebAssetKind.Css,
            SourceKind = UIWebAssetSourceKind.Content,
            Source = "material-symbols-rounded",
            Content = MaterialIconStylesheet.Build(registration, font.ResolveVersionedPublicPath()),
            PublicPath = "/_ne/css/ui-icons-material.css",
            Order = 101
        });

        return registration;
    }
}
