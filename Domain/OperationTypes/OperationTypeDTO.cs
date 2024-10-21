using System;
using System.ComponentModel.DataAnnotations;

namespace DDDSample1.Domain.OperationTypes;

public class OperationTypeDTO {

    public Guid id { get; set; }

    [Required]
    public string name { get; set; }

    [Required]
    public int anaesthesiaTime { get; set; }

    [Required]
    public int surgeryTime { get; set; }

    [Required]
    public int cleaningTime { get; set; }


}
