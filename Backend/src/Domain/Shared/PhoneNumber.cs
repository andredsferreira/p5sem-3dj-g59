using System;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.Shared;

public class PhoneNumber : IValueObject {
    public string value;
    public PhoneNumber(string value){
        var r = new Regex("[0-9]{9}");
        if (!r.IsMatch(value)) throw new FormatException();
        this.value = value;
    }
    public override string ToString(){return value;}
}