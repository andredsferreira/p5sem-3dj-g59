using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationTypes
{

    public class EstimatedDuration : IValueObject
    {

        public AnaesthesiaTime anaesthesiaTime { get; private set; }

        public SurgeryTime surgeryTime { get; private set; }

        public CleaningTime cleaningTime { get; private set; }

        public Duration totalDuration { get; private set; }

        public EstimatedDuration(AnaesthesiaTime anaesthesiaTime, SurgeryTime surgeryTime, CleaningTime cleaningTime)
        {

            this.anaesthesiaTime = anaesthesiaTime;
            this.surgeryTime = surgeryTime;
            this.cleaningTime = cleaningTime;

            this.totalDuration = new Duration(anaesthesiaTime.duration.value + surgeryTime.duration.value + cleaningTime.duration.value);
        }

        

        
    }
}