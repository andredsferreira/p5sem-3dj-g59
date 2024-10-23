using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationTypes;

public class SurgeryTime : IValueObject {

    public int duration { get; private set; }

    public SurgeryTime(int duration) {
        if (duration < 0) {
            throw new ArgumentException("Duration can't be negative");
        }
        this.duration = duration;
    }
}
