using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Configuratinator3000
{

	public class AssemblyLoader : IAssemblyLoader
	{
		public IEnumerable<Assembly> Assemblies
		{
			get { return UpdateAssemblies(); }
		}

		public IEnumerable<Type> Types
		{
			get { throw new NotImplementedException(); }
		}

		private readonly List<string> _assembliesToCheck = new List<string>(); 

		public IAssemblyLocationFilter IncludeAssembliesFrom(string path, string wildCard, bool includeChildFolders)
		{
			var pathToAdd = Path.IsPathRooted(path) ? path : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
			if ((File.GetAttributes(pathToAdd) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				if (Directory.Exists(pathToAdd))
				{
					foreach (var file in Directory.GetFiles(pathToAdd, wildCard ?? "*.dll", includeChildFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
					{
						CheckIfFileExistsAndAddToAssemblyList(file);
					}
				}
			}
			else
			{
				CheckIfFileExistsAndAddToAssemblyList(pathToAdd);
			}
			
			return this;
		}

		private void CheckIfFileExistsAndAddToAssemblyList(string pathToAdd)
		{
			if (File.Exists(pathToAdd))
				_assembliesToCheck.Add(pathToAdd);
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
			
			var loadedAssemblyPaths = LoadedAssemblies().Select(a => a.Location).ToArray();

			var unloadedAssemblies =
				_assembliesToCheck.Where(f => !loadedAssemblyPaths.Contains(f, StringComparer.InvariantCultureIgnoreCase));


			foreach (var file in unloadedAssemblies)
			{
				var assemblyName = AssemblyName.GetAssemblyName(file);
				if (!FilterAssemblyName(assemblyName.Name)) break;
				var assembly = Assembly.Load(assemblyName);
			}
			_assembliesToCheck.Clear();

			return LoadedAssemblies();
			//foreach (var assemblyFile in _assembliesToCheck)
			//{
			//    var unloadedAssemblies = 

			//    foreach (var file in unloadedAssemblies)
			//    {
			//        if (!FilterAssemblyName(Path.GetFileName(file.FullName))) break;
			//        var assembly = Assembly.Load(AssemblyName.GetAssemblyName(file.FullName));
			//        if (FilterAssembly(assembly))
			//            yield return assembly;
			//    }
			//}
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

		private IEnumerable<Assembly> LoadedAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies().Where(assembly => !assembly.IsDynamic && !assembly.ReflectionOnly);
		}
	}
}
