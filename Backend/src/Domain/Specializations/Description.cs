using System;
using System.Collections.Generic;
using System.ComponentModel;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared;

namespace Backend.Domain.Specializations;

public class Description : IValueObject{

    string description { get; set; }

    public Description() {
    }

    public Description(string description) {
        this.description = description;
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        Description description = (Description)obj;
        return this.description.Equals(description.description);
    }

    public override int GetHashCode() {
        return description.GetHashCode();
    }

    public override string ToString() {
        return description;
    }


}