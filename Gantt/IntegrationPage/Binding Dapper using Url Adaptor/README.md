# Blazor Gantt with MS SQL and Dapper

## Project Overview

This repository demonstrates a straightforward pattern for binding  **MS SQL Server** database to **Syncfusion Blazor Gantt** using **[Dapper](https://www.nuget.org/packages/Dapper)** and the UrlAdaptor from SfDataManager. The sample provides full CRUD (Create, Read, Update, Delete) functionality, filtering, sorting, and hierarchical parent/child support.

## Key Features

- **MS SQL**: Reliable relational database for production environments and LocalDB support for development.
- **Dapper**: Lightweight, high-performance micro‑ORM for parameterized SQL and fast data mapping.
- **Syncfusion Blazor Gantt**:  Built-in editing, inserting, deleting, filtering, sorting, and parent/child hierarchy (via ParentId).
- **UrlAdaptor**: API communication for all CRUD actions.

## Prerequisites

| Component | Version | Purpose |
|-----------|---------|---------|
| Visual Studio 2026 | 18.1.0 or later | Development IDE with Blazor workload |
| .NET SDK | net10.0 or compatible | Runtime and build tools |
| [SQL Server Management Studio](https://learn.microsoft.com/en-us/ssms/install/install?redirectedfrom=MSDN) | Latest | Core framework for database operations |
| [Dapper](https://www.nuget.org/packages/Dapper) | 2.1.66 or latest | Lightweight data mapper |
| [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient) | 6.1.4 or Latest | SQL Server client driver |
| [Syncfusion.Blazor.Gantt](https://www.nuget.org/packages/Syncfusion.Blazor.Gantt) | 32.1.25 or Latest | Gantt and UI components |
| [Syncfusion.Blazor.Themes](https://www.nuget.org/packages/Syncfusion.Blazor.Themes/) | 32.1.25 or Latest | Styling for Gantt components |

## Quick Start

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd "Binding Dapper using UrlAdaptor"
   cd "Blazor Web app/Gantt_Dapper"
   ```

2. **Create the database and table**
   
   Open MS SQL and run:
   ```sql
   CREATE DATABASE ganttdb;
   USE ganttdb;

   CREATE TABLE task_data (
    TaskId INT IDENTITY(1,1) PRIMARY KEY,
    TaskName NVARCHAR(50) NOT NULL,
    StartDate DATETIME DEFAULT GETDATE(),
    EndDate DATETIME NULL,
    ParentId INT NULL,
    Duration INT NOT NULL,
    Progress INT NOT NULL
   );
   ```

3. **Update the connection string**
   
   Replace connected database's connectionstring in GanttController.cs file in Controllers folder of Gantt_Dapper.   

4. **Restore packages and build**
   ```bash
   dotnet restore
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Open the application**
   
   Navigate to the local URL displayed in the terminal (typically `https://localhost:7xxx`).


## Project Layout

| File/Folder | Purpose |
|-------------|---------|
| `Controllers/GanttController.cs` | API controller with Dapper queries for CRUD |
| `/Components/Pages/Home.razor` | Gantt page with UrlAdaptor implementation |
| `/Program.cs` | Service registration and Syncfusion configuration |


## Common Tasks

### Add a Task
1. Click the **Add** button in the toolbar
2. Fill in the Dialog fields
3. Click **Save** to persist the record to the database

### Edit a Task
1. Select a row in the gantt
2. Click the **Edit** button in the toolbar
3. Modify the required fields
4. Click **Save** to save changes

### Delete a Task
1. Select a row in the gantt
2. Click the **Delete** button in the toolbar
3. Confirm the deletion in the dialog

### Search Records
1. Use the **Search** box in the toolbar
2. Enter keywords to filter records (searches across all columns)

### Filter Records
1. Click the filter icon in any column header
2. Select filter criteria (equals, contains, greater than, etc.)
3. Click **Filter** to apply

### Sort Records
1. Click the column header to sort ascending
2. Click again to sort descending

## Troubleshooting

### Connection Error
- Verify SQL Server is running and reachable.
- Test connection in SSMS with same credentials.
- If using LocalDB, ensure (LocalDB)\MSSQLLocalDB is installed and running.

### Missing Tables
- Verify the SQL script was executed successfully
- Check that migrations were applied (if using EF migrations)
- Run the database creation script again

### Static Files Not Loading
- Verify Syncfusion stylesheet and script references are present in `App.razor`
- Check browser developer tools for 404 errors on static resources

### Version Conflicts
- Align Entity Framework Core, Pomelo, and Syncfusion package versions
- Run `dotnet restore` to update NuGet packages
- Check the `.csproj` file for conflicting version constraints
