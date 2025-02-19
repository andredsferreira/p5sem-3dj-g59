using System;
using Backend.Domain.Shared;

namespace Backend.Domain.OperationTypes;

public class CleaningTime : IValueObject {

    public int duration { get; private set; }

    public CleaningTime(int duration) {
        if (duration < 0) {
            throw new ArgumentException("Duration can't be negative");
        }
        this.duration = duration;
    }


}