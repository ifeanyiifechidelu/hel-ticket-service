using System.DirectoryServices.Protocols;
using FluentValidation.Results;

namespace Hel_Ticket_Service.Domain;
public interface IAppExceptionService
   {
      AppException GetValidationExceptionResult(ValidationResult validationResult);
      
   }