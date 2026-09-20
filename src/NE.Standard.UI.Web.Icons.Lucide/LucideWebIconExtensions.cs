using System;
using Microsoft.Extensions.DependencyInjection;
using NE.Standard.UI.Web.Abstractions.Assets;

namespace NE.Standard.UI.Web.Icons.Lucide;

/// <summary>
/// Asks for a glyph rather than for a set.
/// </summary>
public enum LucideIconScope
{
    /// <summary>Every glyph the pack knows — roughly 700 KB of stylesheet.</summary>
    All
}

public static class LucideWebIconExtensions
{
    /// <summary>
    /// Serves the named Lucide glyphs. Callable multiple times — each feature can register its own icons, and
    /// the pack builds one stylesheet from all of them.
    /// </summary>
    public static IServiceCollection AddLucideWebIcons(this IServiceCollection services, params string[] names)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = Register(services).Add(names);

        return services;
    }

    /// <summary>
    /// Serves the whole set — for a gallery, or when icon names come from data and can't be named at
    /// registration time.
    /// </summary>
    public static IServiceCollection AddLucideWebIcons(this IServiceCollection services, LucideIconScope scope)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = scope == LucideIconScope.All
            ? Register(services).AddEverything()
            : throw new ArgumentOutOfRangeException(nameof(scope));

        return services;
    }

    /// <summary>
    /// One registration and one asset however many times the application calls in; the stylesheet builds from
    /// a factory, so it's written after every call, not the first.
    /// </summary>
    private static LucideIconRegistration Register(IServiceCollection services)
    {
        LucideIconRegistration registration = WebPackageRegistration.GetOrAdd(services, static () => new LucideIconRegistration(), out var added);

        if (!added)
            return registration;

        _ = services.AddSingleton(_ => new WebAssetDescriptor
        {
            Key = "ui-icons-lucide.css",
            Kind = UIWebAssetKind.Css,
            SourceKind = UIWebAssetSourceKind.Content,
            Source = "lucide",
            Content = LucideIconStylesheet.Build(registration),
            PublicPath = "/css/ui-icons-lucide.css",
            Order = 100
        });

        return registration;
    }
}
