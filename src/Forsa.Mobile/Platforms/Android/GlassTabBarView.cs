#pragma warning disable CA1001, CA2213

using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using Forsa.Mobile.Controls;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Navigation;
using Microsoft.Maui.Platform;
using MauiColor = Microsoft.Maui.Graphics.Color;

namespace Forsa.Mobile.Handlers;

/// <summary>
/// Lightweight floating-layout host for Material's native <see cref="BottomNavigationView"/>.
/// </summary>
public sealed class GlassTabBarView : FrameLayout
{
    const float MinSurfaceAlpha = 0.94f;
    const int FirstItemId = 1;
    const int AlwaysShowLabels = 1;

    readonly float _density;
    readonly GradientDrawable _background = new();
    readonly BottomNavigationView _tabBar;
    readonly ItemSelectedListener _selectedListener;
    readonly ItemReselectedListener _reselectedListener;
    bool _keyboardVisible;
    bool _suppressSelectionEvents;
    int _itemCount;

    public GlassTabBarView(Context context)
        : base(context)
    {
        _density = context.Resources?.DisplayMetrics?.Density ?? 1f;
        _selectedListener = new ItemSelectedListener(this);
        _reselectedListener = new ItemReselectedListener(this);
        _tabBar = new BottomNavigationView(context)
        {
            Background = _background,
            Elevation = Px(6),
            ItemHorizontalTranslationEnabled = false,
            LabelVisibilityMode = AlwaysShowLabels,
        };
        _tabBar.SetOnItemSelectedListener(_selectedListener);
        _tabBar.SetOnItemReselectedListener(_reselectedListener);
        AddView(_tabBar);
        SetClipChildren(false);
        SetClipToPadding(false);
    }

    public event EventHandler<int>? ItemInvoked;

    public event EventHandler<bool>? KeyboardVisibilityChanged;

    public bool CanAnimateSelection => IsLaidOut && _tabBar.IsLaidOut;

    public void SetItems(IReadOnlyList<TabBarItem>? items)
    {
        _suppressSelectionEvents = true;
        try
        {
            var menu = _tabBar.Menu;
            menu.Clear();
            _itemCount = items?.Count ?? 0;

            if (items is not null)
            {
                for (var index = 0; index < items.Count; index++)
                {
                    var menuItem = menu.Add(IMenu.None, FirstItemId + index, index, items[index].Title);
                    var icon = ContextCompat.GetDrawable(Context, IconResource(items[index].Icon));
                    if (menuItem is not null && icon is not null)
                    {
                        menuItem.SetIcon(icon);
                    }
                }
            }
        }
        finally
        {
            _suppressSelectionEvents = false;
        }

        RequestLayout();
    }

    public void SetSelectedIndex(int index, bool animate)
    {
        if (index >= 0 && index < _itemCount)
        {
            _suppressSelectionEvents = true;
            try
            {
                _tabBar.SelectedItemId = FirstItemId + index;
            }
            finally
            {
                _suppressSelectionEvents = false;
            }
        }
    }

    public void SetColors(MauiColor active, MauiColor inactive, MauiColor highlight, MauiColor surface)
    {
        var states = new[]
        {
            new[] { Android.Resource.Attribute.StateChecked },
            new[] { -Android.Resource.Attribute.StateChecked },
        };
        var tint = new ColorStateList(states, new[]
        {
            active.ToPlatform().ToArgb(),
            inactive.ToPlatform().ToArgb(),
        });
        _tabBar.ItemIconTintList = tint;
        _tabBar.ItemTextColor = tint;
        _tabBar.ItemActiveIndicatorColor = ColorStateList.ValueOf(highlight.ToPlatform());
        _tabBar.ItemActiveIndicatorEnabled = true;

        var opaqueSurface = surface.Alpha < MinSurfaceAlpha ? surface.WithAlpha(MinSurfaceAlpha) : surface;
        _background.SetColor(opaqueSurface.ToPlatform());
        Invalidate();
    }

    public void Cleanup()
    {
        _tabBar.SetOnItemSelectedListener(null);
        _tabBar.SetOnItemReselectedListener(null);
    }

    protected override void OnAttachedToWindow()
    {
        base.OnAttachedToWindow();
        if (ViewTreeObserver is { } observer)
        {
            observer.GlobalLayout += OnGlobalLayout;
        }
    }

    protected override void OnDetachedFromWindow()
    {
        if (ViewTreeObserver is { IsAlive: true } observer)
        {
            observer.GlobalLayout -= OnGlobalLayout;
        }

        base.OnDetachedFromWindow();
    }

    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
        var width = MeasureSpec.GetMode(widthMeasureSpec) == MeasureSpecMode.Unspecified
            ? Resources?.DisplayMetrics?.WidthPixels ?? 0
            : MeasureSpec.GetSize(widthMeasureSpec);
        var barWidth = Px(GlassTabBarMetrics.BarWidth(width / _density));
        var barHeight = Px(GlassTabBarMetrics.BarHeight);
        _tabBar.Measure(
            MeasureSpec.MakeMeasureSpec(barWidth, MeasureSpecMode.Exactly),
            MeasureSpec.MakeMeasureSpec(barHeight, MeasureSpecMode.Exactly));
        SetMeasuredDimension(width, Px(GlassTabBarMetrics.ReservedHeight));
    }

    protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
    {
        var width = right - left;
        var height = bottom - top;
        var barWidth = _tabBar.MeasuredWidth;
        var barHeight = _tabBar.MeasuredHeight;
        var barLeft = (width - barWidth) / 2;
        var barTop = (height - barHeight) / 2;
        _tabBar.Layout(barLeft, barTop, barLeft + barWidth, barTop + barHeight);
        _background.SetCornerRadius(barHeight / 2f);
    }

    int Px(double dp) => (int)Math.Round(dp * _density);

    static int IconResource(TabBarIcon icon) => icon.MaterialGlyph switch
    {
        "\ue8b6" => Resource.Drawable.ic_tab_discover,
        "\uef55" => Resource.Drawable.ic_tab_activity,
        "\ue866" => Resource.Drawable.ic_tab_resume,
        "\ue7fd" => Resource.Drawable.ic_tab_profile,
        _ => Resource.Drawable.ic_tab_discover,
    };

    void OnGlobalLayout(object? sender, EventArgs e)
    {
        var insets = AndroidX.Core.View.ViewCompat.GetRootWindowInsets(this);
        var visible = insets?.IsVisible(AndroidX.Core.View.WindowInsetsCompat.Type.Ime()) ?? false;
        if (visible == _keyboardVisible)
        {
            return;
        }

        _keyboardVisible = visible;
        KeyboardVisibilityChanged?.Invoke(this, visible);
    }

    sealed class ItemSelectedListener(GlassTabBarView owner) : Java.Lang.Object, NavigationBarView.IOnItemSelectedListener
    {
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (owner._suppressSelectionEvents)
            {
                return true;
            }

            var index = item.ItemId - FirstItemId;
            if (index < 0 || index >= owner._itemCount)
            {
                return false;
            }

            owner.ItemInvoked?.Invoke(owner, index);
            return true;
        }
    }

    sealed class ItemReselectedListener(GlassTabBarView owner) : Java.Lang.Object, NavigationBarView.IOnItemReselectedListener
    {
        public void OnNavigationItemReselected(IMenuItem item)
        {
            if (owner._suppressSelectionEvents)
            {
                return;
            }

            var index = item.ItemId - FirstItemId;
            if (index >= 0 && index < owner._itemCount)
            {
                owner.ItemInvoked?.Invoke(owner, index);
            }
        }
    }
}
