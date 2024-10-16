using System.Collections.Generic;
using System.Linq;

namespace DDDSample1.Domain.Shared {
    public class FullName : IValueObject {
        public string FirstName { get; set; }
        public List<string> MiddleNames { get; set; }
        public string LastName { get; set; }
        public FullName(string FirstName, List<string> MiddleNames, string LastName){
            this.FirstName = FirstName;
            this.MiddleNames = MiddleNames;
            this.LastName = LastName;
        }
        public FullName(string Names){
            List<string> NameList = [.. Names.Split(" ")];
            FirstName = NameList.First();
            LastName = NameList.Last();
            MiddleNames = NameList.GetRange(1,NameList.Count-2);
        }
    }
}