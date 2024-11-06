using System.Collections.Generic;
using System.Linq;

namespace Backend.Domain.Shared;

public class FullName : IValueObject {

    public string Full { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    private FullName() {

    }

    public FullName(string Names) {
        Full = Names;
        List<string> NameList = [.. Full.Split(" ")];
        FirstName = NameList.First();
        LastName = NameList.Last();
    }

    public override string ToString() {
        return Full;
    }
}