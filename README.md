Muren
=====

Muren is an C# .NET implementation of a skinning library for WPF applications.  
Using Muren, you can manage one or more skins for a WPF application.  These skins can be in separate
assemblies, making it easy to swap skins at design time, or even at runtime.

Implementation uses the .NET 4.0 Client Profile framework, however an easy change (documented in the code)
allows support for .NET 3.5 Client Profile as well.

A Skin assembly is simply an assembly with XAML Resources located at a specified location (defaults to a Skins subdirectory)

Simply call:

    ThemeManager.Initialize();
    var selectedTheme = ThemeManager.AvailableThemes.First();
    ThemeManager.ChangeTheme(selectedTheme);

to load available themes and activate a specific theme.
