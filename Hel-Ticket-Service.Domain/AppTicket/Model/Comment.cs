using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Hel_Ticket_Service.Domain.AppTicket.Model
{
    public class Comment
    {
      [BsonId]
      public string Reference { get; set; }
      public string UserReference { get; set; } 
      public string Message { get; set; }
      public DateTime TimeStamp { get; set; }
        
        
    }
}