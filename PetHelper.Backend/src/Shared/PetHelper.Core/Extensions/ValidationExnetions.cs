using System.Runtime.InteropServices.JavaScript;
using FluentValidation.Results;
using PetHelper.SharedKernel;

namespace PetHelper.Core.Extensions;

public static class ValidationExnetions
{
    public static ErrorList ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;
            
        var errors = from validationError in validationErrors
            let errorMessage = validationError.ErrorMessage
            let error = Error.Deserialize(errorMessage)
            select Error.Validation(error.Code, error.Message, validationError.PropertyName);
            
        return errors.ToList();
    }
}