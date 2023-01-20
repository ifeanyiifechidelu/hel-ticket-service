using System.DirectoryServices.Protocols;
using System.Net;
using MongoDB.Driver;
namespace Hel_Ticket_Service.Domain;
public interface IDBProvider
   {
      public IMongoDatabase Connect();
      public int GetPageLimit();
   }