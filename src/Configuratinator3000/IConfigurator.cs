namespace System
{

    public interface IConfigurator
    {
        string GetEnvironment(IConfigurator configurator);
    }
}
