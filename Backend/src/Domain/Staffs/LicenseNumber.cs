using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs;

public class LicenseNumber : IValueObject {
    public string License;
    public LicenseNumber(string License){
        //Put regex logic here, see MedicalRecordNumber as an example

        this.License = License;
    }
    public override string ToString(){return License;}
}
