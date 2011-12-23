namespace Configuratinator3000.Tests.ConfigTests
{
	using System.Linq;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class WhenLoadIsCalled
	{

		[Test]
		public void Default_Environment_Should_Be_local()
		{
			Configuratinator.Load();
			Configuratinator.Environment.Should().Be("local");

		}

		[Test]
		public void Environment_Should_Be_Set_To_ParameterValue()
		{
			var environment = "something";

			Configuratinator.Load(environment);

			Configuratinator.Environment.Should().Be(environment);
		}

		[Test]
		public void Should_Set_Environment_To_Environment_In_Config_File()
		{
			Configuratinator.Load();

			Configuratinator.Environment.Should().Be("test");
		}

		[Test]
		public void Should_Load_All_IConfig_Classes_in_Bin()
		{

			Configuratinator.Load();

			Configuratinator.
		}
	}
}
