
using System.ComponentModel.DataAnnotations;
namespace Hel_Ticket_Service.Domain;
public static class Service
{
    
    public static string Url { get; set; }
    public static string Name{get; set; }
    public static string Mode{get; set; }
    public static string LogFileName{get; set; }
    public static string LogDirectory{get; set;}
    public static string EventBusUrl{get; set; }
    public static string LaunchUrl{get; set; }
  
}

