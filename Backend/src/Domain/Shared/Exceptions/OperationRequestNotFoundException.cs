namespace DDDSample1.Domain.Shared.Exceptions;

public class OperationRequestNotFoundException : BusinessRuleValidationException {

    public OperationRequestNotFoundException(string message) : base(message) {

    }

}