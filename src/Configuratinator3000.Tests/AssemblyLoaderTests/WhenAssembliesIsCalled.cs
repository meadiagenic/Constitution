using System;
using FluentAssertions;

namespace Configuratinator3000.Tests.AssemblyLoaderTests
{
	using NUnit.Framework;

	[TestFixture]
	public class WhenAssembliesIsCalled
	{
		private AssemblyLoader Loader;

		[SetUp]
		public void SetUp()
		{
			Loader = new AssemblyLoader();
		}

		[Test]
		public void WithBinFolderReturnsSevenAssemblies()
		{
			Loader.IncludeAssembliesInBin();

			Loader.Assemblies.Should().HaveCount(7);
		}

		[Test]
		public void WithExcludeNamedNunitReturnsFourAssemblies()
		{
			Loader.IncludeAssembliesInBin().ExcludeNamed(s => s.Contains("nunit"));

			Loader.Assemblies.Should().HaveCount(4);
		}

		[Test]
		public void NoFilterIsUsedReturnsAppDomainAssemblies()
		{
			var assemblyCount = AppDomain.CurrentDomain.GetAssemblies().Length;

			Loader.Assemblies.Should().HaveCount(assemblyCount);
		}

		[Test]
		public void DefaultShouldReturnAppDomainAssembliesAssemblies()
		{
			var assemblyCount = AppDomain.CurrentDomain.GetAssemblies().Length;
			Loader.Assemblies.Should().HaveCount(assemblyCount);
		}
	}
}
