using System;
using Microsoft.Extensions.DependencyInjection;
using NE.Standard.UI.Web.Icons.Lucide;
using NE.Standard.UI.Web.Icons.Material;
using NE.Standard.UI.Web.Renderers.DI;
using NE.Standard.UI.Web.Startup;

namespace DemoApp.Icons;

internal sealed class IconsWebStartup : WebStartupBase<IconsAppStartup>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddStandardRenderers();

        // The gallery is the one case a registration cannot cover: it draws every name there is, in both of
        // Material's drawings — about a megabyte and a half of stylesheet that no application would ask for.
        _ = services.AddMaterialWebIcons(MaterialIconScope.All, MaterialIconStyle.Fill | MaterialIconStyle.Outlined);
        _ = services.AddLucideWebIcons(LucideIconScope.All);
    }
}
