namespace Backend.Domain.Shared.Exceptions;

public class InvalidOperationRequestPriorityException : BusinessRuleValidationException {
    
    public InvalidOperationRequestPriorityException(string message) : base(message) {

    }
}