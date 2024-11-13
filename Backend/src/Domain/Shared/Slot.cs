using System;
using System.Text.RegularExpressions;

namespace Backend.Domain.Shared;

public class Slot : IValueObject {
    public DateOnly day;
    public TimeOnly begin, end;
    public Slot(DateOnly day, TimeOnly begin, TimeOnly end){
        if(begin.CompareTo(end) > 0) throw new Exception("End time cannot be before Start time");
        this.day = day;
        this.begin = begin;
        this.end = end;
    }
}