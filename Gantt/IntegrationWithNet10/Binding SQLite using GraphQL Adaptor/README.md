# Blazor Gantt Chart with SQLite and GraphQL Adaptor

## Project Overview

This repository demonstrates a production-ready pattern for binding **GraphQL** data to **Syncfusion Blazor Gantt Chart** using **Hot Chocolate GraphQL Server**. The sample application provides complete CRUD (Create, Read, Update, Delete) operations, filtering, sorting, searching and hierarchical parent/child support. The implementation follows industry best practices using GraphQL queries, mutations, resolvers, and a GraphQL adaptor for seamless Gantt Chart functionality.

## Key Features

- **Hot Chocolate GraphQL Server Integration**: Query resolvers for read operations and mutation resolvers for write operations
- **Syncfusion Blazor Gantt**:  Built-in editing, inserting, deleting, filtering, sorting, and parent/child hierarchy (via ParentID).
- **Complete CRUD Operations**: Add, edit, delete, and batch update of records directly from the Gantt Chart
- **GraphQL Adaptor**: Full control over Gantt Chart data operations (read, search, filter, sort) via GraphQL queries and mutations
- **DataManagerRequestInput**: Structured input type for handling complex data operations from the Gantt Chart
- **Configurable GraphQL Endpoint**: Backend port and GraphQL endpoint managed via `launchSettings.json`

## Prerequisites

| Component | Version | Purpose |
|-----------|---------|---------|
| Visual Studio 2026 | 18.1.0 or later | Development IDE with Blazor workload |
| .NET SDK | net10.0 or compatible | Runtime and build tools |
| SQLite | Latest stable version | Core framework for database operations |
|Microsoft.EntityFrameworkCore.Sqlite| 10.0.3 or latest|Provides SQLite database provider support for Entity Framework Core.|
| HotChocolate.AspNetCore | 2.1.66 or latest | Lightweight data mapper |
| Syncfusion.Blazor.Gantt | 32.1.25 or Latest | Gantt and UI components |
| Syncfusion.Blazor.Themes | 32.1.25 or Latest | Styling for Gantt components |
| Syncfusion.Blazor.Data | Latest | Data adaptors including GraphQL support |

## Quick Start

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd "Binding SQLite using GraphQL Adaptor"
   cd "Blazor Web app/GanttGraphQL"
   ```

2. **Create the database and table**
   
   Open DB Browser for SQLite and Create Database GanttDB in the desired location
   ```sql
   
   CREATE TABLE IF NOT EXISTS TaskData (
    TaskID       INTEGER PRIMARY KEY,
    TaskName     TEXT NOT NULL,
    StartDate    DATETIME,     
    EndDate      DATETIME,     
    Duration     TEXT,
    Progress     INTEGER,
    Predecessor  TEXT,         
    ParentID     INTEGER
   );
   ```

3. **Update the connection string**

   SQLite uses a **file‑based database**, so to connect to the database, simply point to the location of the .db file.
   The key **GanttDb** refers to the database name used by the application and it can be renamed as needed.
   
   Open `appsettings.json` and configure the SQLite connection:
   ```json
   {
     "ConnectionStrings": {
         "GanttDb": "Data Source=D:\\GanttGraphQL\\GanttDB.db"
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
   
   Navigate to the local URL displayed in the terminal (typically `https://localhost:xxxx`).
   
7. **Access GraphQL Playground (Optional)**
   
   Navigate to `https://localhost:70xx/graphql` (Replace `70xx` with the port number shown in the **launchSettings.json**) to explore the GraphQL schema and test queries/mutations manually.

## Configuration

### GraphQL Endpoint Port Configuration

The GraphQL endpoint port is configured in `Properties/launchSettings.json`. This file controls where the application runs and where the GraphQL endpoint is exposed.

**Instructions to Change the Port:**

1. Open `Properties/launchSettings.json` in the project root.
2. Locate the `https` profile section:

```json
"https": {
  "commandName": "Project",
  "dotnetRunMessages": true,
  "launchBrowser": true,
  "applicationUrl": "https://localhost:7000;http://localhost:5167",
  "environmentVariables": {
    "ASPNETCORE_ENVIRONMENT": "Development"
  }
}
```

3. Modify the `applicationUrl` property to change port numbers:
   - `https://localhost:7000` - HTTPS port for GraphQL endpoint
   - `http://localhost:5167` - HTTP port for GraphQL endpoint

4. Update the Gantt Chart connection URL in `Components/Pages/Home.razor` to match the configured port:
   ```html
   <SfDataManager Url="https://localhost:7000/graphql" GraphQLAdaptorOptions="@adaptorOptions" Adaptor="Adaptors.GraphQLAdaptor"></SfDataManager>
   ```

**Important Notes:**
- Port numbers must be between 1024 and 65535
- Avoid using ports already in use by other applications
- The GraphQL endpoint is always accessible at `/graphql` path (configured via `app.MapGraphQL()` in Program.cs)

### GraphQL Adaptor Configuration

The `GraphQLAdaptorOptions` in `Home.razor` defines how the Gantt Chart communicates with the GraphQL backend. This configuration specifies:

- **Query**: GraphQL query for reading task records with data operations support
- **ResolverName**: The backend resolver method name — for the Gantt Chart sample this refers to TaskData (or whatever you named your GraphQL query resolver that returns the task list)
- **Mutation.Insert**: GraphQL mutation for creating new tasks
- **Mutation.Update**: GraphQL mutation for updating existing tasks
- **Mutation.Delete**: GraphQL mutation for deleting tasks
- **Mutation.Batch**: GraphQL mutation for batch operations

## Project Layout

| File/Folder | Purpose |
|-------------|---------|
| `/Models/TaskDataModel.cs` | Data model representing Task Data |
| `/Models/GraphQLQuery.cs` | Query resolvers for retrieving task data (e.g., GetTaskData) |
| `/Models/GraphQLMutation.cs` | Mutation resolvers for creating, updating, deleting, and batch-updating tasks |
| `/Models/DataManagerRequestInput.cs` | Input type for data operation parameters (filter, sort, search) |
| `/Components/Pages/Home.razor` | Gantt Chart page with GraphQL adaptor configuration and task management UI |
| `/Program.cs` | Service registration for Hot Chocolate GraphQL server |
| `/Properties/launchSettings.json` | Port configuration for GraphQL endpoint |

## Common Tasks

### Add a Task
1. Click the **Add** button in the toolbar
2. Fill in the Dialog fields
3. Click **Save** to persist the record to the database

### Edit a Task
1. Select a task row in the Gantt Chart
2. Click the **Edit** button in the toolbar
3. Modify the required fields
4. Click **Save** to save changes

### Delete a Task
1. Select a task row in the Gantt Chart
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

### GraphQL Endpoint Not Responding

- Verify the application is running: Check terminal for `Now listening on: https://localhost:70xx` (Replace `70xx` with the port number shown in the **launchSettings.json**)
- Confirm the URL in `SfDataManager` matches the running port
- Ensure `app.MapGraphQL()` is configured in `Program.cs`
- Check firewall settings if accessing from a different machine

### Static Files Not Loading (CSS/Scripts)

- Verify Syncfusion stylesheet is referenced in `Components/App.razor`:
  ```html
  <link href="_content/Syncfusion.Blazor.Themes/tailwind3.css" rel="stylesheet" />
  ```
- Verify Syncfusion scripts are referenced in `Components/App.razor`:
  ```html
  <script src="_content/Syncfusion.Blazor.Core/scripts/syncfusion-blazor.min.js" type="text/javascript"></script>
  ```
- Check browser developer tools (F12) for 404 errors on static resources

### CRUD Operations Not Working

- Verify the GraphQL endpoint URL in `Home.razor` SfDataManager matches the backend port
- Check browser console for GraphQL errors (Network tab → GraphQL requests)
- Ensure all GraphQL mutation resolvers are implemented in `GraphQLMutation.cs`
- Verify the `DataManagerRequestInput` class includes all required properties for data operations

### Version Conflicts

- Align HotChocolate.AspNetCore and Syncfusion package versions
- Run `dotnet restore` to update NuGet packages
- Check the `GanttGraphQLAdaptor.csproj` file for conflicting version constraints
- Verify all packages are compatible with .NET 10.0 or later

### Connection Error
- Verify that the database file exists at the path specified in the connection string.
- Ensure the .db file is not locked or opened exclusively by another program.
- Confirm that the application has read/write permissions to the folder where the database file is stored.
- If you changed the database location or filename, make sure the Data Source= path is updated accordingly in **appsettings.json**.

### Port Already in Use

- Change the port numbers in `launchSettings.json` to available ports
- Or identify the process using the port: `Get-Process | Where-Object {$_.Handles -gt 500} | Select-Object Name, Id`
- Update the Gantt Chart connection URL to match the new port