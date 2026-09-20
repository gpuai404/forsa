namespace Forsa.Mobile;

public partial class App : Microsoft.Maui.Controls.Application
{
    private readonly Func<AppShell> _createShell;

    public App(Func<AppShell> createShell)
    {
        InitializeComponent();
        _createShell = createShell;
    }

    protected override Window CreateWindow(IActivationState? activationState) => new(_createShell());
}
