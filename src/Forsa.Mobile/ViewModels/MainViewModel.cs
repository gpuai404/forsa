using CommunityToolkit.Mvvm.ComponentModel;
using Forsa.Mobile.Controls;
using Forsa.Mobile.Resources.Strings;

namespace Forsa.Mobile.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public IReadOnlyList<TabBarItem> Tabs { get; } =
    [
        new(AppStrings.DiscoverTab, new TabBarIcon("magnifyingglass", "\ue8b6")),
        new(AppStrings.ActivityTab, new TabBarIcon("flame", "\uef55")),
        new(AppStrings.ResumeAiTab, new TabBarIcon("bookmark", "\ue866")),
        new(AppStrings.ProfileTab, new TabBarIcon("person", "\ue7fd")),
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
