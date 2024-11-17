using System;
using System.Collections.Generic;
using Backend.Domain.Appointments;
using Backend.Domain.OperationTypes;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Backend.Domain.Staffs;

namespace Backend.Domain.OperationRequests;

public class OperationRequest : Entity<OperationRequestId>, IAggregateRoot {

    public StaffId staffId { get; set; }

    public Staff staff { get; set; }

    public PatientId patientId { get; set; }

    public Patient patient { get; set; }

    public OperationTypeId operationTypeId { get; set; }

    public OperationType operationType { get; set; }

    public OperationRequestPriority priority { get; set; } = OperationRequestPriority.Elective;

    public DateTime dateTime { get; set; } = DateTime.Now;

    public OperationRequestStatus requestStatus { get; set; } = OperationRequestStatus.Pending;

    public ICollection<Appointment> Appointments { get; set; } = [];


}
