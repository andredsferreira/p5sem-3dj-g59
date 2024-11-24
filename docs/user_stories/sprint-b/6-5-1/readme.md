# US 6.5.1 - Show a 3D Visualization of the Hospital Floor

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

Through the requisites and open questions, we can conclude that US is going to include information from the domain:
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
* The general logic is in the main component file, [hospitalfloor.component.ts](../../../../HospitalApp/src/app/hospitalfloor/hospitalfloor.component.ts).
* The structure construction logic in [loader.js](../../../../HospitalApp/src/app/hospitalfloor/jsfiles/loader.js).
* The JSON with the structures and models to be used is [config.json](../../../../HospitalApp/src/app/hospitalfloor/config.json).

## 5. Implementation

The structure for the entire hospital is built using a **map** in the **config.json** file:

```json
"map": [
    [3, 2, 2, 3, 2, 2, 2, 3, 2, 2, 1],
    [1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1],
    [10, 0, 0, 8, 0, 0, 0, 9, 0, 0, 11],
    [1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1],
    [1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1],
    [1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1],
    [1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1],
    [10, 0, 0, 8, 0, 0, 0, 9, 0, 0, 11],
    [1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1],
    [1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1],
    [2, 2, 2, 2, 6, 7, 2, 2, 2, 2, 0]
],
```

And this is its constructor:

```ts
interface LoaderParameters {
    map: number[][];
    groundTextureUrl: string;
    groundSize: Vector2;
    wallTextureUrl: string;
    wallSize: Vector3;
    door: Fbx;
}
```

And the structure for a room is also there:

```json
"roomMap": [
    [0,0,0,0,0],
    [10,0,4,9,0],
    [0,0,0,0,0],
    [0,0,0,0,0],
    [0,2,2,2,0]
]
```

And this is its constructor:

```ts
interface LoaderParameters {
    roomNumber: number;
    leftSide: boolean;
    map: number[][];
    roomSize: Vector2;
    wallTextureUrl: string;
    wallSize: Vector3;
    table: ObjMtl;
    tableWithPerson: ObjMtl;
    windowSize: Vector3;
    table: ObjMtl;
    tableWithPerson: ObjMtl;
    door: Fbx;
}
```

(The meaning behind each number can be found [here](../../../../HospitalApp/src/app/hospitalfloor/map_values.xlsx)).

In order to get all rooms from the **Backend** API:

**hospitalfloor.component.ts** (on method **ngOnInit**):

```ts
const response = await this.service.getRooms(this.token);
this.rooms = response.body;
```

**hospitalfloor-service.ts**:

```ts
  async getRooms(token: string | null) : Promise<HttpResponse<RoomType[]>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    const rooms = await lastValueFrom(
        this.http.get<RoomType[]>(`${this.apiPath}/SurgeryRoom/All`,{ headers, observe: 'response' })
      );
    return rooms;
  }
```

And in order to, for every room, check if it is occupied on a certain instant:

**hospitalfloor.component.ts**:

```ts
  handleDateTimeSelection(): void {
    const date = new Date(this.selectedDateTime);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hour = String(date.getHours()).padStart(2, '0');
    const minute = String(date.getMinutes()).padStart(2, '0');
    const time = `${year}${month}${day}${hour}${minute}`;
    this.roomLoaders!.forEach(async element => {
      element.toggleTableVisibility((await this.service.isRoomOccupied(this.token, element.roomNumber, time)).body);
    });
  }
```

**hospitalfloor-service.ts**:

```ts
  async isRoomOccupied(token: string | null, RoomNumber: number, date: string) : Promise<HttpResponse<boolean>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    const rooms = await lastValueFrom(
        this.http.get<boolean>(`${this.apiPath}/SurgeryRoom/Occupied/${RoomNumber}/${date}`,{ headers, observe: 'response' })
      );
    return rooms;
  }
```

**roomloader.js**:

```js
toggleTableVisibility(isOccupied) {
    console.log("Is room " + this.description.roomNumber + " occupied? " + isOccupied);
    this.object.traverse((child) => {
        if (child.tableWithPersonObject) {
            child.tableWithPersonObject.visible = isOccupied;
        }
        if (child.name === 'table') {
            child.visible = !isOccupied;
        }
    });
}
```

## 6. Demonstration

Here, you can see the structure with 2 rooms that, when a date/time is defined, load a human model if it's occupied during that instant.

[demonstracao](images/demonstration/demonstration.mp4)