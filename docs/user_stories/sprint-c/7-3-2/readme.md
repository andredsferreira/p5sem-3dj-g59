# US 7.3.2 - Schedule surgeries using Genetic Algorithms

As an Admin, I want to be able to schedule surgeries to several operation rooms using Genetic Algorithms (Genetic Algorithm parameters need to be tuned according to conditions like number of genes, desired time for solution, etc.)

## 1. Context

This **US** is part of the **Planning module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The user must be able to run a script that schedules surgeries to a room using Genetic Algorithms.
2. These Genetic Algorithms are tuned using conditions like number of genes, etc.

### 2.2. Dependencies

No dependencies

### 2.3. Pre-Conditions

* A **Surgery Room** must exist.
* **Operation Requests** must exist.
* **Operation Requests** must be assigned to a **Surgery Room** ([US 7.3.1](../7-3-1/readme.md))

### 2.4. Open Questions

No open questions yet.

## 3. Analysis

Through the requisites, the team concluded:
* The team must implement a prolog script that schedules various **operation requests** to one **surgery room**.
* The team must improve the given **Genetic Algorithm** code and adjust it to **Surgeries in Rooms** instead of **Tasks in Machines**.

## 4. Design

The team decided that:
* A new callable predicate will be added to our existing prolog scheduler.
* The new predicate will use the dynamic facts established by [**US 7.3.1.**](../7-3-1/readme.md).
* The best resulting schedule will be the output.

## 5. Implementation

-

## 6. Demonstration

-