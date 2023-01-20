using System.Net.Http;
using Hel_Ticket_Service.Domain;
using MongoDB.Driver;
using Serilog;
using StackExchange.Redis;

namespace Hel_Ticket_Service.Infrastructure;

public class EBProvider: IEBProvider
{ 
    readonly IConfiguration _configuration;
    readonly EBConnection _connection;
   
    public EBProvider(IConfiguration configuration, EBConnection connection)
    {
        
        _connection = connection;
        _configuration = configuration;
        _connection.ConnectionString =  _configuration.GetSection(nameof(Service))["EventBusUrl"];
    }

    public ISubscriber Connect()
    {
      
            Log.Information("Connecting to Redis...");
            var client = ConnectionMultiplexer.Connect(_connection.ConnectionString); 
            var status =  client.GetDatabase().PingAsync();
            Thread.Sleep(500);

            if (status == null)
            {
                 Log.Error("Error connecting to Redis");
                 throw new  AppException(new[]{ $"Redis Server Error: {MessageProvider.RedisDBDown}"}, "SERVER",500);
            }
           
            Log.Information("Connecting to Redis Server successful...");
            Log.Information("Getting subscriber...");
            return  client.GetSubscriber();
       
        

    }

   

}