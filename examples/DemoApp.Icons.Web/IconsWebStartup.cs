using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Icons.Web;

internal sealed class IconsWebStartup : WebStartupBase<IconsAppStartup>
{
    protected override void ConfigureServices(IServiceCollection services)
        => IconsWebServices.Register(services);
}
