namespace DDDSample1.Domain.Shared.Exceptions;

public class StaffNotRegisteredException : BusinessRuleValidationException {

    public StaffNotRegisteredException(string message) : base(message) {

    }
}