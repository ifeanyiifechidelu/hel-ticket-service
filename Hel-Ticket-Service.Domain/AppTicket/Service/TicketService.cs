using System.Text.Json;
using FluentValidation.Results;


namespace Hel_Ticket_Service.Domain;

public class TicketService:ITicketService
{
      
      public AppException ValidateCreateTicketDto(CreateTicketDto createTicketDto)
      {
        return new ErrorService().GetValidationExceptionResult(new CreateTicketValidator().Validate(createTicketDto));
      }   
      public AppException ValidateUpdateTicketDto(UpdateTicketDto updateTicketDto)
      {
        return new ErrorService().GetValidationExceptionResult(new UpdateTicketValidator().Validate(updateTicketDto));
      }   
}