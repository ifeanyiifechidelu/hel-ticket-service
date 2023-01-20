

using System.Globalization;
namespace Hel_Ticket_Service.Domain;

public class AppException : Exception
{
    public AppException(string[] errorData,string exceptionType = "VALIDATION", int statusCode  = 400) : base()
    {
      
        ExceptionType = exceptionType;
        StatusCode = statusCode;
        ErrorData = errorData;
      

    }
   
    public int StatusCode{ get; set; }
    public string ExceptionType{ get; set; }
    public string[] ErrorData{ get; set; }
   
}

public class AppExceptionResponse
{
    public AppExceptionResponse(AppException exception)
    {
        Reference = Guid.NewGuid().ToString();
        StatusCode = exception.StatusCode;
        ExceptionType = exception.ExceptionType;
        ErrorData = exception.ErrorData;
        TimeStamp = DateTime.Now;
        
    }
    public AppExceptionResponse(){}
    public string Reference{ get; internal set; }
    public int StatusCode{ get; set; }
    public string ExceptionType{ get; set; }
    public string[] ErrorData{ get; set; }
    public DateTime TimeStamp{ get; internal set; }
}
