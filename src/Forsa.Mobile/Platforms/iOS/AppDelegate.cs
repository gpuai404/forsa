using Foundation;

namespace Forsa.Mobile;

[Register("AppDelegate")]
public class IosApplication : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
