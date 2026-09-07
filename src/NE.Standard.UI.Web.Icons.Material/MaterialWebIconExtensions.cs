using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NE.Standard.UI.Web.Abstractions.Assets;

namespace NE.Standard.UI.Web.Icons.Material;

/// <summary>
/// Asks for a glyph rather than for a set.
/// </summary>
public enum MaterialIconScope
{
    /// <summary>Every glyph the pack knows — around half a megabyte of stylesheet per style.</summary>
    All
}

public static class MaterialWebIconExtensions
{
    private const string FontKey = "ui-icons-material.woff2";

    private const string FontPath = "/fonts/ui-icons-material.woff2";

    /// <summary>
    /// Serves the named Material glyphs. Call it as many times as suits the application — a feature can ask
    /// for its own icons where it is registered, and the pack builds one stylesheet from all of it.
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
    /// Serves the whole set. For a gallery, and for an application whose icon names come from data — the one
    /// case a registration cannot cover, because nothing knows the names before they arrive.
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
        MaterialIconRegistration? registration = (MaterialIconRegistration?)services
            .FirstOrDefault(descriptor => descriptor.ServiceType == typeof(MaterialIconRegistration))?
            .ImplementationInstance;

        if (registration is not null)
            return registration;

        registration = new MaterialIconRegistration();

        _ = services.AddSingleton(registration);

        // The font is its own asset rather than a data URI in the stylesheet: 382 KB of woff2 is 509 KB of
        // base64, and inlined it can never be cached apart from the rules that change with every registration.
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
            PublicPath = "/css/ui-icons-material.css",
            Order = 101
        });

        return registration;
    }
}
