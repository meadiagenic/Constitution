using System;
using FluentAssertions;
using NUnit.Framework;

namespace Configuratinator3000.Tests.ConfiguratinatorTests
{
	[TestFixture]
	public class WhenLoadIsCalled
	{
		[SetUp]
		public void SetUp()
		{
			Configuratinator.Reset();
		}

		[Test]
		public void ThrowsExceptionIfCalledMoreThanOnce()
		{
			Configuratinator.Load();

			Action act = Configuratinator.Load;

			act.ShouldThrow<ConfiguratinatorException>();
		}

		[Test]
		public void Default_Environment_Should_Be_local()
		{
			Configuratinator.Load();
			Configuratinator.Config.Environment.Should().Be("local");
		}

		[Test]
		public void Environment_Should_Be_Set_To_ParameterValue()
		{
			var environment = "something";

			Configuratinator.Load(environment);

			Configuratinator.Config.Environment.Should().Be(environment);
		}

	}
}
