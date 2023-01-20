using Hel_Ticket_Service.Domain;
using MongoDB.Driver;
using Serilog;
namespace Hel_Ticket_Service.Infrastructure;

public class ServiceProvider
{ 
    
    public static void MapConfiguration(IConfiguration _configuration)
    {
        Log.Information("Mapping service configuration from env file to global configuration object...");
        Service.Url=_configuration.GetSection("Service")["Url"];
        Service.Name=_configuration.GetSection("Service")["Name"];;
        Service.Mode=_configuration.GetSection("Service")["Mode"];
        Service.LogFileName=_configuration.GetSection("Service")["LogFileName"];
        Service.LogDirectory=_configuration.GetSection("Service")["LogDirectory"];
        Service.EventBusUrl=_configuration.GetSection("Service")["EventBusUrl"];
        Service.LaunchUrl=_configuration.GetSection("Service")["LaunchUrl"];  
    }
    
    public static bool ReadConfiguration(string envFilePath)
    {
        Log.Information("Reading service configuration from env file...");
      
        if (!File.Exists(envFilePath)) 
        {
            Log.Error("Env file does not exist");
            return false;
        }
        foreach (var line in File.ReadAllLines(envFilePath))
        {
            var parts = line.Split("===",StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) continue;
            Log.Information("Setting global environment variables...");
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
        return true;
    }

}