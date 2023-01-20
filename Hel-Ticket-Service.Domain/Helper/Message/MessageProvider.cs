namespace Hel_Ticket_Service.Domain;

public static class MessageProvider
{
    public const string MongoDBDown = "MongoDB Server is down";
    public const string RedisDBDown = "Redis Server is down";

    public const string UserNotFound = "User Not Found";
    public const string UserAlreadyExists = "User Already Exists";
    public const string UserEmailAlreadyExists = "User Email Already Exists";
    public const string UserCredentialInvalid  ="Invalid Credentials. Please stand advised";
    public const string InvalidParameters  ="Parameters must be in the required format and must not be null. Please stand advised.";
    public const string UserNameNull = "Username must not be empty. Please stand advised.";
    public const string PasswordNull = "Password must not be empty. Please stand advised.";


}