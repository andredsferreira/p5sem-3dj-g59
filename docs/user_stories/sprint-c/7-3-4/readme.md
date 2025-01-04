# US 7.3.4 - Select Schedule System Automatically

As an Admin, I want the schedule system selects in an adequate way the method (generate all & select better; heuristic; Genetic Algorithm) to use for scheduling operations for each operation room according to the dimension of the problem and useful time to generate the solution.

## 1. Context

This **US** is part of the **Planning module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The user must have a way to, according to the number of surgeries to be scheduled and time available for scheduling, get suggested a way to schedule them.

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
* The team must implement a prolog script that analyzes the values and picks a scheduling method.

## 4. Design

The team decided that:
* We need to develop a method that will receive a room and a time (in seconds). After that, through the number of surgeries assigned to that room (done by [US 7.3.1](../7-3-1/readme.md)), it will calculate the most appropriate method to be used (generate all & select better; heuristic; Genetic Algorithm) and return that suggestion to the user.

## 5. Implementation and Demonstration

These are specified in the [report](Report.pdf).