using System;
using Microsoft.Extensions.DependencyInjection;
// The host takes the framework's namespaces as global usings; the words coverage test, which compiles this file too, has none.
#if DEMO_WORDS_COVERAGE
using NE.Standard.UI.Web.Icons.Material;
using NE.Standard.UI.Web.Renderers.DI;
#endif

namespace DemoApp.Icons.Web;

/// <summary>DemoApp.Icons.Web's registrations, which DemoWordsCoverageTests makes too: each package that brings words is tested as the host adds it.</summary>
internal static class IconsWebServices
{
    public static void Register(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddStandardRenderers();

        // The gallery is the one case a registration cannot cover: it draws every name there is, in both of
        // Material's drawings — over a megabyte of stylesheet that no application would ask for.
        _ = services.AddMaterialWebIcons(MaterialIconScope.All, MaterialIconStyle.Fill | MaterialIconStyle.Outlined);
    }
}
