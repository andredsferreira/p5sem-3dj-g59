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
* [**build-all.bat**](scripts/build-all.bat) to build the **Backend** module.
* [**run-backend.bat**](scripts/run-backend.bat) to run the **Backend** module.
* [**test-backend.bat**](scripts/test-backend.bat) to run the tests created for the project's **Backend**.

If you're using **Linux**, you can use the following files:
* [**build-all.sh**](scripts/build-all.sh) to build the **Backend** module.
* [**run-backend.sh**](scripts/run-backend.sh) to run the **Backend** module.
* [**test-backend.sh**](scripts/test-backend.sh) to run the tests created for the project's **Backend**.

### 5.2. Frontend

If you're using **Windows**, you can use the following files:
* [**run-frontend.bat**](scripts/run-frontend.bat) to run the **Frontend** module.
* [**test-frontend.bat**](scripts/test-frontend.bat) to run the tests created for the project's **Frontend**.

If you're using **Linux**, you can use the following files:
* [**run-frontend.sh**](scripts/run-frontend.sh) to run the **Frontend** module.
* [**test-frontend.sh**](scripts/test-frontend.sh) to run the tests created for the project's **Frontend**.

### 5.3. Planning

If you're using **Windows**, you can use the following file:
* [**run-planning.bat**](scripts/run-planning.bat) to run the **Planning** module.

If you're using **Linux**, you can use the following file:
* [**run-planning.sh**](scripts/run-planning.sh) to run the **Planning** module.
