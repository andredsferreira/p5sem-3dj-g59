using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationTypes;

public class AnaesthesiaTime : IValueObject {

    public int duration { get; private set; }

    public AnaesthesiaTime(int duration) {
        if (duration < 0) {
            throw new ArgumentException("Duration can't be negative");
        }
        this.duration = duration;
    }

}
