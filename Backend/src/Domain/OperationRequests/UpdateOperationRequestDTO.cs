using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.OperationRequests;

public class UpdateOperationRequestDTO {

    [Required]
    public Guid updatedId { get; set; }

    [Required]
    public string priority { get; set; }

    [Required]
    public DateTime dateTime { get; set; }

    public UpdateOperationRequestDTO(Guid updatedId, string priority, DateTime dateTime) {
        this.updatedId = updatedId;
        this.priority = priority;
        this.dateTime = dateTime;
    }

}