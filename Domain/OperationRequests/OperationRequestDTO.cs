using System;
using System.ComponentModel.DataAnnotations;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staffs;


namespace DDDSample1.Domain.OperationRequests {

    public class OperationRequestDTO {

        public Guid id { get; set; }

        public PatientDTO patientDTO { get; set; }

        public OperationTypeDTO operationTypeDTO { get; set; }

        public StaffDTO staffDTO { get; set; }

        public string priority { get; private set; }

        public DateTime dateTime { get; private set; }

        public RequestStatus requestStatus { get; private set; }

    }

}