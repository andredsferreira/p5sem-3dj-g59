# US 7.4.3 - Create Database Backups

As **system administrator**, I want to make a backup copy of the DB(s) to a Cloud environment using a script that renames it to the format <ddb_name>_yyyymmdd where <db_name> is the name of the database, yyyy is the year the copy was made, mm is the month the copy was made and dd is the day the copy was made.

## 1. Context

This **US** is part of the **Business Continuity module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The system creates a database backup to the cloud.
2. This backup follows the established naming convention.

### 2.2. Dependencies

None

### 2.3. Pre-Conditions

The database has to exist.

### 2.4. Open Questions

No questions

## 3. Analysis

Through analysis of the requisites, we can conclude that the team needs to implement a strategy that creates database backups and puts them in a cloud environment.

## 4. Design

The team decided that:
* The system would do a backup of our **Mongo** database's state, as the **MySQL** database's state is already saved in our system.

The decisions made and their justification will be explained more in depth in its section on the [report](report.pdf).    

## 5. Implementation

The implementation and the code explanation will be explained more in depth in its section on the [report](report.pdf).  
