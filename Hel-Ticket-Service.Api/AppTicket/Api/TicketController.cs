
using Microsoft.AspNetCore.Authorization;
using System.DirectoryServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Hel_Ticket_Service.Domain.AppTicket.Contract;
using Hel_Ticket_Service.Domain;

namespace Hel_Ticket_Service.Api;
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
       
        readonly ITicketRepository _ticketRepository ; 
        readonly ITicketService _ticketService;
        public TicketController(ITicketRepository ticketRepository, ITicketService ticketService) {
            _ticketRepository = ticketRepository;
            _ticketService    = ticketService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            
                return Ok (await _ticketRepository.CreateTicket(createTicketDto));
            
        }
        [HttpPut("{reference}")]
        public async Task<ActionResult<string>> UpdateTicket(string reference,[FromBody] UpdateTicketDto updateTicketDto)
        {

                return Ok(await _ticketRepository.UpdateTicket(reference,updateTicketDto));

        }
        [HttpDelete("{reference}")]
        public async Task<ActionResult<string?>> DeleteTicket(string reference)
        {

                return Ok(await _ticketRepository.DeleteTicket(reference));
  
        }
        [HttpGet("{reference}")]
        public async Task<ActionResult<Ticket>> GetTicketByReference(string reference)
        {
  
                return Ok( await _ticketRepository.GetTicketByReference(reference));
           
        }


        [HttpGet("{get-ticket-summary}")]
        public async Task<ActionResult<TicketsSummaryDto>> GetTicketsSummary()
        {
            var result = await _ticketRepository.GetTicketsSummary();

            return Ok(result);
        }

        [HttpGet("{get-escalated-ticket-by-user}")]
        public async Task<ActionResult<Ticket>> GetEscalatedTicketsByUser(string user)
        {
            var result = await _ticketRepository.GetEscalatedTicketsByUser(user);

            return Ok(result);
        }

        [HttpGet("{get-escalated-ticket-by-user}")]
        public async Task<ActionResult<Ticket>> GetEscalatedTicketsToAdmin(string admin)
        {
            var result = await _ticketRepository.GetEscalatedTicketsByUser(admin);

            return Ok(result);
        }

        // [HttpGet("list/{page}")]
        // public async Task<ActionResult<List<Ticket>>> GetTicketList(string page)
        // {

        //         return Ok( await _ticketRepository.GetTicketList(page));
        // }
    }

