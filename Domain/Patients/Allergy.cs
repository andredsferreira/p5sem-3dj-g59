using System;

namespace DDDSample1.Domain.Patients;

public class Allergy {

    public Guid allergyId { get; set; }

    public string allergyName { get; set; }

    public Allergy() {
        this.allergyId = Guid.NewGuid();
    }

    public Allergy(string allergyName) {
        this.allergyId = Guid.NewGuid();
        this.allergyName = allergyName;
    }

}