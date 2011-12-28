using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Configuratinator3000
{


	public static class Configuratinator
	{
		private static bool _isLoaded = false;
		private static string _baseEnvironment;
		static Configuratinator()
		{
			_baseEnvironment = (ConfigurationManager.AppSettings["ENV"] ?? "local").ToLowerInvariant();
		}

		private static IConfiguration _config;

		public static IConfiguration Config
		{
			get { return _config = _config ?? new DefaultConfiguration(_baseEnvironment, null, null); }
			private set { _config = value; }
		}

		public static void Load()
		{
			Load(_baseEnvironment);
		}

		public static void Load(string environment)
		{
			if (_isLoaded) throw new ConfiguratinatorException("Load can only be called once.");
			Config = new DefaultConfiguration(environment, null, null);
			_isLoaded = true;
		}

		public static void Reset()
		{
			_isLoaded = false;
			Config = null;
		}

		public static IEnumerable<Assembly> LoadedAssemblies { get; private set; }


		//private Func<IEnumerable<Assembly>> _assemblyLoader = () =>
		//                                                        {
		//                                                            var loadedAssemblies =
		//                                                                AppDomain.CurrentDomain.GetAssemblies().Where(
		//                                                                    assembly => !(assembly.IsDynamic || assembly.ReflectionOnly));

		//                                                            var loadedAssemblyPaths =
		//                                                                loadedAssemblies.Select(a => a.Location);

		//                                                            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

		//                                                            var assemblyFiles =
		//                                                                Directory.GetFiles(baseDirectory, "*.dll",
		//                                                                                   SearchOption.AllDirectories).Where(
		//                                                                                    f =>
		//                                                                                    !loadedAssemblyPaths.Contains(f,
		//                                                                                                                  StringComparer
		//                                                                                                                    .
		//                                                                                                                    InvariantCultureIgnoreCase));

		//                                                            foreach (var assemblyFile in assemblyFiles)
		//                                                            {
		//                                                                Assembly.Load(AssemblyName.GetAssemblyName(assemblyFile));
		//                                                            }

		//                                                            return
		//                                                                AppDomain.CurrentDomain.GetAssemblies().Where(
		//                                                                    ass =>
		//                                                                    !(ass.FullName.Contains("System") ||
		//                                                                      ass.FullName.Contains("mscorlib")));
		//                                                        };

	}
}

