Q-and-A
=======

Navodila za uprabo aplikacije
---------------------------

###  1. Database
The used database for this project was a MySql database. The scripts for setting up the database is in the install folder, along with EntityFramework migration scripts. You can use a different database, but you have to reconfigure the DatabaseContect options builder to use your database type.

### 2. API
API is written in C# anlog with the .NET framework ASP.Net Core. To build the application you have to install the `dotnet` CLI. 
In the API\src\API folder:
* run `dotnet restore`
* run the build command for the desired platform    
`dotnet build -r win10-x64`
`dotnet build -r osx.10.10-x64`
` dotnet build -r ubuntu.14.04-x64`

The database connection string resides in  `questionoverflowContext.cs`. Just change the connectionString to the desired database, user and password. 
```cs
public string connectionString = @"Server=localhost;User Id=root;Password=admin;Database=questionoverflow";
```
To change the database type just reconfigure the `optionsBuilder` to use the desired database.
(Note: you may have to modify dependencies if switching to different db type)
```cs
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString);
    }
```

### 3. TESTS
Tests are written in NodeJs with Mocha and Supertest. Run `npm install` inside teh ApiTest folder to install dependencies and `npm test` to run tests.