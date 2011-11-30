using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Configuratinator3000
{
	public static class Config
	{
		public static void Load()
		{
			Load(ConfigurationManager.AppSettings["Configuration.Environment"]);
		}

		private static Func<IEnumerable<Assembly>> _assemblyLoader = () =>
		{
			var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => !(assembly.IsDynamic || assembly.ReflectionOnly));

			var loadedAssemblyPaths = loadedAssemblies.Select(a => a.Location);

			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

			var assemblyFiles = Directory.GetFiles(baseDirectory, "*.dll",SearchOption.AllDirectories).Where(f => !loadedAssemblyPaths.Contains(f, StringComparer.InvariantCultureIgnoreCase));

			foreach (var assemblyFile in assemblyFiles)
			{
				Assembly.Load(AssemblyName.GetAssemblyName(assemblyFile));
			}

			return AppDomain.CurrentDomain.GetAssemblies().Where(ass => !(ass.FullName.Contains("System") || ass.FullName.Contains("mscorlib")));
		};

		public static void Load(string environment)
		{
			Environment = environment ?? "test";
			LoadedAssemblies = GetAssemblies();
		}

		private static IEnumerable<Assembly> GetAssemblies()
		{
			return _assemblyLoader();
		}

		public static string Environment { get; private set; }

		public static IEnumerable<Assembly> LoadedAssemblies
		{
			get; private set;
		}
	}
}
