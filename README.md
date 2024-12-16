# p5sem-3dj-g59

## 1. Description of the Project

Our project provides a **Web API** that lets you:
* As an Admin, manage users (staff and patients) and Operation Types.
* As a Patient, manage your profile.
* As a Doctor, manage Operations.

A **Frontend** module is provided to support these functionalities. It also provides support for other modules:
* **3D Visualization** module, where you can view the floor of the hospital, along with their state.
* **Planning** module. Here, you can prepare schedules for the various staff and operations.

We also have a **Business Continuity Plan** module, where we prepare important characteristics like *backup plans*, **RPO** and **WRT**, and a **GDPR** module, where we explain what personal details we'll need to have access to, what we're going to use them for and what rules and laws allow us to do this.

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
* *THREE.js*
* *Angular*
* *Tailwind CSS*

## 4. Architecture used

Project adopts:
* *DDD concepts*
* *Onion Architecture*
* *4+1 Architectural View Model*

## 5. How to run

Although the following scripts are available for you to run the various components of our project, they will be deployed in various machines. That way, you'll only have to access the **Frontend** by the clicking [this link](), which in turns leads to the other components.

### 5.1. Backend

If you're using **Windows**, you can use the following files:
* [**run.bat**](scripts/backend/run.bat) to run the **Backend** module.
* [**test.bat**](scripts/backend/test.bat) to run the tests created for the project's **Backend**.

If you're using **Linux**, you can use the following files:
* [**run.sh**](scripts/backend/run.sh) to run the **Backend** module.
* [**test.sh**](scripts/backend/test.sh) to run the tests created for the project's **Backend**.

### 5.2. Frontend

If you're using **Windows**, you can use the following files:
* [**run.bat**](scripts/frontend/run.bat) to run the **Frontend** module.
* [**test.bat**](scripts/frontend/test.bat) to run the tests created for the project's **Frontend**.
* [**test-e2e.bat**](scripts/frontend/test-e2e.bat) to run the end-to-end tests created for the project's **Frontend**.

If you're using **Linux**, you can use the following files:
* [**run.sh**](scripts/frontend/run.sh) to run the **Frontend** module.
* [**test.sh**](scripts/frontend/test.sh) to run the tests created for the project's **Frontend**.
* [**test-e2e.sh**](scripts/frontend/test-e2e.sh) to run the end-to-end tests created for the project's **Frontend**.

### 5.3. Planning

If you're using **Windows**, you can use the following file:
* [**run.bat**](scripts/planning/run.bat) to run the **Planning** module.

If you're using **Linux**, you can use the following file:
* [**run.sh**](scripts/planning/run.sh) to run the **Planning** module.

### 5.4. Express Backend

If you're using **Windows**, you can use the following file:
* [**run.bat**](scripts/express-backend/run.bat) to run the **Express Backend** module.

If you're using **Linux**, you can use the following file:
* [**run.sh**](scripts/express-backend/run.sh) to run the **Express Backend** module.