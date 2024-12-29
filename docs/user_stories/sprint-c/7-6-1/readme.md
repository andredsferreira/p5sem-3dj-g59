# US 7.6.1 - Download Medical History

As a Patient, I want to download my medical history in a portable and secure format, so that I can easily transfer it to another healthcare provider.

## 1. Context

This **US** is part of the **GDPR module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. Patients can request to download their medical history via their profile.

### 2.2. Dependencies

For this **US** to work, every type of entry must have a way to list them through the **medical record number** associated with them.

### 2.3. Pre-Conditions

A **Patient** (the actor) must exist.

### 2.4. Open Questions

**Question 1:** What format/s should be available? 
And what parts of the medical history should be used for this? all or only some specific

**Answer 1:** please use an easily machine-processable format like XML or JSON.
all medical, personal, information must be exported

**Question 2:** What is considered a secure format?

**Answer 2:** de forma a garantir a segurança e confidencialidade dos dados aquando do pedido de portabilidade, sugiro um formato que possibilite a aposição, por exemplo, de uma password. 

## 3. Analysis

Through the requisites, the team concluded:
* A **Patient** must have a way to request their medical record.
    * This **medical record** must contain its number and all of the entries associated with it.

Through the open questions, the team concluded:
* This **medical record** must come in a format like XML or JSON.
* For it to be secure, it should come with something like a **password**.

## 4. Design

-

## 5. Implementation

-

## 6. Demonstration

-