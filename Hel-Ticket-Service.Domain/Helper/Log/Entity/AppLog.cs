namespace Hel_Ticket_Service.Domain;


public class AppLog
{
    public string Method{get;set;}
    public string Level{get;set;}
    public int LevelValue{get;set;}
    public int StatusCode{get;set;}
    public string Path{get;set;}
    public string UserAgent{get;set;}
    public string ForwardedFor{get;set;}
    public string ResponseTimeout{get;set;}
    public string Payload{get;set;}
    public string Message{get;set;}
    public string Version{get;set;}
    public string CorrelationId{get;set;}
    public string AppName{get;set;}
    public string ApplicationHost{get;set;}
    public string LoggerName{get;set;}
    public DateTime Timestamp{get;set;}

    public AppLog(string level,int statusCode, string message)
    {
        Level = level;
        StatusCode = statusCode;
        Message = message;
        Timestamp = DateTime.Now;
    }
    public AppLog()
    {

    }

}