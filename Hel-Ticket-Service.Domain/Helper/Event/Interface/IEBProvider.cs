using System.DirectoryServices.Protocols;
using System.Net;
using StackExchange.Redis;
using MongoDB.Driver;
namespace Hel_Ticket_Service.Domain;
public interface IEBProvider
   {
      public ISubscriber Connect();
     
   }