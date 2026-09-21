namespace Forsa.Mobile.Controls;

internal static class GlassTabBarMetrics
{
    public const double BarHeight = 64;
    public const double OuterPadding = 8;
    public const double MaxBarWidth = 440;
    public const double MinSideMargin = 20;
    public const double PillInset = 5;
    public const double IconSize = 24;
    public const double MaxFontScale = 1.3;
    public const double PillHeight = BarHeight - (2 * PillInset);
    public const double ReservedHeight = BarHeight + (2 * OuterPadding);

    public static double BarWidth(double availableWidth) =>
        Math.Min(MaxBarWidth, Math.Max(0, availableWidth - (2 * MinSideMargin)));
}
