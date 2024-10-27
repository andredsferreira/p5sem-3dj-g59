using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.OperationTypes;

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

    [FromForm (Name = "specialization")]
    public string Specialization { get; set; }

    [FromForm (Name = "minDoctor")]
    public int MinDoctor { get; set; }
    
    [FromForm (Name = "minAnesthetist")]
    public int MinAnesthetist { get; set; }

    [FromForm (Name = "minInstrumentingNurse")]
    public int MinInstrumentingNurse { get; set; }

    [FromForm (Name = "minCirculatingNurse")]
    public int MinCirculatingNurse { get; set; }

    [FromForm (Name = "minNurseAnaesthetist")]
    public int MinNurseAnaesthetist { get; set; }

    [FromForm (Name = "minXRayTechnician")]
    public int MinXRayTechnician { get; set; }

    [FromForm (Name = "minMedicalActionAssistant")]
    public int MinMedicalActionAssistant { get; set; }


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
        this.Specialization = Specialization;
        this.MinDoctor = MinDoctor;
        this.MinAnesthetist = MinAnesthetist;
        this.MinInstrumentingNurse = MinInstrumentingNurse;
        this.MinCirculatingNurse = MinCirculatingNurse;
        this.MinNurseAnaesthetist = MinNurseAnaesthetist;
        this.MinXRayTechnician = MinXRayTechnician;
        this.MinMedicalActionAssistant = MinMedicalActionAssistant;
    }



}
