using System;

namespace DemoApp.Icons;

public sealed class IconsAppStartup : UIStartupBase
{
    protected override void ConfigureApplication(UIApplicationBuilder application)
    {
        ArgumentNullException.ThrowIfNull(application);

        _ = application.AddLocalizationSource(IconsDemoWords.Build());

        // Only a string starting "icons-demo." is a key: every other string on a translatable property is content, so the
        // missing-word report in Development names only words the demo has not translated.
        _ = application.ConfigureLocalization(options => options.KeyPrefixes.Add(IconsDemoWords.KeyPrefix));

        _ = application.Route<MaterialView, IconsController>(IconsDemoView.MaterialRoute);
    }
}
