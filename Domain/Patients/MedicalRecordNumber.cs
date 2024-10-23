using System.Data;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.Patients;

public partial class MedicalRecordNumber
{
    public string Record;
    public MedicalRecordNumber(string Record){
        var r = MyRegex();
        if (r.IsMatch(Record)) this.Record = Record;
        else throw new InvalidExpressionException();
    }

    [GeneratedRegex("[0-9]{12}")]
    private static partial Regex MyRegex();
}