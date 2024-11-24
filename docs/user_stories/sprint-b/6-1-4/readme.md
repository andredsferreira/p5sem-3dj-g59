# US 6.1.4 - Sync the information used by the Planning with the Backend module

As **Admin** I want the information about healthcare staff, operation types, and operation requests used in the planning module is in sync with the information entered in the backoffice module.

## 1. Context

This **US** is one of the syncing USs of the **Frontend** module.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The information used by the planning module (healthcare staff, operation types and operation requests) to be, somehow, in sync with the information entered in the backoffice module.

### 2.2. Dependencies

No dependencies

### 2.3. Pre-Conditions

None

### 2.4. Open Questions

No open questions yet.

## 3. Analysis

As specified by the **Acceptance Criteria**, the team should make it so, somehow, the information shown used by the planning module (USs 6.3.x) should be in sync with the information entered in the backoffice module (USs 5.1.x).

## 4. Design

The team decided that:
* The information used by the Planning module will come from the Frontend module, which in turn gets it from the Backend module, in order to simplify the Planning module's responsabilities.

## 5. Demonstration

*Not applicable*
