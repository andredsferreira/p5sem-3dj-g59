namespace DDDSample1.Domain.OperationTypes {

    public class OperationTypeDTO {

        public string id { get; set; }
        public string name { get; set; }
        public int estimatedDuration { get; set; }

        public int anaesthesiaTime { get; set; }
        public int surgeryTime { get; set; }
        public int cleaningTime { get; set; }
        
   
    }
}