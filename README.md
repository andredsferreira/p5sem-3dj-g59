# p5sem-3dj-g59

## 1. Description of the Project

Our project provides a **Web API** that lets you:
* As an Admin, manage users (staff and patients) and Operation Types.
* As a Patient, manage your profile.
* As a Doctor, manage Operations.

A **Frontend** module is not yet done, but is planned.

Developed by **Group 059** in order to meet the requirements for **LAPR5(67509)**.

## 2. Planning and Technical Documentation

[Planning and Technical Documentation](docs/readme.md)

## 3. Technologies used

Project uses:
* *ASP.NET Core 8.0 API*
* *Entity Framework*
* *MySql Database*
* *Google Identity and Access Management*
* *Google SMTP Server*

## 4. Architecture used

Project adopts:
* *DDD concepts*
* *Onion Architecture*
* *4+1 Architectural View Model*

## 5. How to run
### 5.1 Backend

If you're using **Windows**, you can use the following files:
* [**build-all.bat**](scripts/build-all.bat) to build the entire project.
* [**run-backend.bat**](scripts/run-backend.bat) to run the **Backend** module.
* [**test.bat**](scripts/test.bat) to run the tests created for the project.