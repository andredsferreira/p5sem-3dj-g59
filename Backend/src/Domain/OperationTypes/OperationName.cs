using System;
using Backend.Domain.Shared;

namespace Backend.Domain.OperationTypes;

public class OperationName : IValueObject {

    public string name { get; private set; }

    public OperationName(string name) {
        if (string.IsNullOrEmpty(name)) {
            throw new ArgumentException("Operation name can't be empty");
        }
        this.name = name;
    }

    public override string ToString() {
        return name;
    }

}
