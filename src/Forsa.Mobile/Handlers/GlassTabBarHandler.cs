using Forsa.Mobile.Controls;
using Microsoft.Maui.Handlers;

namespace Forsa.Mobile.Handlers;

public sealed partial class GlassTabBarHandler : ViewHandler<GlassTabBar, GlassTabBarView>
{
    public static readonly PropertyMapper<GlassTabBar, GlassTabBarHandler> PropertyMapper =
        new(ViewMapper)
        {
            [nameof(GlassTabBar.Items)] = MapItems,
            [nameof(GlassTabBar.SelectedIndex)] = MapSelectedIndex,
            [nameof(GlassTabBar.ActiveColor)] = MapColors,
            [nameof(GlassTabBar.InactiveColor)] = MapColors,
            [nameof(GlassTabBar.HighlightColor)] = MapColors,
            [nameof(GlassTabBar.SurfaceColor)] = MapColors,
        };

    public GlassTabBarHandler()
        : base(PropertyMapper)
    {
    }

    protected override void ConnectHandler(GlassTabBarView platformView)
    {
        base.ConnectHandler(platformView);
        platformView.ItemInvoked += OnItemInvoked;
        platformView.KeyboardVisibilityChanged += OnKeyboardVisibilityChanged;
    }

    protected override void DisconnectHandler(GlassTabBarView platformView)
    {
        platformView.ItemInvoked -= OnItemInvoked;
        platformView.KeyboardVisibilityChanged -= OnKeyboardVisibilityChanged;
        platformView.Cleanup();
        base.DisconnectHandler(platformView);
    }

    static void MapItems(GlassTabBarHandler handler, GlassTabBar view)
    {
        handler.PlatformView.SetItems(view.Items);
        handler.PlatformView.SetSelectedIndex(view.SelectedIndex, animate: false);
    }

    static void MapSelectedIndex(GlassTabBarHandler handler, GlassTabBar view) =>
        handler.PlatformView.SetSelectedIndex(view.SelectedIndex, handler.PlatformView.CanAnimateSelection);

    static void MapColors(GlassTabBarHandler handler, GlassTabBar view) =>
        handler.PlatformView.SetColors(view.ActiveColor, view.InactiveColor, view.HighlightColor, view.SurfaceColor);

    void OnItemInvoked(object? sender, int index) => VirtualView.NotifyItemInvoked(index);

    void OnKeyboardVisibilityChanged(object? sender, bool visible) =>
        VirtualView.NotifyKeyboardVisibilityChanged(visible);
}
