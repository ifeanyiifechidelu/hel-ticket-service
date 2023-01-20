using System.ComponentModel.DataAnnotations;
using FluentValidation.Validators;

namespace Hel_Ticket_Service.Domain;

public interface ITicketService{
      
      AppException ValidateCreateTicketDto(CreateTicketDto createTicketDto);
      AppException ValidateUpdateTicketDto(UpdateTicketDto updateTicketDto);
      
}

