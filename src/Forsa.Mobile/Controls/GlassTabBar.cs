using System.Windows.Input;

namespace Forsa.Mobile.Controls;

/// <summary>
/// Cross-platform contract for the native floating tab bar.
/// </summary>
public sealed class GlassTabBar : View
{
    public const double ReservedHeight = GlassTabBarMetrics.ReservedHeight;

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items), typeof(IReadOnlyList<TabBarItem>), typeof(GlassTabBar), null,
        propertyChanged: (bindable, _, _) => ((GlassTabBar)bindable).CoerceValue(SelectedIndexProperty));

    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
        nameof(SelectedIndex), typeof(int), typeof(GlassTabBar), 0, BindingMode.TwoWay,
        coerceValue: (bindable, value) => ((GlassTabBar)bindable).CoerceSelectedIndex((int)value));

    public static readonly BindableProperty ActiveColorProperty = BindableProperty.Create(
        nameof(ActiveColor), typeof(Color), typeof(GlassTabBar), Color.FromArgb("#0057B8"));

    public static readonly BindableProperty InactiveColorProperty = BindableProperty.Create(
        nameof(InactiveColor), typeof(Color), typeof(GlassTabBar), Color.FromArgb("#667085"));

    public static readonly BindableProperty HighlightColorProperty = BindableProperty.Create(
        nameof(HighlightColor), typeof(Color), typeof(GlassTabBar), Color.FromArgb("#1F0057B8"));

    public static readonly BindableProperty SurfaceColorProperty = BindableProperty.Create(
        nameof(SurfaceColor), typeof(Color), typeof(GlassTabBar), Color.FromArgb("#66FFFFFF"));

    public static readonly BindableProperty TabReselectedCommandProperty = BindableProperty.Create(
        nameof(TabReselectedCommand), typeof(ICommand), typeof(GlassTabBar));

    public GlassTabBar()
    {
        HeightRequest = ReservedHeight;
        VerticalOptions = LayoutOptions.End;
    }

    public IReadOnlyList<TabBarItem>? Items
    {
        get => (IReadOnlyList<TabBarItem>?)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    public Color ActiveColor
    {
        get => (Color)GetValue(ActiveColorProperty);
        set => SetValue(ActiveColorProperty, value);
    }

    public Color InactiveColor
    {
        get => (Color)GetValue(InactiveColorProperty);
        set => SetValue(InactiveColorProperty, value);
    }

    public Color HighlightColor
    {
        get => (Color)GetValue(HighlightColorProperty);
        set => SetValue(HighlightColorProperty, value);
    }

    public Color SurfaceColor
    {
        get => (Color)GetValue(SurfaceColorProperty);
        set => SetValue(SurfaceColorProperty, value);
    }

    public ICommand? TabReselectedCommand
    {
        get => (ICommand?)GetValue(TabReselectedCommandProperty);
        set => SetValue(TabReselectedCommandProperty, value);
    }

    public event EventHandler<int>? TabReselected;

    internal void NotifyItemInvoked(int index)
    {
        if (Items is not { } items || index < 0 || index >= items.Count)
        {
            return;
        }

        if (index != SelectedIndex)
        {
            SelectedIndex = index;
            return;
        }

        TabReselected?.Invoke(this, index);
        if (TabReselectedCommand is { } command && command.CanExecute(index))
        {
            command.Execute(index);
        }
    }

    internal void NotifyKeyboardVisibilityChanged(bool visible)
    {
        Opacity = visible ? 0 : 1;
        InputTransparent = visible;
    }

    int CoerceSelectedIndex(int index)
    {
        var count = Items?.Count ?? 0;
        return count == 0 ? Math.Max(index, 0) : Math.Clamp(index, 0, count - 1);
    }
}
