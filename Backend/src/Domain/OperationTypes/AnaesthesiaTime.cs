using System;
using Backend.Domain.Shared;

namespace Backend.Domain.OperationTypes;

public class AnaesthesiaTime : IValueObject {

    public int duration { get; private set; }

    public AnaesthesiaTime(int duration) {
        if (duration < 0) {
            throw new ArgumentException("Duration can't be negative");
        }
        this.duration = duration;
    }

}
