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
		IAssemblyLocationFilter IncludeAssembliesFrom(string path, string wildCard, bool includeChildFolders);
	}

	public interface IAssemblyNameFilter : IFluent
	{
		void OnlyNamed(Func<string, bool> filter);
		void ExcludeNamed(Func<string, bool> filter);
	}


	public static class AssemblyLocationFilterExtensions
	{
		public static IAssemblyLocationFilter IncludeAssembliesFrom(this IAssemblyLocationFilter filter, string path)
		{
			
		}

		public static IAssemblyLocationFilter IncludeAssembliesFrom(this IAssemblyLocationFilter filter, string path, string wildCard)
		{
			
		}
		
		public static IAssemblyLocationFilter IncludeAssembliesInBin(this IAssemblyLocationFilter filter)
		{

			return filter.IncludeAssembliesInBin("*.dll");
		}

		public static IAssemblyLocationFilter IncludeAssembliesInBin(this IAssemblyLocationFilter filter, string wildCard)
		{
			filter.IncludeAssembliesFrom("bin", wildCard, true);
			return filter;
		}
	}
}
