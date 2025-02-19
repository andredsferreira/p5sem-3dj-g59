using System.Data;
using System.Text.RegularExpressions;
using Backend.Domain.Shared;

namespace Backend.Domain.Patients;

public class MedicalRecordNumber : IValueObject {
    public string Record;
    public MedicalRecordNumber(string Record) {
        var r = new Regex("[0-9]{12}");
        if (r.IsMatch(Record)) this.Record = Record;
        else throw new InvalidExpressionException();
    }
    public override string ToString() { return Record; }
}