namespace Hel_Ticket_Service.Domain;
using System.ComponentModel.DataAnnotations;
public class Cache
{
    [Required]
    public string Key { get; set; }

    public Cache(string key)
    {
        Key = key;
    }

}