namespace Configuratinator3000
{

	public interface IConfiguration
	{
		string Environment { get; }
		dynamic AppSettings { get; }
		dynamic ConnectionStrings { get; }
	}
}
