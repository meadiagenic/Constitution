using System.Linq;
using FluentAssertions;

namespace Configuratinator3000.Tests.AssemblyLoaderTests
{
	using System;
	using NUnit.Framework;

	[TestFixture]
	public class WhenLoadFromPathIsCalled
	{
		private AssemblyLoader Loader;

		[SetUp]
		public void SetUp()
		{
			Loader = new AssemblyLoader();
		}

		[Test]
		public void AddsPathIfPathIsRooted()
		{
			Loader.IncludeAssembliesFrom("c:\\");
			Loader.Assemblies.Should().HaveCount(1);
		}

		[Test]
		public void AddsResolvedPathIfPathIsNotRooted()
		{
			Loader.LoadFromPath("Properties");
			Loader.AssemblyFolders.Should().HaveCount(1);

			var dir = Loader.AssemblyFolders.First();
			dir.Should().NotBeNull();
			dir.FullName.Should().StartWith(AppDomain.CurrentDomain.BaseDirectory);
		}

		[Test]
		public void PathMustExistToBeAdded()
		{
			Loader.LoadFromPath("paththatdoesntexist");
			Loader.AssemblyFolders.Should().HaveCount(0);
		}
	}
}
