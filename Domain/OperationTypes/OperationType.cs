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

        public OperationType(OperationName name, EstimatedDuration estimatedDuration) {
            
            this.id = new OperationTypeID(Guid.NewGuid().ToString());
            this.name = name;
            this.estimatedDuration = estimatedDuration;
        }

        public static OperationType createFromDTO(OperationTypeDTO dto) {
            return new OperationType(new OperationTypeID(dto.id), new OperationName(dto.name), 
            new EstimatedDuration(new AnaesthesiaTime(dto.anaesthesiaTime), new SurgeryTime (dto.surgeryTime),new CleaningTime (dto.cleaningTime)));   
        }

    }
    
}