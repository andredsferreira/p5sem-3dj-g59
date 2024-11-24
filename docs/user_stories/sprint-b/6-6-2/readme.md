# US 6.6.1 - Prepare a data breach detection and treatment plan

As a **System**, I want to notify both users and the responsible authority in case of a data breach, so that I comply with GDPR's breach notification requirements.

## 1. Context

This **US** is part of the **GDPR module**.

## 2. Requirements

### 2.1. Acceptance Criteria

* The system automatically detects potential data breaches and immediately notifies both the affected users and the relevant GDPR authority.
* Breach notifications to users include:
    * Details of the breach (e.g., what data was compromised).
    * Steps being taken to mitigate the breach.
    * Recommendations for users (e.g., changing passwords, monitoring for suspicious activity).
* Notifications to the GDPR authority include detailed logs of the breach and actions taken.
* Breach notifications are sent within the legally required timeframe (e.g., 72 hours).
* The system logs all breach notifications and subsequent actions taken for auditing and compliance purposes.

### 2.2. Dependencies

No dependencies.

### 2.3. Pre-Conditions

No pre-conditions.

### 2.4. Open Questions

**Question 1:** When it comes to US6.6.2, are we supposed to functionally simulate dataleak detection on our projects or just document what would happen during a potential data breach?

**Answer 1:** You'll only have to explain during the pitch what would happen if a data breach was identified according to the US 6.6.2., that's all. 

**You don't have to operationalize the solution, just explain it in the pitch.**

## 3. Analysis

Through the requisites, the team can conclude that:
* The team must explain, in the same video from [6.6.1](../6-6-1/readme.md), what the team and system would do in case of a data breach.

Through the open questions, the team can conclude that:
* The solution doesn't need to be actually implemented in the system, the team only needs to explain what would theoretically happen.

## 4. Design

The team decided that: 
* This section of the script should be divided by the following **steps**:
    * How the system would detect a data breach.
    * How the system would notify the affected party and the GDPR authority.
    * How the team would document the findings and use them to better prepare themselves for any future incidents.

## 5. Implementation and Demonstration

This can be viewed in the final video:

![](../6-6-1/report.mp4)