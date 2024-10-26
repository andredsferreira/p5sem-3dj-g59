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



}
