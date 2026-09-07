using System;
using System.Linq;
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
    /// Serves the named Lucide glyphs. Call it as many times as suits the application — a feature can ask for
    /// its own icons where it is registered, and the pack builds one stylesheet from all of it.
    /// </summary>
    public static IServiceCollection AddLucideWebIcons(this IServiceCollection services, params string[] names)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = Register(services).Add(names);

        return services;
    }

    /// <summary>
    /// Serves the whole set. For a gallery, and for an application whose icon names come from data — the one
    /// case a registration cannot cover, because nothing knows the names before they arrive.
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
    /// One registration and one asset however many times the application calls in — the stylesheet is built
    /// from a factory, so it is written after every call has had its say rather than at the first.
    /// </summary>
    private static LucideIconRegistration Register(IServiceCollection services)
    {
        LucideIconRegistration? registration = (LucideIconRegistration?)services
            .FirstOrDefault(descriptor => descriptor.ServiceType == typeof(LucideIconRegistration))?
            .ImplementationInstance;

        if (registration is not null)
            return registration;

        registration = new LucideIconRegistration();

        _ = services.AddSingleton(registration);
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
