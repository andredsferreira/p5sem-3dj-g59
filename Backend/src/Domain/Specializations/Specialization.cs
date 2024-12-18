using System;
using Backend.Domain.Shared;
namespace Backend.Domain.Specializations;

public class Specialization : Entity<SpecializationID>, IAggregateRoot
{

    public CodeSpec codeSpec { get; set; }
    public Designation designation { get; set; }
    public Description description { get; set; }

    public Specialization()
    {
    }

    public Specialization(CodeSpec codeSpec, Designation designation, Description description)
    {
        this.codeSpec = codeSpec;
        this.designation = designation;
        this.description = description;
    }

    public Specialization(SpecializationID id, CodeSpec codeSpec, Designation designation, Description description)
    {
        this.Id = id;
        this.codeSpec = codeSpec;
        this.designation = designation;
        this.description = description;
    }

    public SpecializationDTO ToDTO()
    {
        return new SpecializationDTO(codeSpec.ToString(), designation.ToString(), description.ToString());
    }

    public static Specialization FromDTO(SpecializationDTO specializationDTO)
    {
        return new Specialization(new CodeSpec(specializationDTO.codeSpec), new Designation(specializationDTO.designation), new Description(specializationDTO.description));
    }

}