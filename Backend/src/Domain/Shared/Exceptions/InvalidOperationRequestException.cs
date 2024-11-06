namespace Backend.Domain.Shared.Exceptions;

public class InvalidOperationRequestException : BusinessRuleValidationException {

    public InvalidOperationRequestException(string message) : base(message) {

    }

}