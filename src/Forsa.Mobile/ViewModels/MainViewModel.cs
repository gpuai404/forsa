using CommunityToolkit.Mvvm.ComponentModel;
using Forsa.Mobile.Controls;
using Forsa.Mobile.Resources.Strings;

namespace Forsa.Mobile.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public IReadOnlyList<TabBarItem> Tabs { get; } =
    [
        new(AppStrings.DiscoverTab, TabIcon.Discover),
        new(AppStrings.ActivityTab, TabIcon.Activity),
        new(AppStrings.ResumeAiTab, TabIcon.ResumeAi),
        new(AppStrings.ProfileTab, TabIcon.Profile),
    ];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDiscover))]
    [NotifyPropertyChangedFor(nameof(IsActivity))]
    [NotifyPropertyChangedFor(nameof(IsResumeAi))]
    [NotifyPropertyChangedFor(nameof(IsProfile))]
    private int _selectedIndex;

    public bool IsDiscover => SelectedIndex == 0;
    public bool IsActivity => SelectedIndex == 1;
    public bool IsResumeAi => SelectedIndex == 2;
    public bool IsProfile  => SelectedIndex == 3;
}
