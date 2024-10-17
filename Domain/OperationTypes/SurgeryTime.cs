using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationTypes{

    public class SurgeryTime : IValueObject {

        public Duration duration { get; private set; }

        public SurgeryTime(int duration) {
            this.duration = new Duration(duration);
        }
    }

}
