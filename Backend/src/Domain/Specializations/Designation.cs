
using Backend.Domain.Shared;
using System;

namespace Backend.Domain.Specializations;

public class Designation : IValueObject{

    public string name { get; private set; }

    public Designation(string name) {
        if (string.IsNullOrEmpty(name)) {
            throw new ArgumentException("Designation name can't be empty");
        }
        this.name = name;
    }

    public override string ToString() {
        return name;
    }

}