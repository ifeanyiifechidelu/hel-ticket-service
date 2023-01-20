namespace Hel_Ticket_Service.Domain;

public interface IServiceProvider
{
    void MapConfig();
    void ReadConfig(string envFilePath);
    
}
    
