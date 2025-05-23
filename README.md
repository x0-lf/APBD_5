# APBD_5 - Task 11

This project is a .NET 9 Web API Project built with **Entity Framework Core (Code First)**.
It manages prescriptions for patients, allowing doctors to prescribe up to 10 medicaments per prescription.

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




# Running the Project

`dotnet ef database drop`

`dotnet ef migrations add InitialCreate`

`dotnet ef database update`   

by default debugging is turned on.