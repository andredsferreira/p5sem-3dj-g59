using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.OperationRequests;

public class UpdatedOperationRequestDTO {

    [Required]
    [FromForm(Name = "Id")]
    public Guid updatedId { get; set; }

    [Required]
    [FromForm(Name = "priority")]
    public string priority { get; set; }

    [Required]
    [FromForm(Name = "dateTime")]
    public DateTime dateTime { get; set; }

}