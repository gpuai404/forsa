using Forsa.Mobile.Views;

namespace Forsa.Mobile;

public sealed class AppShell : Shell
{
    public AppShell(MainPage mainPage)
    {
        Items.Add(new ShellContent { Title = "Forsa", Content = mainPage });
    }
}
