namespace Forsa.Mobile.Controls;

/// <summary>Platform-specific icon names for a tab.</summary>
public sealed record TabBarIcon(string SfSymbol, string MaterialGlyph);

/// <summary>Presentation data for an item in <see cref="GlassTabBar"/>.</summary>
public sealed record TabBarItem(string Title, TabBarIcon Icon);
