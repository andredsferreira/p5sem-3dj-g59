# US 6.3.1 - Obtain the best schedule of a set of operations

As an **Admin**, I want to obtain the better scheduling of a set of operations (surgeries) in a certain operation room in a specific day.

The better scheduling is considered as the sequence of operations that finishes early. Note that
surgeries have constraints (e.g. number of doctors or other staff), namely concerning the time
availability of staff during the day. The approach may be generating all surgeries’ sequences and
select the better, and this is possible till a certain dimension (number of surgeries).

The user must have a user interface to start the process (enter any additional parameters the
planning algorithm needs, e.g., room number, date). The system will then generate the plan and
show it to the user on the screen. It is acceptable that the UI blocks while waiting for the planning
module response.


## 1. Context

This **US** is part of the **Planning module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. The admin must be able to choose a list of operations to schedule.
2. The program must have a user interface.
3. The solution must be shown to the user.
4. It's acceptable if the UI blocks while waiting for the response.

### 2.2. Dependencies

No dependencies

### 2.3. Pre-Conditions

For this **US** to work, the database must contain **staff** and **surgery rooms** previously.

### 2.4. Open Questions

**Question 1:** Gostaríamos que nos esclarecesse se na US 6.3.1, se é esperado pelo programa sugestões de horários para as cirurgias ou se essas sugestões já devem ser guardadas na base de dados e serem scheduled.

**Answer 1:** O objetivo desse requisito é obter o escalonamento dos pedidos de cirurgia. ou seja, em que dia/hora/sala/etc cada cirurgia deve ser realizada.

**Question 2:** Quantas salas, no máximo, é que nos vão pedir para escalonar?

**Answer 2:** Neste *Sprint*, só é preciso 1. No próximo será pedido um máximo de mais ou menos 3.

## 3. Analysis

Through the requisites and the open questions, the team can conclude that:
* The team must create a program that receives a collection of staff, rooms and operations and schedules them, calculating all possibilities and returning the best one.
    * For now, we only need to develop a solution that accepts only **1 room (Question 2)**.
    * The best solution is the one that ends the soonest possible (if a solution had a final time of 900, and another had a final time of 1000, the first one would be considered better). **
        * However, the team must create another evaluation method that, instead of considering the final total time, considers the average staff exit time (if a solution had a final time of 1000, but an average staff exit time of 850, it would be considered better than another solution with a final time of 900 and an average staff exit time of 900) **(Appendix for teams with 5 members)**.
* The solution must be developed in **Swi Prolog**.

## 4. Design

The team decided that the following should be the process of events:
1. The **Planning** component should receive the information from the **UI**.
2. The **Planning** component should process the data and compute the best solution. While this is happening, the **UI** is blocked (in order to increase simplicity).
3. The **Planning** should then return the information to the **UI**.
4. The **UI** asks the user if they're satisfied. If they choose "Yes", the appointments are booked in the **Backend**.

The team also decided that the **UI** should give to the user the option to choose what criteria is used to define what is the "Best solution" (Final time vs Average staff exit time).

## 5. Implementation and Demonstration

These are specified in the [report](Report.pdf).