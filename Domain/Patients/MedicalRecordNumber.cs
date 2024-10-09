using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients {

    public class MedicalRecordNumber : EntityId {


        [JsonConstructor]
        public MedicalRecordNumber(Guid value) : base(value) {

        }

        public MedicalRecordNumber(object value) : base(value) {
        }


        public override string AsString() {
            throw new System.NotImplementedException();
        }

        protected override object createFromString(string text) {
            throw new System.NotImplementedException();
        }
    }
}