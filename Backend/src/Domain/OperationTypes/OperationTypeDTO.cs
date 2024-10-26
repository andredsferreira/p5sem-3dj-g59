using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace DDDSample1.Domain.OperationTypes;

public class OperationTypeDTO (string name,  int anaesthesiaTime, int surgeryTime, int cleaningTime, string status){

    //public Guid id { get; set; } = id;

    [Required]
    public string name { get; set; } = name;

    [Required]
    public int anaesthesiaTime { get; set; } = anaesthesiaTime;

    [Required]
    public int surgeryTime { get; set; } = surgeryTime;

    [Required]
    public int cleaningTime { get; set; } = cleaningTime;
    [Required]
    public string Status { get; set; } = status;



}
