using System.Globalization;
using System.Resources;

namespace Forsa.Mobile.Resources.Strings;

public static class AppStrings
{
    private static readonly ResourceManager Resources = new("Forsa.Mobile.Resources.Strings.AppStrings", typeof(AppStrings).Assembly);

    private static string Get(string name) =>
        Resources.GetString(name, CultureInfo.CurrentUICulture) ??
        Resources.GetString(name, CultureInfo.InvariantCulture) ??
        throw new MissingManifestResourceException($"Missing resource: {name}");
    public static string AppName => Get(nameof(AppName));
    public static string DiscoverTab => Get(nameof(DiscoverTab));
    public static string ActivityTab => Get(nameof(ActivityTab));
    public static string ResumeAiTab => Get(nameof(ResumeAiTab));
    public static string ProfileTab => Get(nameof(ProfileTab));
}
