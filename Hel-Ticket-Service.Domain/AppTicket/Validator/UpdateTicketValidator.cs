using FluentValidation;
using FluentValidation.Results;

namespace Hel_Ticket_Service.Domain;

public class UpdateTicketValidator : AbstractValidator<UpdateTicketDto>
{
    public UpdateTicketValidator()
    {
          
            RuleFor(ticket => ticket.Message).NotEmpty()
            .WithMessage("Message must not be empty. Please stand advised.");
            RuleFor(ticket => ticket.Image).NotEmpty()
            .WithMessage("Image must not be empty. Please stand advised.");
            RuleFor(ticket => ticket.UserReference).NotEmpty()
            .WithMessage("UserReference must not be empty. Please stand advised.");    
           
           
    }
        public override ValidationResult Validate(ValidationContext<UpdateTicketDto> context)
    {
        return context.InstanceToValidate == null
            ? new ValidationResult(new[] { new ValidationFailure(nameof(UpdateTicketDto), 
            "Parameters must be in the required format and must not be null. Please stand advised.") })
            : base.Validate(context);
    }
}  