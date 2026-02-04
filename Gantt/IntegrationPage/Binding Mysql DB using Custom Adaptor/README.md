# Blazor Gantt with MySQL and Entity Framework Core

## Project Overview

This repository demonstrates a production-ready pattern for binding **MySQL Server** data to **Syncfusion Blazor Gantt** using **Entity Framework Core (EF Core)**. The sample application provides complete CRUD (Create, Read, Update, Delete) operations, filtering, sorting, and batch updates. The implementation follows industry best practices using models, DbContext, repository pattern, and a custom adaptor for seamless functionality.

## Key Features

- **MySQL–Entity Framework Core Integration**: Models, DbContext, and Entity Framework Core migrations for database operations
- **Syncfusion Blazor Gantt**: Built-in search, filter, sorting capabilities
- **Complete CRUD Operations**: Add, edit, delete, and batch update records directly from the Gantt
- **Repository Pattern**: Clean separation of concerns with dependency injection support
- **CustomAdaptor**: Full control over data operations (read, search, filter, sort)
- **Configurable Connection String**: Database credentials managed via `appsettings.json`

## Prerequisites

| Component | Version | Purpose |
|-----------|---------|---------|
| Visual Studio 2022 | 17.0 or later | Development IDE with Blazor workload |
| .NET SDK | net8.0 or compatible | Runtime and build tools |
| MySQL Server | 8.0.45 or later | Database server |
| Microsoft.EntityFrameworkCore | 9.0.0 or later | Core framework for database operations |
| Microsoft.EntityFrameworkCore.Tools | 9.0.0 or later | Tools for managing database migrations |
| Pomelo.EntityFrameworkCore.MySql | 9.0.0 or later | MySQL provider for Entity Framework Core |
| Syncfusion.Blazor.Gantt | Latest | Gantt and UI components |
| Syncfusion.Blazor.Themes | Latest | Styling for Gantt components |

## Quick Start

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd "Binding MySQL database using CustomAdaptor"
   cd "Blazor Web app/Gantt_MySQL"
   ```

2. **Create the database and table**
   
   Open MySQL Workbench or any MySQL client and run:
   ```sql
   CREATE DATABASE IF NOT EXISTS ganttdb;
   USE ganttdb;

   CREATE TABLE IF NOT EXISTS task_data (
    TaskId INT AUTO_INCREMENT PRIMARY KEY,
    TaskName VARCHAR(50) NOT NULL, 
    StartDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    EndDate DATETIME,
    ParentId INT NULL,
    Duration VARCHAR(10) NOT NULL,   
    Progress DECIMAL(18, 2) NOT NULL    
   );
   ```

3. **Update the connection string**
   
   Open `appsettings.json` and configure the MySQL connection:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3306;Database=ganttdb;Uid=root;Pwd=<password>;SslMode=None;ConvertZeroDateTime=true;"
     },
     "AllowedHosts": "*"
   }
   ```

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

## Configuration

### Connection String

The connection string in `appsettings.json` contains the following components:

| Component | Description | Example |
|-----------|-------------|---------|
| Server | MySQL server address | `localhost` |
| Port | MySQL port number | `3306` |
| Database | Database name | `ganttdb` |
| Uid | MySQL username | `root` |
| Pwd | MySQL password | `<secure-password>` |
| SslMode | SSL encryption mode | `None` (for local development) |

**Security Note**: For production environments, store sensitive credentials using:
- User secrets for development
- Environment variables for production
- Azure Key Vault or similar secure storage solutions

## Project Layout

| File/Folder | Purpose |
|-------------|---------|
| `/Data/TaskDataModel.cs` | Entity model representing the transactions table |
| `/Data/TaskDbContext.cs` | Entity Framework Core DbContext for database operations |
| `/Data/TaskRepository.cs` | Repository class providing CRUD methods |
| `/Components/Pages/Home.razor` | DataGrid page with CustomAdaptor implementation |
| `/Program.cs` | Service registration and Syncfusion configuration |
| `/appsettings.json` | Application configuration including connection string |

## Common Tasks

### Add a Transaction
1. Click the **Add** button in the toolbar
2. Fill in the Dialog fields
3. Click **Save** to persist the record to the database

### Edit a Transaction
1. Select a row in the gantt
2. Click the **Edit** button in the toolbar
3. Modify the required fields
4. Click **Update** to save changes

### Delete a Transaction
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
- Verify MySQL Server is running on the specified host and port
- Confirm the database name, username, and password are correct
- Ensure the `ganttdb` database exists

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
