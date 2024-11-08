using Backend.Domain.Shared;

namespace Backend.Domain.Staffs;

public class LicenseNumber : IValueObject {
    public string License;
    public LicenseNumber(string License) {
        this.License = License;
    }
    public override string ToString() { return License; }
}
