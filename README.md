# Todo.Api.Apps
.NET 8 Web API & MVC Project for Todo.Api.Apps dan Todo.Web.Apps

## Installation of .NET 8 SDK RUNTIMES
1. Download .NET 8 SDK and runtimes from the following link:
   ```
   https://dotnet.microsoft.com/en-us/download/dotnet/8.0
   ```

## Documentation To Learn
1. Read anything about Entity Framework Core at https://www.learnentityframeworkcore.com/
2. Read anything about AutoMapper at https://automapper.org/
3. Read anything about FluentValidation at https://fluentvalidation.net/

## Configuration
1. Open the "Todo.Api.Apps/appsettings.json" file and update the connection string.

## Data Seeding
1. Open Visual Studio
2. Go to Extensions > Manage Extensions
3. Search "Insert Guid" that created by Mads Kristensen
4. To use this extension use this shortcut
   ```
   Ctrl + K, Ctrl + Space
   ```
5. That will insert a GUID exactly in the point you are in the code

## Migration Database
1. Open Visual Studio
2. Set the "Todo.Api.Apps" as the startup project.
3. Open the Package Manager Console.
4. Run the following command to add new migration:
   ```
   Add-Migration -s Todo.Api.Apps -p Todo.Api.DataAccess -c ApplicationDbContext
   ```
5. Start "Todo.Api.Apps" project to create the database.

## Running the Project
1. Open the "Todo.Api.Apps.sln" solution file in Visual Studio 2022 or newer.
2. To run the this solution, select "Todo.Api.Apps" and choose to run with or without debugging.