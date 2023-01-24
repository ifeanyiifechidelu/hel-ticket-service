using Hel_Ticket_Service.Domain;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using Serilog;
using Hel_Ticket_Service.Domain.AppTicket.Contract;
using System.Text.Json;

namespace Hel_Ticket_Service.Infrastructure;
public class TicketRepository: ITicketRepository
{ 
    readonly IConfiguration _configuration;
    readonly ICacheProvider _cacheProvider;
    readonly IDBProvider _dbProvider;
    readonly IMongoCollection<Ticket> _ticket; 
    readonly ITicketService _ticketService;
    public TicketRepository(IConfiguration configuration,ICacheProvider cacheProvider, IDBProvider dbProvider, ITicketService ticketService)
    {
        _cacheProvider = cacheProvider;
        _dbProvider = dbProvider;
        _configuration = configuration;
        _ticketService = ticketService;
        _ticket =( (IMongoDatabase)_dbProvider.Connect()).GetCollection<Ticket>(nameof(Ticket).ToLower());
    }
    public async Task<string> CreateTicket(CreateTicketDto createTicketDto)
    {
        Log.Information("Validating input data {0} ...", createTicketDto);

        var validationError = _ticketService.ValidateCreateTicketDto(createTicketDto);
        if (validationError!= null) 
        {  
            Log.Error("Error validating input data: " + JsonSerializer.Serialize(createTicketDto));
            throw new AppException(validationError.ErrorData);
        }
        Log.Information("Mapping Ticket Data");
        var ticket = new Ticket(createTicketDto); //map the dto to the ticket object
        Log.Information("Inserting Ticket Data");
        await _ticket.InsertOneAsync(ticket);
        Log.Information("Data Inserted");
        return ticket.Reference;
    }
    public async Task<string?> UpdateTicket(string reference, UpdateTicketDto updateTicketDto)
    {
         Log.Information("Validating input data {0} ...", updateTicketDto);

        var validationError = _ticketService.ValidateUpdateTicketDto(updateTicketDto);
        if (validationError!= null) 
        {  
            Log.Error("Error validating input data: " + JsonSerializer.Serialize(updateTicketDto));
            throw new AppException(validationError.ErrorData);
        }

        Log.Information("Getting data by reference {0} ...", reference);
        var ticket = GetTicketByReference(reference).Result;
        Log.Information("Mapping Data");
        ticket = new Ticket(updateTicketDto);
        Log.Information("Updating Data");
        var result = await _ticket.ReplaceOneAsync(ticket => ticket.Reference == reference, ticket);
        Log.Information("Data Updated");
        return result.ModifiedCount != 0 ? reference: null;
    }
    public async Task<string?> DeleteTicket(string reference)
    {
        Log.Information("Deleting data");
         var result = await _ticket.DeleteOneAsync(ticket => ticket.Reference == reference);
         Log.Information("Data Deleted");
         return result.DeletedCount != 0? reference: null;
    }
    public async Task<Ticket> GetTicketByReference(string reference)
    {
        Log.Information("Getting data by reference {0}", reference);
        var data = await _cacheProvider.GetFromCache<Ticket>(reference); // Get data from cache
        if (data is not null) return data;
        data = await _ticket.Find(ticket => ticket.Reference == reference).FirstOrDefaultAsync();
        await _cacheProvider.SetToCache(reference,data); // Set cache
        return data;
    }

    public async Task<TicketsSummaryDto> GetTicketsSummary()
    {
        Log.Information("Getting ticket summary");
        var data = await _cacheProvider.GetFromCache<TicketsSummaryDto>("ticket_summary"); // Get data from cache
        if (data is not null) return data;

        var openTickets = await _ticket.CountDocumentsAsync(ticket => ticket.Status == "OPEN");
        var closedTickets = await _ticket.CountDocumentsAsync(ticket => ticket.Status == "CLOSED");
        var activeTickets = await _ticket.CountDocumentsAsync(ticket => ticket.Status == "ACTIVE");
        var escalatedTickets = await _ticket.CountDocumentsAsync(ticket => ticket.IsEscalted == true);


        var totalSummary = new TicketsSummaryDto
        {
            TotalOpenTickets = (int)openTickets,
            TotalClosedTickets = (int)closedTickets,
            TotalActiveTickets = (int)activeTickets,
            TotalEscalatedTickets = (int)escalatedTickets
        };

        await _cacheProvider.SetToCache("ticket_summary", totalSummary); // Set cache
        return totalSummary;
    }


    public async Task<List<Ticket>> GetEscalatedTicketsByUser(string reference)
    {
        Log.Information("Getting escalated tickets for user {0}", reference);
        var data = await _cacheProvider.GetFromCache<List<Ticket>>("escalated_tickets_" + reference); // Get data from cache
        if (data is not null) return data;
        data = await _ticket.Find(ticket => ticket.IsEscalted == true && ticket.Reference == reference).ToListAsync();
        await _cacheProvider.SetToCache("escalated_tickets_" + reference, data); // Set cache
        return data;
    }

    public async Task<List<Ticket>> GetEscalatedTicketsToAdmin(string reference)
{
    Log.Information("Getting escalated tickets assigned to user {0}", reference);
    var data = await _cacheProvider.GetFromCache<List<Ticket>>("escalated_tickets_assigned_" + reference); // Get data from cache
    if (data is not null) return data;
    data = await _ticket.Find(ticket => ticket.IsEscalted == true && ticket.AssignedTo == reference).ToListAsync();
    await _cacheProvider.SetToCache("escalated_tickets_assigned_" + reference, data); // Set cache
    return data;
}


    // public async Task<List<Ticket>> GetTicketList(string page)
    // {
    //     Log.Information("Getting data by page {0}", page);
    //     var data = await _cacheProvider.GetFromCache<List<Ticket>>(page); // Get data from cache
    //     if (data is not null) return data;
    //     data = await _ticket.Find(ticket => true).Skip((Convert.ToInt16(page)-1) * _dbProvider.GetPageLimit())
    //     .Limit(_dbProvider.GetPageLimit()).ToListAsync();
    //     await _cacheProvider.SetToCache(page,data); // Set cache
    //     return data;
    // }
}