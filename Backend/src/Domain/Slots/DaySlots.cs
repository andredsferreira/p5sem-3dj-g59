using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Domain.Slots;

public class DaySlots {
    public DateOnly day;
    public List<Slot> slots;

    // Construct object from ToString
    public DaySlots(string s){
        string[] ds = s.Split("="); 
        /* Will result in:
            s[0] = date
            s[1] = list of slots
        */
        day = DateOnly.Parse(ds[0]);
        string[] slotStrings = ds[1].Split(";"); // The last string from this will be empty
        List<Slot> slots = [];
        for (int i = 0; i < slotStrings.Length-1; i++)
            slots.Add(new Slot(slotStrings[i]));
        this.slots = slots;
    }
    public DaySlots(DateOnly day, List<Slot> slots){
        this.day = day;
        this.slots = slots;
    }
    public override string ToString(){
        StringBuilder builder = new(day.ToString()+"=");
        foreach (Slot s in slots) builder.Append(s.ToString()+";");
        return builder.ToString();
    }
}