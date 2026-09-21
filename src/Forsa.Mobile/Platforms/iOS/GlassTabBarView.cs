#pragma warning disable CA1001, CA2213

using CoreGraphics;
using Forsa.Mobile.Controls;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace Forsa.Mobile.Handlers;

/// <summary>
/// Lightweight floating-layout host for UIKit's native <see cref="UITabBar"/>.
/// </summary>
public sealed class GlassTabBarView : UIView
{
    readonly UITabBar _tabBar;
    readonly NativeTabBarDelegate _delegate;
    readonly List<UITabBarItem> _items = [];
    UIColor _surfaceColor = UIColor.Clear;
    int _selectedIndex;
    NSObject? _keyboardShow;
    NSObject? _keyboardHide;

    public GlassTabBarView()
    {
        BackgroundColor = UIColor.Clear;
        _delegate = new NativeTabBarDelegate(this);
        _tabBar = new UITabBar
        {
            Delegate = _delegate,
            ItemPositioning = UITabBarItemPositioning.Fill,
            Translucent = true,
        };
        _tabBar.ClipsToBounds = true;
        AddSubview(_tabBar);
        ApplyAppearance();
    }

    public event EventHandler<int>? ItemInvoked;

    public event EventHandler<bool>? KeyboardVisibilityChanged;

    public bool CanAnimateSelection => Window is not null && Bounds.Width > 0;

    public void SetItems(IReadOnlyList<TabBarItem>? items)
    {
        _tabBar.SetItems([], animated: false);
        foreach (var item in _items)
        {
            item.Dispose();
        }

        _items.Clear();
        if (items is not null)
        {
            foreach (var item in items)
            {
                var normal = UIImage.GetSystemImage(item.Icon.SfSymbol);
                var selected = UIImage.GetSystemImage(item.Icon.SfSymbol + ".fill") ?? normal;
                _items.Add(new UITabBarItem(item.Title, normal, selected));
            }
        }

        _tabBar.SetItems([.. _items], animated: false);
        ApplySelectedItem();
    }

    public void SetSelectedIndex(int index, bool animate)
    {
        _selectedIndex = index;
        ApplySelectedItem();
    }

    public void SetColors(Color active, Color inactive, Color highlight, Color surface)
    {
        _tabBar.TintColor = active.ToPlatform();
        _tabBar.UnselectedItemTintColor = inactive.ToPlatform();
        _surfaceColor = surface.ToPlatform();
        ApplyAppearance();
    }

    public void Cleanup()
    {
        StopObservingKeyboard();
        _tabBar.Delegate = null;
    }

    public override CGSize SizeThatFits(CGSize size) =>
        new(double.IsFinite(size.Width) ? size.Width : 0, (nfloat)GlassTabBarMetrics.ReservedHeight);

    public override void MovedToWindow()
    {
        base.MovedToWindow();
        if (Window is null)
        {
            StopObservingKeyboard();
            return;
        }

        _keyboardShow ??= UIKeyboard.Notifications.ObserveWillShow((_, _) => KeyboardVisibilityChanged?.Invoke(this, true));
        _keyboardHide ??= UIKeyboard.Notifications.ObserveWillHide((_, _) => KeyboardVisibilityChanged?.Invoke(this, false));
    }

    public override UIView? HitTest(CGPoint point, UIEvent? uievent)
    {
        var hit = base.HitTest(point, uievent);
        return ReferenceEquals(hit, this) ? null : hit;
    }

    public override void LayoutSubviews()
    {
        base.LayoutSubviews();
        var barWidth = GlassTabBarMetrics.BarWidth(Bounds.Width);
        _tabBar.Frame = new CGRect(
            (Bounds.Width - barWidth) / 2,
            GlassTabBarMetrics.OuterPadding,
            barWidth,
            GlassTabBarMetrics.BarHeight);
        _tabBar.Layer.CornerRadius = (nfloat)(GlassTabBarMetrics.BarHeight / 2);
    }

    void ApplyAppearance()
    {
        // iOS 26 supplies Liquid Glass as UITabBar's native appearance. Earlier releases use
        // UITabBarAppearance's system material rather than a separately composed effect view.
        if (OperatingSystem.IsIOSVersionAtLeast(26))
        {
            return;
        }

        var appearance = new UITabBarAppearance();
        appearance.ConfigureWithTransparentBackground();
        appearance.BackgroundEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.SystemThinMaterial);
        appearance.BackgroundColor = _surfaceColor;
        appearance.ShadowColor = UIColor.Clear;
        _tabBar.StandardAppearance = appearance;
        _tabBar.ScrollEdgeAppearance = appearance;
    }

    void ApplySelectedItem()
    {
        _tabBar.SelectedItem = _selectedIndex >= 0 && _selectedIndex < _items.Count
            ? _items[_selectedIndex]
            : null;
    }

    void OnItemSelected(UITabBarItem item)
    {
        var index = _items.IndexOf(item);
        if (index >= 0)
        {
            ItemInvoked?.Invoke(this, index);
        }
    }

    void StopObservingKeyboard()
    {
        _keyboardShow?.Dispose();
        _keyboardShow = null;
        _keyboardHide?.Dispose();
        _keyboardHide = null;
    }

    sealed class NativeTabBarDelegate(GlassTabBarView owner) : UITabBarDelegate
    {
        public override void ItemSelected(UITabBar tabbar, UITabBarItem item) => owner.OnItemSelected(item);
    }
}
