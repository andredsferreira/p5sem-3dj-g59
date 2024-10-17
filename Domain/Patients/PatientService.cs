using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients {


    public class PatientService {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IPatientRepository _repository;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository repository){
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<PatientDTO> CreatePatient(PatientDTO dto){
            var patient = Patient.createFromDTO(dto); 
            await this._repository.AddAsync(patient);
            await this._unitOfWork.CommitAsync();

            return dto;
        }
    }

}