using FluentValidation.Results;

namespace Stall.BusinessLogic.Extensions;

public static class ValidationResultExtension
{
    public static IEnumerable<string> GetErrorMessages(this ValidationResult validationResult)
    {
        return validationResult.Errors
            .Select(x => x.ErrorMessage)
            .ToList();
    }
}