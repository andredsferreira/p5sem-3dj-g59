using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs;

public class LicenseNumber : EntityId {

    [JsonConstructor]
    public LicenseNumber(Guid value) : base(value) {
    }

    public LicenseNumber(object value) : base(value) {
    }

    public override string AsString() {
        // Retorna o valor GUID como uma string no formato padrão
        return Value.ToString();
    }

    protected override object createFromString(string text) {
        // Converte a string para GUID, se possível, senão lança uma exceção
        if (Guid.TryParse(text, out Guid result)) {
            return result;
        }
        throw new FormatException("Invalid LicenseNumber format");
    }
}
