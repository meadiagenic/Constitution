namespace System
{
    using System.Collections.Generic;
    using System.Extensions;
    using System.Linq;

    public static class Configure
    {
        static Configure()
        {
            ConfiguratorType = typeof(DefaultConfigurator);
        }

        private static IConfigurator _configurator;

        public static Type ConfiguratorType
        {
            get;
            set;
        }

        private static IDictionary<string, object> _environment = new Dictionary<string, object>();

        public static IDictionary<string, object> Environment
        {
            get { return _environment; }
        }

        public static IConfigurator Configurator
        {
            get { return _configurator = _configurator ?? ConfiguratorType.Create<IConfigurator>(); }
            set
            {
                if (value != null) _configurator = value;
            }
        }

        public static IConfigurator Load(params Action<IConfigurator>[] configActions)
        {
            var configurator = LoadFunc(Configurator.GetEnvironment);
            foreach (var a in configActions)
            {
                a(configurator);
            }
            return configurator;
        }

        public static IConfigurator LoadFunc(Func<IConfigurator, string> environmentLoader)
        {
            return Load(environmentLoader(Configurator));
        }

        public static IConfigurator Load(string environment)
        {
            Environment["env"] = environment;
            return Configurator;
        }
    }
}
