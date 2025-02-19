# US 6.4.6 - Implement a backup strategy

As **system administrator**, I want a backup strategy to be proposed, justified and implemented that minimizes RPO (Recovery Point Objective) and WRT (Work Recovery Time).

## 1. Context

This **US** is part of the **Business Continuity module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The system has a backup system.

### 2.2. Dependencies

None

### 2.3. Pre-Conditions

None

### 2.4. Open Questions

**Question 1:** A estratégia proposta pode ser apenas uma ou um misto de várias? No caso da implementação da estratégia proposta, se for um misto de várias, podemos apenas implementar uma delas?

**Answer 1:** A estratégia deve suportar a satisfação do MTD pretendida. Sendo um misto de várias - apesar de não entender o que quer dizer com isso - deverá implementá-la na sua plenitude.

## 3. Analysis

Through analysis of the requisites and open questions, we can conclude that the team must establish a backup strategy that's reliable, frequent and quick.

## 4. Design

The team decided that:
* The system would do a backup of our **MySql** database's state, as the source code is already secured in ***GitHub***.
* The system would do a complete backup in **sundays** and incremental backups in every other week day.
    * This strategy sets **RPO** at 24 hours and **WRT** at, in the worst-case scenario, 1 complete backup and 6 incremental backups.

The decisions made and their justification will be explained more in depth in its section on the [report](../6-4-1/report.pdf).    

## 5. C4 Views

-

## 6. Tests

No tests.

## 7. Implementation

The implementation and the code explanation will be explained more in depth in its section on the [report](report.pdf).  

## 8. Demonstration

We can see here how, at sunday (November 17th), the system did a full backup.

![](images/demonstration/full_backup_20241117.png)

However, the system did partial backups the other days (November 18th and 19th).

![](images/demonstration/incremental_backups_20241119.png)