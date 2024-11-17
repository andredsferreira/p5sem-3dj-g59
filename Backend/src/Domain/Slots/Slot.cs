using System;

namespace Backend.Domain.Slots;

public class Slot {
    public TimeOnly begin, end;
    public Slot(string slot){
        Console.WriteLine(slot);
        slot = slot[1..^1];
        string[] times = slot.Split(",");
        begin = TimeOnly.Parse(times[0]);
        end = TimeOnly.Parse(times[1]);
    }
    public Slot(TimeOnly begin, TimeOnly end){
        if(begin.CompareTo(end) > 0) throw new Exception("End time cannot be before Start time");
        this.begin = begin;
        this.end = end;
    }
    public override string ToString()
    {
        return "["+begin.ToString()+","+end.ToString()+"]";
    }
}