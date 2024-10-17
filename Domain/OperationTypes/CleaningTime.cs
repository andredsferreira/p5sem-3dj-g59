using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationTypes{
    public class CleaningTime : IValueObject {
        public Duration duration { get; private set; }

        public CleaningTime(int duration) {
            this.duration = new Duration(duration);
        }


    }
}