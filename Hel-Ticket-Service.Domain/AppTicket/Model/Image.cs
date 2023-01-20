using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
namespace Hel_Ticket_Service.Domain.AppTicket.Model
{
    public class Image
    {
      
      [BsonId]
      public string Reference { get; set; }
      public string FileName { get; set; } 
    }
    
}