using System;
using System.ComponentModel.DataAnnotations;

namespace DDDSample1.Domain.OperationTypes;

public class OperationTypeDTO {

    public Guid id { get; set; } = Guid.NewGuid();

    [Required]
    public string name { get; set; }

    [Required]
    public int estimatedDuration { get; set; }

    [Required]
    public int anaesthesiaTime { get; set; }

    [Required]
    public int surgeryTime { get; set; }

    [Required]
    public int cleaningTime { get; set; }


}
