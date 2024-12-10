# US 7.5.1 - Select a room

As a **healthcare staff member**, I want to select a room by left clicking on the corresponding surgical table (object picking). The camera should instantly move horizontally so that it targets the room center point.

## 1. Context

This **US** is part of the **3D visualization module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. By left clicking a room's surgical table, the camera should move horizontally.

### 2.2. Dependencies

This **US** depends on:
* [**US 6.5.1**](../../sprint-b/6-5-1/readme.md), since this functionality depends on the **3D Visualization Module**.
* [**US 6.5.4**](../../sprint-b/6-5-4/readme.md). Explained in **Design** section.

### 2.3. Pre-Conditions

The **Hospital Floors** must have at least one room. That way, we can select it.

### 2.4. Open Questions

No open questions yet.

## 3. Analysis

From the requisites, the team can conclude that:
* The **left click** must be used to pick an object.
* This functionality activates when the chosen object is a room's table (hence the dependency on [**US 6.5.1**](../../sprint-b/6-5-1/readme.md))
* There are no **Domain Concepts** in play here.

## 4. Design

The team decided that:
* A method called "**onMouseDown**" would contain the logic for this functionality.
    * This method would be triggered with an ***eventListener*** connected to our mouse.
* When the camera moves, the focus point of its controls (like the **Orbit**) should also be the room's surgical table, hence the dependency on [**US 6.5.4**](../../sprint-b/6-5-4/readme.md).

## 5. Implementation

-

## 6. Demonstration

-