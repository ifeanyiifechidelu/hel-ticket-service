using Hel_Ticket_Service.Domain.AppTicket.Model;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace Hel_Ticket_Service.Domain;
public class Ticket
   { 
      [BsonId]
      public string Reference { get; set; }
      public string Title { get; set; } 
      public string CategoryReference { get; set; }
      public string Message {get;set;} 
      public List<Image> Image {get; set;} 
      public List<Activity> Activity {get; set;}
      public List<Comment> Comment {get;set;}
      public string AssignedTo {get;set;}
      public bool IsEscalted {get;set;} = false;
      public string Status {
        get { return Status; }
                set
                {
                    if (value == "OPEN" || value == "ACTIVE" || value == "CLOSED")
                    {
                        Status = value;
                    }
                    else
                    {
                        Status = "UNKNOWN";
                    }
                }

      } 
      
      public DateTime  TimeStamp {get;set;}
      public string UserReference {get;set;}
      
     
     
      public Ticket(string title, string categoryReference, string message, 
      List<Image> image,List<Activity> activity, List<Comment> comment,string assignedTo,bool isEscalated,string status, DateTime timeStamp, string userReference)
      {
         
         Reference = Guid.NewGuid().ToString();
         Title = title;
         CategoryReference = categoryReference;
         Message = message;
         Image = image;
         Activity = activity;
         Comment = comment;
         AssignedTo = assignedTo;
         IsEscalted = isEscalated;
         Status = status;
         TimeStamp = timeStamp;
         UserReference = userReference;
         
      }
     
     public Ticket(CreateTicketDto createTicketDto)
      {
         
         Reference = Guid.NewGuid().ToString();
         Title = createTicketDto.Title;
         CategoryReference= createTicketDto.CategoryReference;
         Message = createTicketDto.Message;
         Image = createTicketDto.Image;  
         UserReference = createTicketDto.UserReference;
         
         
      }
      public Ticket (UpdateTicketDto updateTicketDto)
      {
          Message = updateTicketDto.Message;
         Image = updateTicketDto.Image;  
         UserReference = updateTicketDto.UserReference;
         
      }

      public Ticket()
      {
         
      }
}