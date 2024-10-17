using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationTypes{

    public class OperationName : IValueObject {

        public string name { get; private set; }

        public OperationName(string name) {
            this.name = name;
        }

    }

}