using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Configuratinator3000
{

	public interface IAssemblyFilter
	{
		IEnumerable<Assembly> Filter(IEnumerable<Assembly> assemblies);
	}

	public class AssemblyLoader
	{
		private readonly List<Func<IEnumerable<Assembly>>> loadFunctions = new List<Func<IEnumerable<Assembly>>>();
		private readonly List<IAssemblyFilter> filters = new List<IAssemblyFilter>();


		public static implicit operator AssemblyLoader(Func<IEnumerable<Assembly>> function )
		{
			var loader = new AssemblyLoader();
			loader.AddItemToEndOfLoader(function);
			return loader;
		}

		public static AssemblyLoader operator +(AssemblyLoader loader, Func<IEnumerable<Assembly>> function )
		{
			loader.AddItemToEndOfLoader(function);
			return loader;
		}

		private void AddItemToEndOfLoader(Func<IEnumerable<Assembly>> function)
		{
			loadFunctions.Add(function);
		}

		public IEnumerable<Assembly> Invoke()
		{
			return loadFunctions.SelectMany(f => f());
		}
	}
}
