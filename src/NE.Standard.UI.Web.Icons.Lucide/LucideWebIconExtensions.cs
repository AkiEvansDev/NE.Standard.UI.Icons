using System;
using Microsoft.Extensions.DependencyInjection;
using NE.Standard.UI.Web.Abstractions.Assets;

namespace NE.Standard.UI.Web.Icons.Lucide;

public static class LucideWebIconExtensions
{
    public static IServiceCollection AddLucideWebIcons(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddSingleton(new WebAssetDescriptor
        {
            Key = "ui-icons-lucide.css",
            Kind = UIWebAssetKind.Css,
            SourceKind = UIWebAssetSourceKind.EmbeddedResource,
            Source = "NE.Standard.UI.Web.Icons.Lucide.Client.dist.ui-icons-lucide.css",
            ResourceAssemblyName = "NE.Standard.UI.Web.Icons.Lucide",
            PublicPath = "/css/ui-icons-lucide.css",
            Order = 100
        });

        return services;
    }
}
