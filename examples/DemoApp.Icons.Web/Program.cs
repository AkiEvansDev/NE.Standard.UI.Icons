using DemoApp.Icons;
using DemoApp.Icons.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

#if DEBUG
builder.Logging.SetMinimumLevel(LogLevel.Debug);
#else
builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endif

WebStartupBuilder.Configure<IconsWebStartup, IconsAppStartup>(builder.Services);

WebApplication app = builder.Build();

await app.MapStandardUIWebAsync().ConfigureAwait(false);

await app.RunAsync().ConfigureAwait(false);
