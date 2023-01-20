using System.Diagnostics;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Hel_Ticket_Service.Domain.AppTicket.Model;
namespace Hel_Ticket_Service.Domain;
public class UpdateTicketDto
   { 
    
      public string Message {get;set;}
      public List<Image> Image {get;set;}
      public string UserReference {get;set;}
      
   }