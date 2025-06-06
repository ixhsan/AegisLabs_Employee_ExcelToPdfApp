# 🧾 AegisLabs - Employee Report

## 🚀 Overview

This project was developed as part of the technical assessment for the **Junior .NET Developer** position at **AegisLabs**.

The application allows users to:

- Manage employee records (CRD operations)
- Persist and retrieve data using **MS SQL Server** (Stored Procedures + CTE)
- Export data to **PDF** and **Excel**
- Follow the **Repository Pattern** architecture using **ADO.NET** (without ORM)

---

## 🏗️ Technologies Used

| Category        | Technology / Tool                    |
|-----------------|--------------------------------------|
| Backend         | ASP.NET Core 8 (Razor Components)    |
| Frontend        | Razor Components + Bootstrap         |
| Database        | SQL Server                           |
| Data Access     | ADO.NET, Stored Procedures, CTE      |
| PDF Export      | Rotativa.AspNetCore                  |
| Excel Export    | EPPlus                               |
| Architecture    | Repository Pattern                   |

---

## ✨ Features

- ✅ Add, delete employee records
- ✅ Persist data using stored procedures in SQL Server
- ✅ Retrieve data using CTE inside stored procedures
- ✅ Generate downloadable Excel reports (.xlsx)
- ✅ Generate downloadable PDF reports (.pdf) using Rotativa
- ✅ Clean architecture (controller → service → repository)


## ⚙️ Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/aegislabs-employee-report.git
cd aegislabs-employee-report
```

### 2. Set Up the Database

* Create a new database named: `EmployeeDb`.
* Run the SQL script provided to create:

  * `Employees` table
  * Stored Procedures:

    * `sp_GetAllEmployees` (uses CTE)
    * `sp_AddEmployee`
    * `sp_DeleteEmployee`

**Note:** The data retrieval procedure utilizes a CTE (Common Table Expression) to simulate more advanced query logic.

### 3. Configure the Database Connection

Edit the `appsettings.json` file to include your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EmployeeDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> Replace `localhost` with your actual SQL Server name if needed.
> Replace `EmployeeDb` with your actual Database name.

### 4. Restore dependencies and run the Application

```bash
dotnet restore
dotnet build
dotnet run
```

Once running, open your browser and navigate to:

```
https://localhost:5001/employee-form
```

---

### 5. Generate PDF and Excel Reports

* Add employee records via the form.
* Click on **"Download Excel"** or **"Download PDF"** to generate and download reports.
* Reports are generated using:

  * `EPPlus` for Excel
  * `Rotativa.AspNetCore` for PDF (based on Razor Views)

---

### 6. Example: Stored Procedure with CTE

```sql
CREATE PROCEDURE sp_GetAllEmployees
AS
BEGIN
    WITH EmployeeCTE AS (
        SELECT
            ROW_NUMBER() OVER (ORDER BY Name) AS RowNum,
            Name, Email, Phone, Address
        FROM Employees
    )
    SELECT * FROM EmployeeCTE;
END
```

