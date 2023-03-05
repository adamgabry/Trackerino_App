using Microsoft.Extensions.Configuration;

namespace Trackerino.DAL.Factories;

public class ConfigurationController
{
    private readonly IConfiguration _configuration;

    public ConfigurationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string NameOfSQLService()
    {
        var ldbEnabled = _configuration.GetSection("Connections:LocalDB:enabled");
        if (ldbEnabled.To = true)
        {

        }
        Console.WriteLine(ldbEnabled.Value);
        return "";
    }
}