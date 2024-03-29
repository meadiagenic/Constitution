﻿using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Configuratinator3000.Tests.AssemblyLoaderTests
{

	[TestFixture]
	public class WhenLoadFromBinIsCalled
	{
		public AssemblyLoader Loader;
		[SetUp]
		public void SetUp()
		{
			Loader = new AssemblyLoader();
		}

		[Test]
		public void AssemblyFileLocationsShouldContainOneFolder()
		{
			Loader.IncludeAssembliesInBin();

			Loader.Assemblies.Select(assembly => assembly.GetName().Name).Should().Contain("DummyDll");

		}
	}
}
