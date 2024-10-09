using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs {

    public class LicenseNumber : EntityId {

        [JsonConstructor]
        public LicenseNumber(Guid value) : base(value) {

        }


        public LicenseNumber(object value) : base(value) {

        }

        public override string AsString() {
            throw new System.NotImplementedException();
        }

        protected override object createFromString(string text) {
            throw new System.NotImplementedException();
        }
    }

}