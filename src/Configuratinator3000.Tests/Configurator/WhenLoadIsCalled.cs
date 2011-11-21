namespace Configuratinator3000.Tests.Configure
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class WhenLoadIsCalled
    {
        [Test]
        public void Should_Set_Environment_To_Environment_In_Config_File()
        {
            Configurator.Load();

            Assert.AreEqual("Test", Configure.Environment["env"]);

        }
    }
}
