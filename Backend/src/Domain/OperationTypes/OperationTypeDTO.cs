using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domain.OperationTypes;

public class OperationTypeDTO {

    [FromForm (Name = "id")]
    public Guid id { get; set; }

    [Required]
    [FromForm (Name = "name")]
    public string name { get; set; }

    [Required]
    [FromForm (Name = "anaesthesiaTime")]
    public int anaesthesiaTime { get; set; } 

    [Required]
    [FromForm (Name = "surgeryTime")]
    public int surgeryTime { get; set; } 

    [Required]
    [FromForm (Name = "cleaningTime")]
    public int cleaningTime { get; set; }
    
    [Required]
    [FromForm (Name = "status")]
    public string Status { get; set; }

    [FromForm (Name = "specializationId")]
    public string SpecializationId { get; set; }

    [FromForm (Name = "minDoctor")]
    public int minDoctor { get; set; }
    
    [FromForm (Name = "minAnesthetist")]
    public int minAnaesthetist { get; set; }

    [FromForm (Name = "minInstrumentingNurse")]
    public int minInstrumentingNurse { get; set; }

    [FromForm (Name = "minCirculatingNurse")]
    public int minCirculatingNurse { get; set; }

    [FromForm (Name = "minNurseAnaesthetist")]
    public int minNurseAnaesthetist { get; set; }

    [FromForm (Name = "minXRayTechnician")]
    public int minXRayTechnician { get; set; }

    [FromForm (Name = "minMedicalActionAssistant")]
    public int minMedicalActionAssistant { get; set; }


    public OperationTypeDTO() {
    }

    public OperationTypeDTO(string name, int anaesthesiaTime, int surgeryTime, int cleaningTime, string Status) {
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
        this.Status = Status;
    }

    public OperationTypeDTO(string name, int anaesthesiaTime, int surgeryTime, int cleaningTime) {
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
    }

    public OperationTypeDTO(Guid id, string name, int anaesthesiaTime, int surgeryTime, int cleaningTime, string Status) {
        this.id = id;
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
        this.Status = Status;
    }

    public OperationTypeDTO(Guid id, string name, int anaesthesiaTime, int surgeryTime, int cleaningTime, string Status, string Specialization, int MinDoctor, int MinAnesthetist, int MinInstrumentingNurse, int MinCirculatingNurse, int MinNurseAnaesthetist, int MinXRayTechnician, int MinMedicalActionAssistant) {
        this.id = id;
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
        this.Status = Status;
        this.SpecializationId = Specialization;
        this.minDoctor = MinDoctor;
        this.minAnaesthetist = MinAnesthetist;
        this.minInstrumentingNurse = MinInstrumentingNurse;
        this.minCirculatingNurse = MinCirculatingNurse;
        this.minNurseAnaesthetist = MinNurseAnaesthetist;
        this.minXRayTechnician = MinXRayTechnician;
        this.minMedicalActionAssistant = MinMedicalActionAssistant;
    }
    public OperationTypeDTO( string name, int anaesthesiaTime, int surgeryTime, int cleaningTime, string Status, string Specialization, int MinDoctor, int MinAnesthetist, int MinInstrumentingNurse, int MinCirculatingNurse, int MinNurseAnaesthetist, int MinXRayTechnician, int MinMedicalActionAssistant) {
        
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
        this.Status = Status;
        this.SpecializationId = Specialization;
        this.minDoctor = MinDoctor;
        this.minAnaesthetist = MinAnesthetist;
        this.minInstrumentingNurse = MinInstrumentingNurse;
        this.minCirculatingNurse = MinCirculatingNurse;
        this.minNurseAnaesthetist = MinNurseAnaesthetist;
        this.minXRayTechnician = MinXRayTechnician;
        this.minMedicalActionAssistant = MinMedicalActionAssistant;
    }



}
