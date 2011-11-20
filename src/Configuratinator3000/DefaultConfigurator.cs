
namespace System
{
    using System.Configuration;

    public class DefaultConfigurator : IConfigurator
    {
        public string GetEnvironment(IConfigurator configurator)
        {
            return ConfigurationManager.AppSettings["Environment"];
        }
    }
}
