using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace Backend.Domain.Specializations
{
    public class SpecializationDTO
    {
        [Required]
        [FromForm(Name = "codeSpec")]
        public string codeSpec { get; set; }

        [Required]
        [FromForm(Name = "Designation")]
        public string designation { get; set; }

        [Required]
        [FromForm(Name = "description")]
        public string description { get; set; }

        public SpecializationDTO()
        {
        }

        public SpecializationDTO(string codeSpec, string designation, string description)
        {
            this.codeSpec = codeSpec;
            this.designation = designation;
            this.description = description;
        }
    }


}