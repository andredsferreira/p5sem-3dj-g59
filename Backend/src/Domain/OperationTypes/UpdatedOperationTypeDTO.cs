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

    public UpdatedOperationTypeDTO(string name, int anaesthesiaTime, int surgeryTime, int cleaningTime)
    {
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
    }
}