using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;


namespace DDDSample1.Domain.OperationTypes {

    public class OperationType : Entity<OperationTypeID>, IAggregateRoot {

        public OperationTypeID id { get; private set; }
        public OperationName name { get; private set; }
        public EstimatedDuration estimatedDuration { get; private set; }
        
        public OperationType(OperationTypeID id, OperationName name, EstimatedDuration estimatedDuration) {
            this.id = id;
            this.name = name;
            this.estimatedDuration = estimatedDuration;
        }

        public static OperationType createFromDTO(OperationTypeDTO dto) {
            throw new NotImplementedException();
        }

    }
    
}