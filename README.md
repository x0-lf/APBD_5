# APBD_5 - Task 11

This project is a .NET Web API Project built with **Entity Framework Core (Code First)**.
It is created to help manage prescriptions for patients, allowing doctors to prescribe up to 10 medicaments per prescription.

The project follows SOLID principles, a clean architecture consisting of:
(`Controllers`, `Repositories`, `Services` & `DbContext`), and includes:

- Endpoints for adding prescriptions and retrieving patient data
- `Models/` used to create database schema & `Seeder` fills them with initial data
- Auto-seeding of Doctors, Patients, Medicaments, Prescriptions and Prescription_Medicament via `Seeder`
- At Least 11 positions of the `Medicament` in the `Seeder` to follow what the Task requires
- `DTOs` usage for data isolation for Requests and Responses
- `Business logic` enforced in the `Service Layer`
- Logging and transaction handling
- Ready to use `.http` requests for testing via Visual Studio's / JetBrains Rider built-in HTTP client or tools like Postman.

# Project Structure

1. `APBD_5` - Project
2. `APBD_5_Tests` - Unit Tests


# Running the Project

1. Clone the repo

```
git clone https://github.com/x0-lf/APBD_5.git
```

2. Restore dependencies

```
dotnet restore
```

3. Optional (if there's a db with the same name)

```
dotnet ef database drop --force
```

4. Apply migration

```
dotnet ef migrations add InitialCreate
```

5. Seed the base

```
dotnet ef database update
```

6. Run the project

```
dotnet run --project APBD_5
```

Note: by default debugging is turned on, this means that by running a Project you will see stuff like
```
Using launch settings from APBD_5\Properties\launchSettings.json...
Building...
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
```

# How to Turn on Debugging Queries (Helpful in the N+1 Analyse)

1. Go to the
```
cd APBD_5
```

2. Uncomment the line 16 in Program.cs
```
\\builder.Logging.AddConsole();
```

# Running Unit Tests

1. Go into `APBD_5_Tests` folder

```
cd APBD_5_Tests
```

2. Run the:
```
dotnet test
```

# API Endpoints

`POST /api/prescriptions` - Create a new prescription.
- Adds a patient if they don’t exist
- Validates:
    - Max 10 medicaments
    - DueDate >= Date
    - Medicaments must exist
- Returns `201 Created` or `400/404` with a JSON error

`GET /api/patients/{id}` - Retrieve patient details by ID including:
- Basic details
- All prescriptions (sorted by `DueDate`)
- Each prescription includes doctor and medicament info


# Testing the Endpoints

Similarly to the previous Project, there is also a [APBD_5.http](./APBD_5.http) file which let you test those endpoints in Jetbrains Rider or Visual Studio environment.

### Previous Projects

**[APBD_4 - Warehouse Management](https://github.com/x0-lf/APBD_4)**  

**[APBD_3 - REST API Travel Agency](https://github.com/x0-lf/APBD_3)**

**[APBD_2 - Simple 20 LINQ xUnit Tests](https://github.com/x0-lf/APBD_2)**

**[APBD_1 - Simple yet Advanced Logistics Manager Example](https://github.com/x0-lf/APBD_1)**
