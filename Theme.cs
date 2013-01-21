using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Anythink.Wpf.Skinning
{
	public class Theme
	{
		#region Member Variables

		private List<ResourceDictionary> _resources = new List<ResourceDictionary>();

		#endregion

		#region Constructors

		public Theme(string name, string assemblyPath)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if (string.IsNullOrEmpty(assemblyPath))
				throw new ArgumentNullException("assemblyPath");

			Name = name;
			FullName = AssemblyName.GetAssemblyName(assemblyPath);
			
		}

		public Theme(string name, AssemblyName assemblyName)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if (assemblyName == null)
				throw new ArgumentNullException("assemblyName");

			Name = name;
			FullName = assemblyName;
		}

		#endregion

		#region Properties

		public string Name { get; set; }
		public IEnumerable<ResourceDictionary> Resources { get { return _resources; } }
		private AssemblyName FullName { get; set; }

		#endregion

		#region Public Methods
		
		internal void Load()
		{
			if (Resources.Count() == 0)
			{
				LoadResources();
				MergeResources();
			}
		}

		internal void Unload()
		{
			if (Resources.Count() > 0)
			{
				UnmergeResources();
				_resources.Clear();
			}
		}

		#endregion

		#region Private Methods

		private void LoadResources()
		{
			List<Stream> bamlStreams = BamlHelper.GetBamlStreams(FullName);

			foreach (Stream stream in bamlStreams)
			{
				_resources.Add(BamlHelper.LoadBaml<ResourceDictionary>(stream));
			}
		}

		private void MergeResources()
		{
			_resources.ForEach(rd => Application.Current.Resources.MergedDictionaries.Add(rd));
		}

		private void UnmergeResources()
		{
			_resources.ForEach(rd => Application.Current.Resources.MergedDictionaries.Remove(rd));
		}

		#endregion
	}
}
