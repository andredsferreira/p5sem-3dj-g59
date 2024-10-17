using DDDSample1.Domain.Shared;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using DDDSample1.Domain.OperationTypes;

namespace DDDSample1.Domain.OperationTypes{

    public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeID> , IOperationTypeRepository {

        public OperationTypeRepository(DDDSample1DbContext context) : base(context.OperationTypes) {
        }

    }



}


