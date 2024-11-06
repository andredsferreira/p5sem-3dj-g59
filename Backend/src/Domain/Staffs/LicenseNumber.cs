using Backend.Domain.Shared;

namespace Backend.Domain.Staffs;

public class LicenseNumber : IValueObject {
    public string License;
    public LicenseNumber(string License) {
        //Put regex logic here, see MedicalRecordNumber as an example

        this.License = License;
    }
    public override string ToString() { return License; }
}
