using System.DirectoryServices.Protocols;
using System.Net;
namespace Hel_Ticket_Service.Domain;
public class DBConnection
   {
      public string ConnectionString { get; set; }
      public string DatabaseName { get; set; }
      
      public int PageLimit { get; set; }

}
