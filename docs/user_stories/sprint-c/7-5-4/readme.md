# US 7.5.4 - Display/hide information about the selected room

As a **healthcare staff member**, whenever I select a different, I want the camera (and spotlight if applicable) to move smoothly instead of instantly. The animation can either be created or defined using some *API*, such as *tween.js*.

## 1. Context

This **US** is part of the **3D visualization module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. After picking a room, the camera and the spotlight should move smoothly instead of instantly.

### 2.2. Dependencies

This **US** depends on:
* [**US 7.5.1**](../7-5-1/readme.md).

### 2.3. Pre-Conditions

A **surgery room** must be selected in order for this functionality to be called, hence the dependency on [**US 7.5.1**](../7-5-1/readme.md).

### 2.4. Open Questions

No questions yet.

## 3. Analysis

From the requisites, the team can conclude that:
* If we select a room, the camera should move smoothly.
* **The team doesn't need to develop a spotlight, since we're a four-member group.**

## 4. Design

* A method called "**changeCameraPositionAndAngle**" would contain the logic for this functionality. This method will receive the camera's desired **position** and **target**.

## 5. Implementation

-

## 6. Demonstration

-