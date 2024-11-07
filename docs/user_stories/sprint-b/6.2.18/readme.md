# US 6.2.18 - Create Operation Type

As an Admin, I want to add new types of operations, so that I can reflect on the available medical procedures in the system. (UI for US 5.1.20)

## 1. Context

As an Admin, I want to add new types of operations, so that I can reflect on the available medical procedures in the system. (UI for US 5.1.20)

## 2. Requirements

### 2.1. Acceptance Criteria

Admins can add new operation types with attributes like: Operation Name, Required Staff by Specialization, Estimated Duration

The system validates that the operation name is unique.

The system logs the creation of new operation types and makes them available for scheduling immediately.

### 2.2. Dependencies

This US depends on:

* [US 5.1.20](../../sprint-a/us20/readme.md) since this functionality calls the Web API request to create Operation Types.

### 2.3. Pre-Conditions

This US has no pre-conditions.

### 2.4. Open Questions

This US has no open questions.

## 3. Analysis

This US is the Frontend version of US 5.1.20, which contains the logic. Thus, this section does not apply here.

## 4. Design

The team decided the following aspects:

* The user should be able to input the Operation Name, Required Staff by Specialization, and Estimated Duration.
* The system should validate that the Operation Name is unique.
* After creating the Operation Type, the system should log the creation and make it available for scheduling immediately.

## 5. C4 Views
