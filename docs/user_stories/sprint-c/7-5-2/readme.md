# US 7.5.2 - Display/hide information about the selected room

As a **healthcare staff member**, whenever I press the "i" key, I want to display/hide an overlay containing updated information about the selected room.

## 1. Context

This **US** is part of the **3D visualization module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. After picking a room, the key "i" must be used to display (or hide, if already displayed) information about the selected room.

### 2.2. Dependencies

This **US** depends on:
* [**US 7.5.1**](../7-5-1/readme.md).

### 2.3. Pre-Conditions

A **surgery room** must be selected in order for this functionality to be called, hence the dependency on [**US 7.5.1**](../7-5-1/readme.md).

### 2.4. Open Questions

**Question 1:** Would it be okay, since we have a side menu with the controls and info, if the **room information** was displayed there instead of being an overlay?

**Answer 1:** Yes (confirmed by our **LAPR5** and **SGRAI** teachers).

## 3. Analysis

From the requisites, the team can conclude that:
* If we haven't selected a room, the "i" key doesn't do anything.
* If we have selected a room and the information is not yet displayed, the "i" key should display **information about the room**:
    * Room number.
    * Capacity.
    * Room type.
    * Maintenance slots.
    * Available equipment.
* If we have selected a room and the information is already displayed, the "i" key should hide it.
* **The presented information should be up to date.**

## 4. Design

The team decided that:
* As mentioned in the **Open Questions**, the information will be displayed in the side bar:
    * When no room is selected, the option is not there.
    * When a room is selected, there will be an option telling the user that they can **press the "i" key** to show more information.
        * There will be a button to unselect the room (this will also be possible to do via the "x" key). This will also reset the camera to a neutral state.
    * When the information about a room is displayed and the pick another one, it is automatically hidden.

## 5. Implementation

* **hospitalfloor.component.ts**

```ts
  async ngOnInit(): Promise<void> {
    ...
    document.addEventListener("keypress", this.iPressed.bind(this));
    ...
  }
```

```ts
  iPressed(event: KeyboardEvent) {
    console.log(event.key);
    if(event.key === 'x') this.closeRoomInfo();
    if(!this.selectedRoom || event.key !== 'i') return;
    this.showRoomInfo = !this.showRoomInfo;
  }
```

This information is obtained when we get all rooms from the backend. It is obtained through this object...

```ts
export interface Occupied {
    RoomNumber: number,
    Begin?: Date,
    End?: Date,
    Status: number,
    PatientName?: string,
    PatientMRN?: string,
}
```

...and displayed like this in **roomloader.js**:

```js
    toggleTableVisibility(isOccupied) {
        if(isOccupied.Status == 1){
            this.object.traverse((child) => {
                if (child.tableWithPersonObject) {
                    child.tableWithPersonObject.visible = true;
                }
                if (child.name === 'table') {
                    child.visible = false;
                }
            });
            this.Begin = isOccupied.Begin;
            this.End = isOccupied.End;
            this.Status = "Ocupada";
            this.PatientName = isOccupied.PatientName;
            this.PatientMRN = isOccupied.PatientMRN;
            return;
        }

        this.object.traverse((child) => {
            if (child.tableWithPersonObject) {
                child.tableWithPersonObject.visible = false;
            }
            if (child.name === 'table') {
                child.visible = true;
            }
        });
        this.Begin = null;
        this.End = null;
        this.PatientName = null;
        this.PatientMRN = null;
        if(isOccupied.Status == 0) this.Status = "Disponível";
        else this.Status = "Em Manutenção";
    }
```

## 6. Demonstration

![](../7-5-1/images/demonstration/demonstration.gif)