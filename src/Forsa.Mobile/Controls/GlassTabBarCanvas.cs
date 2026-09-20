namespace Forsa.Mobile.Controls;

public enum TabIcon { Discover, Activity, ResumeAi, Profile }

public sealed record TabBarItem(string Title, TabIcon Icon);

/// <summary>
/// Glass surface drawn with Microsoft.Maui.Graphics and animated font glyphs in MAUI Labels.
/// </summary>
public class GlassTabBarCanvas : ContentView
{
    public const double BarHeight = 64;

    const float HMargin = 20;     // minimum capsule side margin
    const float MaxBarWidth = 440;
    const float TopPad = 10;      // room for the soft shadow
    const float BottomPad = 4;    // extra gap above the system safe area
    const float Inset = 5;        // space between capsule and bubble
    const float IconSize = 24;
    const float IconGap = 3;
    const float LabelHeight = 14;
    const float IconLabelHeight = 28;
    const double IconTop = TopPad + (BarHeight - IconSize - IconGap - LabelHeight) / 2
        - (IconLabelHeight - IconSize) / 2;

    static readonly Color Active = Color.FromArgb("#0057B8");
    static readonly Color Inactive = Color.FromArgb("#667085");
    static readonly Color BubbleFill = Color.FromArgb("#1F0057B8");
    static readonly Color Surface = Color.FromArgb("#66FFFFFF");
    static readonly Color Rim = Color.FromArgb("#99FFFFFF");
    readonly GraphicsView _graphics;
    readonly Grid _iconGrid;
    Label[] _outlinedIcons = [];
    Label[] _filledIcons = [];

    // ---------- Bindable API (same as the control-based version) ----------
    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items), typeof(IReadOnlyList<TabBarItem>), typeof(GlassTabBarCanvas), null,
        propertyChanged: (b, _, _) => ((GlassTabBarCanvas)b).Rebuild());

    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
        nameof(SelectedIndex), typeof(int), typeof(GlassTabBarCanvas), 0, BindingMode.TwoWay,
        propertyChanged: (b, o, n) => ((GlassTabBarCanvas)b).OnSelectedIndexChanged((int)o, (int)n));

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

    /// <summary>Draw the capsule + shadow ourselves (default). Turn off if a native glass layer sits behind.</summary>
    public bool DrawSurface { get; set; } = true;

    public event EventHandler<int>? TabReselected;

    // ---------- Transition state (read by the drawable) ----------
    float[] _sel = [];          // 0..1 selected progress per tab (colour)
    double _bubbleX;            // highlight left offset inside the capsule's inner area
    int _visualIndex = -1;
    Label? _animatedIcon;

    // ---------- Touch preview state ----------
    const int LongPressMilliseconds = 450;
    int _pressedIndex = -1;
    int _touchGeneration;
    bool _longPressed;

    public GlassTabBarCanvas()
    {
        BackgroundColor = Colors.Transparent;
        HeightRequest = BarHeight + TopPad + BottomPad;
        VerticalOptions = LayoutOptions.End;
        _graphics = new GraphicsView
        {
            BackgroundColor = Colors.Transparent,
            Drawable = new BarDrawable(this)
        };
        _iconGrid = new Grid
        {
            BackgroundColor = Colors.Transparent,
            InputTransparent = true,
            HeightRequest = IconLabelHeight,
            Margin = new Thickness(HMargin + Inset, IconTop, HMargin + Inset, 0),
            VerticalOptions = LayoutOptions.Start
        };
        var layout = new Grid { BackgroundColor = Colors.Transparent };
        layout.Children.Add(_graphics);
        layout.Children.Add(_iconGrid);
        Content = layout;

        _graphics.StartInteraction += OnStart;
        _graphics.DragInteraction += OnDrag;
        _graphics.EndInteraction += OnEnd;
        _graphics.CancelInteraction += (_, _) => CancelPreview();
        SizeChanged += (_, _) =>
        {
            UpdateIconGridMargin();
            SnapHighlight();
        };
    }

    void Invalidate() => _graphics.Invalidate();

    float BarWidth => Width > 0 ? (float)Math.Min(MaxBarWidth, Math.Max(0, Width - 2 * HMargin)) : 0;
    float BarLeft => ((float)Width - BarWidth) / 2;

    float ItemWidth =>
        Items is { Count: > 0 } && BarWidth > 2 * Inset
            ? (BarWidth - 2 * Inset) / Items.Count
            : 0;

    void UpdateIconGridMargin()
    {
        var side = BarLeft + Inset;
        _iconGrid.Margin = new Thickness(side, IconTop, side, 0);
    }

    // ---------- Build / selection ----------
    void Rebuild()
    {
        this.AbortAnimation("selection");
        this.AbortAnimation("iconMotion");
        ResetIconMotion();
        var n = Items?.Count ?? 0;
        _sel = new float[n];
        if (SelectedIndex >= 0 && SelectedIndex < n) _sel[SelectedIndex] = 1f;
        _visualIndex = SelectedIndex;
        _iconGrid.Children.Clear();
        _iconGrid.ColumnDefinitions.Clear();
        _outlinedIcons = new Label[n];
        _filledIcons = new Label[n];
        for (var i = 0; i < n; i++)
        {
            _iconGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            var glyph = IconGlyph(Items![i].Icon);
            _outlinedIcons[i] = CreateIconLabel(glyph, "MaterialIconsOutlined");
            _filledIcons[i] = CreateIconLabel(glyph, "MaterialIconsFilled");
            Grid.SetColumn(_outlinedIcons[i], i);
            Grid.SetColumn(_filledIcons[i], i);
            _iconGrid.Children.Add(_outlinedIcons[i]);
            _iconGrid.Children.Add(_filledIcons[i]);
            UpdateIconVisual(i);
        }
        SnapHighlight();
    }

    static Label CreateIconLabel(string glyph, string fontFamily) => new()
    {
        Text = glyph,
        FontFamily = fontFamily,
        FontSize = IconSize,
        FontAutoScalingEnabled = false,
        Padding = new Thickness(0),
        InputTransparent = true,
        HorizontalTextAlignment = TextAlignment.Center,
        VerticalTextAlignment = TextAlignment.Center
    };

    static string IconGlyph(TabIcon icon) => icon switch
    {
        TabIcon.Discover => "\ue8b6", // search
        TabIcon.Activity => "\uef55", // local_fire_department
        TabIcon.ResumeAi => "\ue866", // bookmark
        TabIcon.Profile => "\ue7fd", // person
        _ => string.Empty
    };

    void UpdateIconVisual(int i)
    {
        var progress = _sel[i];
        var color = Lerp(Inactive, Active, progress);
        _outlinedIcons[i].TextColor = color;
        _filledIcons[i].TextColor = color;
        _outlinedIcons[i].Opacity = 1 - progress;
        _filledIcons[i].Opacity = progress;
    }

    void SnapHighlight()
    {
        this.AbortAnimation("selection");
        _bubbleX = _visualIndex * ItemWidth;
        for (var i = 0; i < _sel.Length; i++)
        {
            _sel[i] = i == _visualIndex ? 1f : 0f;
            UpdateIconVisual(i);
        }
        Invalidate();
    }

    void OnSelectedIndexChanged(int oldIndex, int newIndex)
    {
        if (newIndex < 0 || newIndex >= _sel.Length) return;
        ShowHighlight(newIndex);
        if (oldIndex != newIndex) AnimateSelectedIcon(newIndex);
    }

    void ShowHighlight(int index)
    {
        if (index < 0 || index >= _sel.Length || index == _visualIndex) return;
        var w = ItemWidth;
        _visualIndex = index;
        if (w <= 0) return;

        var fromX = _bubbleX;
        var toX = index * (double)w;
        var fromSelection = (float[])_sel.Clone();
        this.AbortAnimation("selection");
        new Animation(t =>
        {
            _bubbleX = fromX + (toX - fromX) * t;
            for (var i = 0; i < _sel.Length; i++)
            {
                var target = i == index ? 1f : 0f;
                if (fromSelection[i] == target) continue;
                _sel[i] = (float)(fromSelection[i] + (target - fromSelection[i]) * t);
                UpdateIconVisual(i);
            }
            Invalidate();
        }).Commit(this, "selection", 16, 120, Easing.CubicOut);
    }

    void AnimateSelectedIcon(int index)
    {
        this.AbortAnimation("iconMotion");
        ResetIconMotion();
        var icon = _filledIcons[index];
        var kind = Items![index].Icon;
        _animatedIcon = icon;
        new Animation(t =>
        {
            var pulse = Math.Sin(Math.PI * t);
            switch (kind)
            {
                case TabIcon.Discover:
                    icon.Scale = 1 + 0.08 * pulse;
                    icon.Rotation = 10 * Math.Sin(2 * Math.PI * t) * (1 - t);
                    break;
                case TabIcon.Activity:
                    icon.Scale = 1 + 0.18 * pulse;
                    icon.TranslationY = -2 * pulse;
                    break;
                case TabIcon.ResumeAi:
                    icon.Scale = 1 + 0.06 * pulse;
                    icon.TranslationY = -4 * pulse;
                    break;
                case TabIcon.Profile:
                    icon.Scale = 1 + 0.14 * pulse;
                    break;
            }
        }).Commit(this, "iconMotion", 16, 260, Easing.Linear, (_, _) => ResetIconMotion());
    }

    void ResetIconMotion()
    {
        if (_animatedIcon is null) return;
        _animatedIcon.Scale = 1;
        _animatedIcon.Rotation = 0;
        _animatedIcon.TranslationY = 0;
        _animatedIcon = null;
    }

    // ---------- Touch ----------
    void OnStart(object? sender, TouchEventArgs e)
    {
        CancelPreview(restoreHighlight: false);
        if (e.Touches.Length == 0) return;
        var index = HitTest(e.Touches[0]);
        if (index < 0)
        {
            ShowHighlight(SelectedIndex);
            return;
        }

        _pressedIndex = index;
        _longPressed = false;
        ShowHighlight(index);

        var generation = _touchGeneration;
        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(LongPressMilliseconds), () =>
        {
            if (generation == _touchGeneration && _pressedIndex >= 0)
            {
                _longPressed = true;
                ShowHighlight(SelectedIndex);
            }
            return false;
        });
    }

    void OnDrag(object? sender, TouchEventArgs e)
    {
        if (_pressedIndex < 0 || _longPressed || e.Touches.Length == 0) return;
        var index = HitTest(e.Touches[0]);
        if (index >= 0 && index != _pressedIndex)
        {
            _pressedIndex = index;
            ShowHighlight(index);
        }
    }

    void OnEnd(object? sender, TouchEventArgs e)
    {
        var index = _pressedIndex;
        var longPressed = _longPressed;
        _pressedIndex = -1;
        _touchGeneration++;
        if (index < 0 || longPressed) return;

        if (index == SelectedIndex)
        {
            TabReselected?.Invoke(this, index);
        }
        else
        {
            Tick();
            SelectedIndex = index;
        }
    }

    void CancelPreview(bool restoreHighlight = true)
    {
        _pressedIndex = -1;
        _touchGeneration++;
        if (restoreHighlight) ShowHighlight(SelectedIndex);
    }

    int HitTest(PointF point)
    {
        var w = ItemWidth;
        if (w <= 0 || Items is not { Count: > 0 } ||
            point.X < BarLeft || point.X > BarLeft + BarWidth ||
            point.Y < TopPad || point.Y > TopPad + BarHeight)
            return -1;

        return Math.Clamp((int)((point.X - BarLeft - Inset) / w), 0, Items.Count - 1);
    }

    static void Tick()
    {
        try { HapticFeedback.Default.Perform(HapticFeedbackType.Click); }
        catch { /* no haptics */ }
    }

    static Color Lerp(Color a, Color b, float t) => new(
        a.Red + (b.Red - a.Red) * t,
        a.Green + (b.Green - a.Green) * t,
        a.Blue + (b.Blue - a.Blue) * t,
        a.Alpha + (b.Alpha - a.Alpha) * t);

    // ======================================================================
    //  Drawing
    // ======================================================================
    sealed class BarDrawable(GlassTabBarCanvas o) : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var items = o.Items;
            if (items is null || items.Count == 0 || o.BarWidth <= 2 * Inset) return;

            var bar = new RectF(o.BarLeft, TopPad, o.BarWidth, (float)BarHeight);
            var radius = bar.Height / 2;

            // 1. Capsule surface + shadow + rim light
            if (o.DrawSurface)
            {
                canvas.SaveState();
                canvas.SetShadow(new SizeF(0, 2), 8, Colors.Black.WithAlpha(0.14f));
                canvas.FillColor = Surface;
                canvas.FillRoundedRectangle(bar, radius);
                canvas.RestoreState();

                canvas.StrokeColor = Rim;
                canvas.StrokeSize = 1;
                canvas.DrawRoundedRectangle(bar.X + 0.5f, bar.Y + 0.5f, bar.Width - 1, bar.Height - 1, radius - 0.5f);
            }

            var itemW = (bar.Width - 2 * Inset) / items.Count;
            var bubbleH = bar.Height - 2 * Inset;

            // Keep the highlight inside the rounded surface during transitions.
            var clip = new PathF();
            clip.AppendRoundedRectangle(bar, radius);
            canvas.SaveState();
            canvas.ClipPath(clip);
            canvas.Translate(bar.X + Inset + (float)o._bubbleX + itemW / 2, bar.Y + Inset + bubbleH / 2);
            canvas.FillColor = BubbleFill;
            canvas.FillRoundedRectangle(-itemW / 2, -bubbleH / 2, itemW, bubbleH, bubbleH / 2);
            canvas.RestoreState();

            // 3. Labels; icon font Labels are overlaid by the ContentView.
            var stackH = IconSize + IconGap + LabelHeight;
            var cy = bar.Y + bar.Height / 2;

            for (var i = 0; i < items.Count && i < o._sel.Length; i++)
            {
                var cx = bar.X + Inset + itemW * (i + 0.5f);
                var color = Lerp(Inactive, Active, o._sel[i]);

                canvas.SaveState();
                canvas.Translate(cx, cy);
                canvas.FontColor = color;
                canvas.FontSize = 11;
                canvas.Font = Microsoft.Maui.Graphics.Font.DefaultBold;
                canvas.DrawString(items[i].Title,
                    -itemW / 2, -stackH / 2 + IconSize + IconGap, itemW, LabelHeight,
                    HorizontalAlignment.Center, VerticalAlignment.Center);

                canvas.RestoreState();
            }
        }
    }
}
