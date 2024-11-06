namespace Backend.Domain.Shared.Exceptions;

public class EmptyUserNameException : BusinessRuleValidationException {

    public EmptyUserNameException(string message) : base(message) {

    }
}