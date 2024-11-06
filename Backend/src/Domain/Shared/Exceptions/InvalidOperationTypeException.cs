namespace Backend.Domain.Shared.Exceptions;

public class InvalidOperationTypeException : BusinessRuleValidationException {

    public InvalidOperationTypeException(string message) : base(message) {

    }
}