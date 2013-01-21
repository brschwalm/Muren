using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Anythink.Wpf.Skinning;

namespace Anythink.Wpf.Skinning.TestWindow
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ThemeManager.Initialize();
			SkinListBox.ItemsSource = ThemeManager.AvailableThemes;

			FontSizeCombo.ItemsSource = Enum.GetValues(typeof(ThemeFontSizes));
			FontSizeCombo.SelectedItem = ThemeFontSizes.Medium;
		}

		private void SkinListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ThemeManager.ChangeTheme(SkinListBox.SelectedItem as Theme);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ChildWindow child = new ChildWindow();
			child.Show();
		}

		private void FontSizeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ThemeFontSizes fontSize = (ThemeFontSizes)FontSizeCombo.SelectedItem;
			ThemeManager.CurrentFontSize = fontSize;
		}
	}
}
