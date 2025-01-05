# US 7.3.1 - Assign surgeries to rooms

As an Admin, I want an automatic method to assign a set of operations (surgeries) to several operation rooms (assign is just to decide in whic operation room the surgery will be done)

## 1. Context

This **US** is part of the **Planning module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The user must be able to run a script that decides which surgeries will be in each room (Does not schedule).

### 2.2. Dependencies

No dependencies

### 2.3. Pre-Conditions

* A **Surgery Room** (or more) must exist.
* **Operation Requests** must exist.

### 2.4. Open Questions

**Question 1:** 

My question is, if we need to check if the surgery is possible to be done in a certain room(if theres an availabity slot big enougth for the time needed for the surgery).

**Answer 1:** 

You can check the relation between the sum of the operations times (preparation+surgery+cleaning) for a certain room and the sum of free times of the room. If you divide the first by the second (ratio) it is obvious that if the value is >1 it will be impossible.

But due to small time intervals tha are not occupied we sugest that the ratio has the value <0.8.

So, a check you can do during the assignment of operatios to rooms is to verify if the ratio is still in the limit.

## 3. Analysis

Through the requisites, the team concluded:
* The team must implement a prolog script that decides which surgeries will be in each room (Does not schedule).

Through the open questions, the team concluded:
* Each room should have a max occupiance rate of 80%.

## 4. Design

The team decided that:
* A new callable predicate will be added to our existing prolog scheduler.
* This predicate will add the room-surgeries combinations (Room **or1** will have surgeries **s01**, **so4** and **so5**) to dynamic facts, so these can be used by the other scheduling methods.

## 5. Implementation and Demonstration

These are specified in the [report](report.pdf).