using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared;

namespace Backend.Domain.Specializations;

public class CodeSpec : IValueObject {

    string codeSpec { get; set; }

    public CodeSpec() {
    }

    public CodeSpec(string codeSpec) {
        this.codeSpec = codeSpec;
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        CodeSpec codeSpec = (CodeSpec)obj;
        return this.codeSpec.Equals(codeSpec.codeSpec);
    }

    public override int GetHashCode() {
        return codeSpec.GetHashCode();
    }

    public override string ToString() {
        return codeSpec;
    }


}