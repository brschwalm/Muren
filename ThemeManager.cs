using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Reflection;

namespace Anythink.Wpf.Skinning
{
	/// <summary>
	/// A static class for managing the themes in an application
	/// </summary>
	public static class ThemeManager
	{
		#region Member Variables
		
		private static List<Theme> _availableThemes;
		private static Theme _currentTheme;
		private static ThemeFontSizes _currentFontSize;
		private static double _baseFontSize;
		private static double _fontSizeOffset;

		internal const string UNKNOWN_THEME_NAME = "Unknown Theme";
		internal const string THEME_DIRECTORY = "Skins";
		internal const string THEME_FILE_FILTER = "*.dll";
		internal const double BASE_FONT_SIZE = 14;
		internal const double FONT_SIZE_OFFSET = 2;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the list of themes that are available
		/// </summary>
		public static IEnumerable<Theme> AvailableThemes
		{
			get { return _availableThemes; }
		}

		/// <summary>
		/// Gets the currently applied theme, if there is one
		/// </summary>
		public static Theme CurrentTheme { get { return _currentTheme; } }

		/// <summary>
		/// Gets the current font size for the theme.  This size is the normal theme font size, and will be offset for
		/// the small and large font sizes.
		/// </summary>
		public static ThemeFontSizes CurrentFontSize 
		{ 
			get { return _currentFontSize; }
			set 
			{
				if (value != _currentFontSize)
				{
					_currentFontSize = value;
					UpdateFontSize();
				}
			}
		}

		public static string ThemeDirectory { get; private set; }
		public static string ThemeFileFilter { get; private set; }
		#endregion
		
		#region Public Methods

		/// <summary>
		/// Initializes the ThemeManager by loading and caching the available themes
		/// </summary>
		public static void Initialize()
		{
			string themeDirectory = Path.Combine(Environment.CurrentDirectory, THEME_DIRECTORY);
			Initialize(themeDirectory, THEME_FILE_FILTER);
		}

		/// <summary>
		/// Initializes the ThemeManager with a custom directory path and theme filter.
		/// </summary>
		/// <param name="themeDirectory">The absolute path to the directory where themes are stored</param>
		/// <param name="themeFileFilter">The filter used to search for theme files - for example: *.dll or *Skin.dll</param>
		public static void Initialize(string themeDirectory, string themeFileFilter)
		{
			ThemeDirectory = themeDirectory;
			ThemeFileFilter = themeFileFilter;

			LoadAvailableThemes();
			UpdateFontSize();
		}

		/// <summary>
		/// Changes the theme to the newly selected theme
		/// </summary>
		/// <param name="newTheme"></param>
		public static void ChangeTheme(Theme newTheme)
		{
			if (newTheme == null)
				throw new ArgumentNullException("newTheme");

			if (_currentTheme != null)
			{
				_currentTheme.Unload();
			}

			newTheme.Load();
			_currentTheme = newTheme;
		}

		/// <summary>
		/// Clears the current theme from the application
		/// </summary>
		public static void ClearTheme()
		{
			if (_currentTheme != null)
			{
				_currentTheme.Unload();
				_currentTheme = null;
			}
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// Will load the themes from the Skins directory
		/// </summary>
		private static void LoadAvailableThemes()
		{
			if (_availableThemes == null)
			{
				_availableThemes = new List<Theme>();

				//Look for assemblies in the appropriate directory
				string[] skinAssemblies = Directory.GetFiles(ThemeDirectory, ThemeFileFilter);
				foreach (string skinAssembly in skinAssemblies)
				{
					string themeName = GetThemeName(skinAssembly);
					Theme theme = new Theme(themeName, skinAssembly);
					_availableThemes.Add(theme);
				}
			}
		}

		/// <summary>
		/// Gets the name of a theme from the assembly path
		/// </summary>
		/// <param name="assemblyPath">The path to the theme assembly</param>
		/// <returns>The name of the theme, which should be defined in the AssemblyDescription attribute</returns>
		private static string GetThemeName(string assemblyPath)
		{
			Assembly asm = Assembly.LoadFrom(assemblyPath);
			if(Attribute.IsDefined(asm, typeof(AssemblyDescriptionAttribute)))
			{
				AssemblyDescriptionAttribute attr = Attribute.GetCustomAttribute(asm, typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute;
				return string.IsNullOrEmpty(attr.Description) ? UNKNOWN_THEME_NAME : attr.Description;
			}

			return UNKNOWN_THEME_NAME;
		}

		/// <summary>
		/// Adjusts the font size based on the CurrentFontSize, setting the resource values for the three different
		/// font siez types
		/// </summary>
		private static void UpdateFontSize()
		{
			double currentBase = BASE_FONT_SIZE + (FONT_SIZE_OFFSET * (int)CurrentFontSize);

			Application.Current.Resources["NormalFontSize"] = currentBase;
			Application.Current.Resources["LargeFontSize"] = currentBase + ThemeManager._fontSizeOffset;
			Application.Current.Resources["SmallFontSize"] = currentBase - ThemeManager._fontSizeOffset;

		}

		#endregion
	}
}
