using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Configuratinator3000
{

	public class AssemblyLoader : IAssemblyLoader
	{
		private IEnumerable<Assembly> assemblies;
		public IEnumerable<Assembly> Assemblies
		{
			get { return assemblies = assemblies ?? UpdateAssemblies(); }
		}

		public IEnumerable<Type> Types
		{
			get { throw new NotImplementedException(); }
		}


		private List<DirectoryInfo> _assemblyFolderLocations = new List<DirectoryInfo>();
		public IEnumerable<DirectoryInfo> AssemblyFolders
		{
			get { return _assemblyFolderLocations; }
		}

		public IAssemblyLocationFilter LoadFromBin()
		{
			LoadFromPath("bin");
			return this;
		}

		public IAssemblyLocationFilter LoadFromPath(string path)
		{
			var pathToAdd = Path.IsPathRooted(path) ? path : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
			if (Directory.Exists(pathToAdd))
			{
				_assemblyFolderLocations.Add(new DirectoryInfo(pathToAdd));
			}
			return this;
		}

		private List<Assembly> _loadedAssemblies = new List<Assembly>(); 

		public IAssemblyLocationFilter Load(Assembly assembly)
		{
			_loadedAssemblies.Add(assembly);
			return this;
		}

		public void OnlyNamed(Func<string, bool> filter)
		{

		}

		private Func<string, bool> excludeFilter;
		public void ExcludeNamed(Func<string, bool> filter)
		{
			excludeFilter = filter;
		}

		private IEnumerable<Assembly> UpdateAssemblies()
		{
			foreach (var assembly in _loadedAssemblies)
			{
				if (!FilterAssembly(assembly))
				{
					yield return assembly;
				}
			}

			var loadedAssemblyPaths = _loadedAssemblies.Select(a => a.Location).ToArray();

			foreach (var dir in _assemblyFolderLocations)
			{
				var unloadedAssemblies =
					dir.GetFiles("*.dll", SearchOption.AllDirectories).Where(
						f => !loadedAssemblyPaths.Contains(f.FullName, StringComparer.InvariantCultureIgnoreCase));

				foreach (var file in unloadedAssemblies)
				{
					if (!FilterAssemblyName(Path.GetFileName(file.FullName))) break;
					var assembly = Assembly.Load(AssemblyName.GetAssemblyName(file.FullName));
					if (!FilterAssembly(assembly))
						yield return assembly;
				}
			}
		}

		private bool FilterAssembly(Assembly assembly)
		{
			if (assembly.IsDynamic || assembly.ReflectionOnly) return false;

			return FilterAssemblyName(Path.GetFileName(assembly.Location));
		}

		private bool FilterAssemblyName(string assemblyName)
		{
			return excludeFilter == null || !excludeFilter(Path.GetFileName(assemblyName));
		}

		public IAssemblyLocationFilter IncludeAppDomainAssemblies()
		{
			_loadedAssemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
			return this;
		}
	}
}
