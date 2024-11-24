# US 6.3.3 - Obtain a good schedule in a decent run-time

As an **Admin**, I want to obtain a good schedule, not necessarily the better, in useful time to be adopted.

The system generates a "good" (non-optimal but efficient) schedule using heuristics or informed methods (e.g., greedy algorithms, rule-based scheduling).

The system prioritizes generating a schedule that is close to optimal but does so within a reasonable time frame (e.g., under 30 seconds).

The user must have a user interface to start the process (enter any additional parameters the planning algorithm needs, e.g., room number, date, which heuristic to use). The system will then generate the plan and show it to the user on the screen. It is acceptable that the UI blocks while waiting for the planning module response.

## 1. Context

This **US** is part of the **Planning module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The team must create 2 heuristics (since we have more than 3 members with ALGAV).
2. These heuristics must be able to generate a good schedule within a reasonable time frame.

### 2.2. Dependencies

While this can be developed with the given code in mind, it must later be adapted to adopt [6.3.1](../6-3-1/readme.md)'s changes.

### 2.3. Pre-Conditions

For this **US** to work, the database must contain **staff** and **surgery rooms** previously.

### 2.4. Open Questions

**Question 1:** Is there any specific heuristics or criteria you want us to be used to find a good enough solution? Like the priority of each operation or the time they take?

**Answer 1:** 
Consider the example of the Travel Salesman Problem (TSP) illustrated in the first ALGAV TP class to support the practical work. In that case the heurist is to visit the city not visited yet more close to the last city visited. The idea is that when the dimention of the problemnis high, and to generate all sequences to select the best is not feasible, we will find a way to generate one solution that isgood enough, but not the better.
In the case of surgeries it may be that the next operation is that involving the doctor that is available early, or the doctor with more surgeries not done yet. The heuristic will be good for some cases, not for others.
But in spite of involving just the doctor it may consider other staff for the surgery as well.
You can test the heuristic for cases with dimentions that are availsble for generating all solutions and compare the result using the heuristic and the better solution.

## 3. Analysis

Through the requisites and the open questions, the team can conclude that:
* The team must develop 2 heuristics that must, as a goal, be able to generate a "good enough" schedule within a max time frame of around 30 seconds.
* The results should then be documented and compared with the best possible solution in terms of final time, run time, and solution.

## 4. Design

The team decided that: 
* The two heuristics the team would develop would be the ones suggested in the official [TP Support ALGAV Sprint2 - Planning Surgeries - 4](https://moodle.isep.ipp.pt/pluginfile.php/424620/mod_resource/content/2/TP%20Support%20ALGAV%20Sprint2%20-%20Planning%20Surgeries%20-%204.pdf) (Page 6).
* The comparison mention in the **Analysis** section should be done with a table.

## 5. Implementation and Demonstration

These are specified in the [report](../6-3-1/report.pdf).