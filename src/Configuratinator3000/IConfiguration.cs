namespace Configuratinator3000
{

	public interface IConfiguration
	{
		dynamic AppSettings { get; }
		dynamic ConnectionStrings { get; }
	}
}
