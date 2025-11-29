# Employees API

A .NET 8 ASP.NET Core Web API for managing employee records with JWT authentication, Entity Framework Core, and SQL Server integration.


## Technology Stack

- **.NET 8** - Latest .NET framework
- **ASP.NET Core Web API** - REST API framework
- **Entity Framework Core 8** - ORM for data access
- **SQL Server LocalDB** - Local database
- **JWT Bearer Authentication** - Token-based security
- **Swagger** - Testing API
- **Postman** - Also for testing API
- **C#** - Programming language

### NuGet Packages I installed before hand

```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.AspNetCore.Authentication.JwtBearer
Swashbuckle.AspNetCore
```

## Project Structure

```
EmployeesApi/
├── Controllers/ 
│   ├── AuthController.cs          
│   └── EmployeesController.cs     
├── Models/
│   └── Employee.cs             
├── Services/
│   ├── IEmployeeService.cs        
│   └── EmployeeService.cs         
├── DTOs/
│   ├── CreateEmployeeDto.cs       
│   ├── UpdateEmployeeDto.cs       
│   └── GetEmployeeDto.cs          
├── Data/
│   └── EmployeeDbContext.cs       
├── Migrations/                    
├── appsettings.json               
├── Program.cs                     
├── EmployeesApi.csproj            
└── README.md                      
```

## IDE
- **Visual Studio Code**

## Database
- **SQL Server**

## Version Control
- **Github**


## Steps to run the project

### Step 1: Clone the Repository

```bash
git clone [(https://github.com/MelisaBunjaku19/Employees-API.git)]
cd EmployeesApi
```

### Step 2: Install NuGet Packages

```bash
dotnet restore
```

```bash
# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

# JWT Authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

# Swagger/OpenAPI
dotnet add package Swashbuckle.AspNetCore
```


### Step 3: Build the Project

```bash
dotnet build
```

## Step 4: Database Setup

### Create Initial Migration

```bash
dotnet ef migrations add InitialCreate
```

This creates a migration file in the `Migrations/` folder.

### Step 3: Apply Migration to Database

```bash
dotnet ef database update
````

## How to run the project

### From Command Line

Navigate to the project folder:

```bash
cd c:\Users\ab\EmployeeTask\EmployeesApi
```

Run the project:

```bash
dotnet run
```

## API Documentation

### Base URL
```
https://localhost:5101/api
```

### Authentication Endpoints

#### POST `/auth/login`
Authenticate and receive JWT token.

### Employee Endpoints

All employee endpoints require JWT authentication(the moment you authorize)

#### GET `/employees`
Returns all employees with calculated age.

#### GET `/employees/{id}`
Returns a single employee by ID.

#### POST `/employees`
Create a new employee.


#### PUT `/employees/{id}`
Update an existing employee.

#### DELETE `/employees/{id}`
Delete an employee by ID.
