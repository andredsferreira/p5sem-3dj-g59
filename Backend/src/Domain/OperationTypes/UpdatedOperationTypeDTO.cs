using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.OperationTypes;


public class UpdatedOperationTypeDTO
{
    [Required]
    [FromForm(Name = "name")]
    public string name { get; set; }

    [Required]
    [FromForm(Name = "anaesthesiaTime")]
    public int anaesthesiaTime { get; set; }

    [Required]
    [FromForm(Name = "surgeryTime")]
    public int surgeryTime { get; set; }

    [Required]
    [FromForm(Name = "cleaningTime")]
    public int cleaningTime { get; set; }

    [Required]
    [FromForm(Name = "minDoctor")]
    public int MinDoctor { get; set; }

    [Required]
    [FromForm(Name = "minAnesthetist")]
    public int MinAnesthetist { get; set; }

    [Required]
    [FromForm(Name = "minInstrumentingNurse")]
    public int MinInstrumentingNurse { get; set; }

    [Required]
    [FromForm(Name = "minCirculatingNurse")]
    public int MinCirculatingNurse { get; set; }

    [Required]
    [FromForm(Name = "minNurseAnaesthetist")]
    public int MinNurseAnaesthetist { get; set; }

    [Required]
    [FromForm(Name = "minXRayTechnician")]
    public int MinXRayTechnician { get; set; }

    [Required]
    [FromForm(Name = "minMedicalActionAssistant")]
    public int MinMedicalActionAssistant { get; set; }
    

    public UpdatedOperationTypeDTO(string name, int anaesthesiaTime, int surgeryTime, int cleaningTime)
    {
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
    }

    public UpdatedOperationTypeDTO(string name, int anaesthesiaTime, int surgeryTime, int cleaningTime, int MinDoctor, int MinAnesthetist, int MinInstrumentingNurse, int MinCirculatingNurse, int MinNurseAnaesthetist, int MinXRayTechnician, int MinMedicalActionAssistant)
    {
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
        this.MinDoctor = MinDoctor;
        this.MinAnesthetist = MinAnesthetist;
        this.MinInstrumentingNurse = MinInstrumentingNurse;
        this.MinCirculatingNurse = MinCirculatingNurse;
        this.MinNurseAnaesthetist = MinNurseAnaesthetist;
        this.MinXRayTechnician = MinXRayTechnician;
        this.MinMedicalActionAssistant = MinMedicalActionAssistant;
    }

    public UpdatedOperationTypeDTO()
    {
    }
}