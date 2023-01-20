using System.Net.Http;
using Hel_Ticket_Service.Domain;
using MongoDB.Driver;
using Serilog;

namespace Hel_Ticket_Service.Infrastructure;

public class DBProvider: IDBProvider
{ 
    readonly IConfiguration _configuration;
    readonly DBConnection _connection;
   
    public DBProvider(IConfiguration configuration, DBConnection connection)
    {
        
        _connection = connection;
        _configuration = configuration;
        _configuration.GetSection(nameof(DBConnection)).Bind(_connection = connection); 
    }

    public IMongoDatabase Connect()
    {
      
            Log.Information("Connecting to MongoDB...");
            var client = new MongoClient(_connection.ConnectionString); 

            Thread.Sleep(500);

            var connectionState =  (client.Cluster.Description.State == MongoDB.Driver.Core.Clusters.ClusterState.Connected);
            if (!connectionState)
            {
                 Log.Error("Error connecting to MongoDB");
                 throw new  AppException(new[]{ $"Server Error: {MessageProvider.MongoDBDown}"}, "SERVER",500);
            }
           
            Log.Information("Connecting to MongoDB successful...");
            Log.Information("Getting database...");
            return client.GetDatabase(_connection.DatabaseName);  
       
        

    }

    public int GetPageLimit()
    {
        return _connection.PageLimit;
    }

}