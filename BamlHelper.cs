using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Baml2006;
using System.Xaml;

namespace Anythink.Wpf.Skinning
{
	internal static class BamlHelper
	{
		#region Member Variables

		//For .net 3.5:
		//private static MethodInfo LoadBamlMethod;	
		//internal const string LOAD_BAML_METHOD = "LoadBaml";

		#endregion

		#region Constructor

		static BamlHelper()
		{

			//For .net 3.5:
			//Type xamlType = typeof(System.Windows.Markup.XamlReader);
			//LoadBamlMethod = xamlType.GetMethod(LOAD_BAML_METHOD, BindingFlags.NonPublic | BindingFlags.Static);
		}

		#endregion

		#region Methods

		public static List<Stream> GetBamlStreams(AssemblyName skinAssemblyName)
		{
			List<Stream> bamlStreams = new List<Stream>();
			Assembly skinAssembly = Assembly.Load(skinAssemblyName);
			string[] resourceDictionaries = skinAssembly.GetManifestResourceNames();
			foreach (string resourceName in resourceDictionaries)
			{
				ManifestResourceInfo info = skinAssembly.GetManifestResourceInfo(resourceName);
				if (info.ResourceLocation != ResourceLocation.ContainedInAnotherAssembly)
				{
					Stream resourceStream = skinAssembly.GetManifestResourceStream(resourceName);
					using (ResourceReader reader = new ResourceReader(resourceStream))
					{
						foreach (DictionaryEntry entry in reader)
						{
							if (IsRelevantResource(entry))
							{
								bamlStreams.Add(entry.Value as Stream);
							}
						}
					}
				}
			}

			return bamlStreams;
		}

		private static bool IsRelevantResource(DictionaryEntry entry)
		{
			return string.Compare(Path.GetExtension(entry.Key.ToString()), ".baml", true) == 0 && entry.Key.ToString().ToLower().Contains("skin") && entry.Value is Stream;
		}

		public static T LoadBaml<T>(Stream stream)
		{
			//For .net 3.5:
			//ParserContext parserContext = new ParserContext();
			//object[] parameters = new object[] { stream, parserContext, null, false };
			//object bamlRoot = LoadBamlMethod.Invoke(null, parameters);
			//return (T)bamlRoot;

			var reader = new Baml2006Reader(stream);
			var writer = new XamlObjectWriter(reader.SchemaContext);
			while (reader.Read())
				writer.WriteNode(reader);

			return (T)writer.Result;

		}

		#endregion
	}
}
