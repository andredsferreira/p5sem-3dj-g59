namespace DDDSample1.Domain.Shared.Exceptions;

public class PatientNotFoundException : BusinessRuleValidationException {

    public PatientNotFoundException(string message) : base(message) {

    }
}