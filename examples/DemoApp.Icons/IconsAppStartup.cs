using System;
using NE.Standard.UI.Application;
using NE.Standard.UI.Startup;

namespace DemoApp.Icons;

public sealed class IconsAppStartup : UIStartupBase
{
    protected override void ConfigureApplication(UIApplicationBuilder application)
    {
        ArgumentNullException.ThrowIfNull(application);

        _ = application.Route<MaterialView, MaterialIconsController>(IconsDemoView.MaterialRoute);
        _ = application.Route<LucideView, LucideIconsController>(IconsDemoView.LucideRoute);
    }
}
