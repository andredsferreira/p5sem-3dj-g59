using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationTypes
{
    public class AddOperationTypeService
    {
        private readonly IOperationTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddOperationTypeService(IOperationTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;

        }

        

        public async Task<OperationTypeDTO> CreateOperationType(string name, int anaesthesiaTime, int surgeryTime, int cleaningTime)
        {
            OperationType operationType = new OperationType(new OperationName(name),
            new EstimatedDuration(new AnaesthesiaTime(anaesthesiaTime), new SurgeryTime(surgeryTime), new CleaningTime(cleaningTime)));

            await _repository.AddAsync(operationType);

            return new OperationTypeDTO()
            {
                id = operationType.Id.ToString(),
                name = operationType.name.ToString(),
                estimatedDuration = operationType.estimatedDuration.totalDuration.value,
                anaesthesiaTime = operationType.estimatedDuration.anaesthesiaTime.duration.value,
                surgeryTime = operationType.estimatedDuration.surgeryTime.duration.value,
                cleaningTime = operationType.estimatedDuration.cleaningTime.duration.value
            };
        }
    }
}