Muren
=====

Muren is a C#.NET implementation of a skinning library for WPF applications.  
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

License (MIT)
=====
Copyright (c) 2010 Anythink Solutions, LLC.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the "Software"), to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions
of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.
