# Balpreet Kaur Portfolio

Full-stack portfolio built from the resume PDF using React, HTML, CSS, .NET 8, EF Core, and SQL Server.

## What Is Included

- React portfolio UI with resume sections, projects, skills, certifications, education, and contact form.
- .NET 8 Web API with EF Core and SQL Server.
- Seeded resume data loaded into SQL Server on first API run.
- SQL script and commands to create the local database.
- API fallback in the frontend, so the UI still opens while the backend is being started.

## Prerequisites

- .NET 8 SDK
- Node.js and npm
- SQL Server Express running as `.\SQLEXPRESS`
- SQLCMD command line tool

This machine already has SQL Server Express running, so the commands below target `.\SQLEXPRESS`.

## 1. Create The Local Database

From this folder:

```powershell
sqlcmd -S .\SQLEXPRESS -E -C -i .\database\create-database.sql
```

If you see `CREATE DATABASE permission denied in database 'master'`, run this command from an elevated terminal or from a SQL login that has `sysadmin` permissions:

```powershell
sqlcmd -S .\SQLEXPRESS -E -C -v PortfolioUser="persistent\balpreet_kaur1" -i .\database\create-database-as-admin.sql
```

If your Windows login is different, replace `persistent\balpreet_kaur1` with the value from:

```powershell
whoami
```

You can verify the database exists:

```powershell
sqlcmd -S .\SQLEXPRESS -E -C -Q "SELECT name FROM sys.databases WHERE name = 'BalpreetPortfolioDb';"
```

The API uses EF Core `EnsureCreated` to create the tables and seed the resume data the first time it starts.

## 2. Run The .NET 8 API

```powershell
dotnet restore .\Portfolio.Api\Portfolio.Api.csproj
dotnet run --launch-profile http --project .\Portfolio.Api\Portfolio.Api.csproj
```

API URLs:

- API: `http://localhost:5042`
- Swagger: `http://localhost:5042/swagger`
- Portfolio data: `http://localhost:5042/api/portfolio`

## 3. Run The React App

Open a second PowerShell window from this folder:

```powershell
cd .\ClientApp
npm install
Copy-Item .env.example .env
npm run dev
```

Open:

```text
http://localhost:5173
```

## SQL Server Connection String

The API connection string is in:

```text
Portfolio.Api\appsettings.Development.json
```

Default value:

```text
Server=.\SQLEXPRESS;Database=BalpreetPortfolioDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true
```

If your SQL Server instance has a different name, update `Server=.\SQLEXPRESS`.

## Useful Commands

Check SQL Server Express:

```powershell
Get-Service MSSQL`$SQLEXPRESS
sqlcmd -S .\SQLEXPRESS -E -C -Q "SELECT @@SERVERNAME;"
```

Recreate the database from scratch:

```powershell
sqlcmd -S .\SQLEXPRESS -E -C -Q "ALTER DATABASE [BalpreetPortfolioDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [BalpreetPortfolioDb];"
sqlcmd -S .\SQLEXPRESS -E -C -i .\database\create-database.sql
dotnet run --launch-profile http --project .\Portfolio.Api\Portfolio.Api.csproj
```

Install EF tooling for future schema changes:

```powershell
dotnet tool install --global dotnet-ef --version 8.0.0
dotnet ef migrations add AddYourChange --project .\Portfolio.Api\Portfolio.Api.csproj
dotnet ef database update --project .\Portfolio.Api\Portfolio.Api.csproj
```

The current project does not require migrations to run because EF creates the initial schema automatically.
