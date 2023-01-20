
namespace Hel_Ticket_Service.Domain;

public abstract class EventBaseEto<T> where T : class
{
    public Guid EventID { get; set; }
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public string EventType { get; set; }
    public string EventDomain { get; set; }
    public string EventSource { get; set; }
    public Guid? EventUserID { get; set; }
    public Guid? CorrelationID{get;set;}
    public T EventData { get; set; }

    public EventBaseEto(T t, string eventType) 
    {
        EventID = Guid.NewGuid();
        EventDate = DateTime.Now;
        EventSource = nameof(T);
        EventDomain = nameof(T);
        EventType = eventType;
        EventName = nameof(T);
        CorrelationID = Guid.Empty;
        EventUserID = Guid.Empty;
        EventData = t;
    }
    
}
