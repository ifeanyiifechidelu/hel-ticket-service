

namespace Hel_Ticket_Service.Domain;
public interface ICacheProvider
{
    Task<T> GetFromCache<T>(string reference) where T : class;
    Task SetToCache<T>(string reference,T value, 
    TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null) where T : class;
    Task ClearCache<T>(string reference) where T : class;
}
    
