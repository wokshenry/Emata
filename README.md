# EMATA Aptitude Test App

## Introduction

This is an aptitude test app, which is used to generate different aptitude test questions for each new test sessions started. It also has a module where you can view and resume any session that was incomplete and its session time is still valid.

This application doesnt not have access controll and the test are not tagged to any user, It is for demonstration purposes only.

The app is built using Blazor WebAssembly for the front end and .Net Core for the back end logic. Data is persisted using PostgreSQL database using Entity Framework Core.

## Project Structure
The project is setup with the following projects:

### Emata.UI
The User interface of the project that is built with Blazor Web Assembly.  

### Emata.API
The API of the project that is built with ASP.NET Core. Works with PostgreSQL database using Entity Framework Core.

### Emata.Shared
This project contains shared code that is used by both the client and API projects. It contains shared models, constants, and other shared code.

## Running the app
To run the app, you need to have the following installed:
- .NET 9 SDK
- PostgreSQL


To run the app, follow these steps:
1. Clone the repository
2. Open the solution in Visual Studio or Visual Studio Code
3. Update the connection string in the `appsettings.json` file in the `Emata.API` project to point to your PostgreSQL database.
4. Run migrations to create the database schema by running the following command in the `Emata.API` project:
	```
	dotnet ef database update
	```
5. Run the app by running the following command in the `CEmata.API project:
	```
	dotnet run
	```																	
	If you are using Visual Studio, you can alternatively run the app by pressing `F5` or click the Run button.

# Dependencies

## Postgresql database
This application uses a PostgreSQL database for data storage. To connect to the database, you need to configure the connection string in the app settings. The connection string should be provided in the "ConnectionString" key of the app settings file.
Example connection string: 
	```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=5432;Database=mydatabase;User Id=myuser;Password=mypassword;"
    }
    ```
	
Make sure to replace the placeholder values with the actual values for your PostgreSQL database. 



