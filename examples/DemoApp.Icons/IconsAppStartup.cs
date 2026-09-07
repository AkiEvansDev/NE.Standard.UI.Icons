using System;
using NE.Standard.UI.Application;
using NE.Standard.UI.Startup;

namespace DemoApp.Icons;

internal sealed class IconsAppStartup : UIStartupBase
{
    protected override void ConfigureApplication(UIApplicationBuilder application)
    {
        ArgumentNullException.ThrowIfNull(application);

        _ = application.Route<IconsView, IconsController>("/");
    }
}
