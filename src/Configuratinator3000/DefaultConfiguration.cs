namespace Configuratinator3000
{

	public class DefaultConfiguration : IConfiguration
	{
		public DefaultConfiguration(string environment, dynamic appSettings, dynamic connectionStrings)
		{
			_appSettings = appSettings;
			_connectionStrings = connectionStrings;
			_environment = environment;
		}


		private string _environment;
		public string Environment
		{
			get { return _environment; }
		}

		private dynamic _appSettings;
		public dynamic AppSettings
		{
			get { return _appSettings; }
		}

		private dynamic _connectionStrings;
		public dynamic ConnectionStrings
		{
			get { return _connectionStrings; }
		}
	}
}
