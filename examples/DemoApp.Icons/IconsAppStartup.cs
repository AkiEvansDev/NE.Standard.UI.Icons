using System;

namespace DemoApp.Icons;

public sealed class IconsAppStartup : UIStartupBase
{
    protected override void ConfigureApplication(UIApplicationBuilder application)
    {
        ArgumentNullException.ThrowIfNull(application);

        _ = application.Route<MaterialView, IconsController>(IconsDemoView.MaterialRoute);
    }
}
