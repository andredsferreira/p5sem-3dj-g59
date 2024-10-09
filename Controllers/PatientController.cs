using DDDSample1.Domain.Patients;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase {
        
        private readonly PatientService _patientService;

    }
}