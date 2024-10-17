using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationTypes{

    public class AnaesthesiaTime : IValueObject {

        public Duration duration { get; private set; }

        public AnaesthesiaTime(int duration) {
            this.duration = new Duration(duration);
        }
    
    }





}