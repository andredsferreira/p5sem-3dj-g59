# US 7.4.12 - Validate backup

As **system administrator**, we need to ensure that backups have been carried out correctly if necessary. To do this, we must automate their recovery, validating that the system is working at the end (e.g. database - execute a SQL query successfully after recovery)

## 1. Context

This **US** is part of the **Business Continuity module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The system restores the database's state.
2. The system validates its state.

### 2.2. Dependencies

* This **US** depends on [**US 7.4.3**](../7-4-3/readme.md).

### 2.3. Pre-Conditions

A backup must exist in order to be validated, hence the dependency on [**US 7.4.3**](../7-4-3/readme.md).

### 2.4. Open Questions

**Question 1:** Should the recovery validation process be scheduled, on-demand, or both?

**Answer 1:** Recovery should only be performed when needed. As so, on-demand is likely to be the best approach.

## 3. Analysis

Through analysis of the requisites, the team can conclude that:
* The system must have a way to restore and validate a backup.
Through the open question, the team can conclude that:
* This should be on-demand.

## 4. Design

The team decided that:
* This should be done to **Mongo**'s backups, hence the dependency on [**US 7.4.3**](../7-4-3/readme.md).
* As this should be on-demand, a script will be developed for this.

## 5. Implementation

The explanation will be explained more in depth in its section on the [report](../7-4-3/report.pdf).  
