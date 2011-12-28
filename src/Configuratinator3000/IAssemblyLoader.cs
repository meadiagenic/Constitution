namespace Configuratinator3000
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;

	public interface IAssemblyLoader : IAssemblyLocationFilter, IFluent
	{
		IEnumerable<Assembly> Assemblies { get; } 
		IEnumerable<Type> Types { get; }
	}


	public interface IAssemblyLocationFilter : IAssemblyNameFilter, IFluent
	{
		IAssemblyLocationFilter IncludeAppDomainAssemblies();
		IAssemblyLocationFilter LoadFromBin();
		IAssemblyLocationFilter LoadFromPath(string path);
		IAssemblyLocationFilter Load(Assembly assembly);
	}

	public interface IAssemblyNameFilter : IFluent
	{
		void OnlyNamed(Func<string, bool> filter);
		void ExcludeNamed(Func<string, bool> filter);
	}
}
