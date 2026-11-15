using System;

namespace DemoApp.Icons.Desktop;

internal static class Program
{
    [STAThread]
    private static int Main(string[] args)
        => DesktopStartupBuilder.Run<IconsDesktopStartup, IconsAppStartup>(args);
}
