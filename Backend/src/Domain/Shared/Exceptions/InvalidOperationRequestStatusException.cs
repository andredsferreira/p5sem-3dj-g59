namespace Backend.Domain.Shared.Exceptions;

public class INvalidOperationRequestStatusException : BusinessRuleValidationException {

    public INvalidOperationRequestStatusException(string message) : base(message) {

    }
}