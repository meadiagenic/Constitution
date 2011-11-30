namespace Configuratinator3000.Tests.ConfigTests
{
	using System.Configuration;
	using System.Diagnostics;
	using System.Linq;
	using FluentAssertions;
	using NUnit.Framework;

    [TestFixture]
    public class WhenLoadIsCalled
    {

		[Test]
		public void Default_Environment_Should_Be_Test()
		{
			Config.Load();
			Config.Environment.Should().Be("test");

			for (int i = 0; i < ConfigurationManager.AppSettings.Count; i++)
			{
				Trace.WriteLine(ConfigurationManager.AppSettings.GetKey(i) + ": " + ConfigurationManager.AppSettings[i]);
			}
		}

		[Test]
		public void Environment_Should_Be_Set_To_ParameterValue()
		{
			var environment = "something";

			Config.Load(environment);

			Config.Environment.Should().Be(environment);
		}

        [Test]
        public void Should_Set_Environment_To_Environment_In_Config_File()
        {			
			Config.Load();

			Config.Environment.Should().Be("test");
        }

		[Test]
		public void Should_Load_All_IConfig_Classes_in_Bin()
		{

			Config.Load();

			Config.LoadedAssemblies.Count().Should().BeGreaterThan(0);
		}
    }
}
