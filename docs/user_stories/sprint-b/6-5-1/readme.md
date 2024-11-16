# US 6.5.1 - Edit a Patient Profile

As a **healthcare staff member**, I want to see a 3D representation of the hospital/clinic floor. Its description shouls be imported from a **JSON (JavaScript Object Notation)** formatted file. The floor must consist of several surgical table. Each room must be enclosed by walls and include a door and a surgical table. There should be no representation of the ceiling. If a room is being used at any given time, a 3D model of a human body should be lying on the table. Models can either be created or imported.

## 1. Context

This **US** is part of the **3D visualization module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. Object definition must be imported from a **JSON** file.
2. Each object should have a model attached, either a basic THREE.js one or a imported one.
3. The floor must consist of several surgical rooms.
    * Each room must be enclosed by walls and include a door and a surgical table.
    * If a room is being used at any given time, a 3D model of a human body should be lying on the table.

### 2.2. Dependencies

This **US** depends on:
* [**US 6.1.3**](../6-1-3/readme.md), since this functionality depends on connection between the **3D Visualization Module** and the **Planning Module** to determine if a room is occupied.

### 2.3. Pre-Conditions

None

### 2.4. Open Questions

1. How does the JSON look like(example), and how it affects the visualization of the room.
    
    * It's up to you to decide how the JSON file looks like. As an example, I suggest the contents of file "Loquitas.json" in project "Basic Thumb Raiser".

2. When should the human body be lying on the table? if at this precise moment there is a surgery going on? or if there are surgeries going to happen that day?

    * If and only if there is a surgery going on on that precise room at that precise moment.

3. For how many rooms exactly should we do the 3d representation?

    * The number of operating rooms in the 3D Visualization Module must be consistent with the number of rooms that can be scheduled in the Planning/Optimization Module.

4. Let's say that, while we have the 3D representation open, someone creates a new Surgery Room. Must that new room be loaded dynamically?

    * it is not necessary to do it "hot wired".
the next time the 3D view is loaded it should pickup the new surgery room.
keep in mind that creating a new surgery room is not something that happens frequently...

## 3. Analysis

Throught the requisites and open questions, we can conclude that US is going to include information from the domain:
* **Surgery Rooms**, in order to display them.
* **Appointments**, in order to display if a **Surgery Room** is currently occupied, and link the user to the respective **Appointment** if that's the case.

The client owner also stated that a maximum of **3 rooms** will be scheduled at one given time.

## 4. Design

The team decided that:
* The information about **Surgery Rooms** and **Appointments** must come from the **Backend**, as it's not viable to run the **Planning** module everytime someone opens the **3D visualization** module.
* Regarding **3D Models**:
    * [This](../../../../HospitalApp/public/models/hospital-door/source/README.md) should be the model for the door.
    * [This](../../../../HospitalApp/public/models/SurgeryTableWithPerson/README.md) should be the model for the **Surgery Table** with a **Person** on it, representing an occupied **Room**. It is a team-made combination of:
        * A [**Surgery Table** model](../../../../HospitalApp/public/models/SurgeryTable/README.md), which was also team made and appears in the **3D visualization**, representing a **Room** that is not occupied.
        * A free-to-use [**Human** model](../../../../HospitalApp/public/models/Human/README.md).        

## 5. C4 Views

-

## 6. Tests

-

## 7. Implementation

-

## 8. Demonstration

-