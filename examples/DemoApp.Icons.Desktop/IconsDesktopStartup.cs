using System;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Icons.Desktop;

internal sealed class IconsDesktopStartup : DesktopStartupBase<IconsAppStartup>
{
    protected override void ConfigureHost(DesktopHostOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        options.WindowTitle = "NE.Standard — Icons";
        options.InitialWidth = 1400;
        options.InitialHeight = 880;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddStandardRenderers();
        _ = services.AddMaterialWebIcons(MaterialIconScope.All, MaterialIconStyle.Fill | MaterialIconStyle.Outlined);
    }
}
