###

# Banking app

## User stories ( As a User)
- As a user I want to be able to log in with my username and pin and view my accounts information
- As a user I want to be able to view my past transaction history
- As a user I want to be able to withdraw money out of an account and deposit money into an account
- As a user I want to be able to add or remove users' accounts that are apart of my “family”
- As a user I want to be able to change my profile information (username, password)



- Ned - (Front-end)

- Nick - (Back-end)

- Harold - (Front-end)

- Robert - (Back-end)

## ERD - Tables
### Users 
| UserId | Userpin | RealPersonName |
| --- | --- | --- |

### Accounts
| AccountID | Balance | AccountType | PrimaryUser |
| --- | --- | --- | --- |

### AccountUsers

| AccountID | User |
| --- | --- |


### Transactions 
| TransactionID | AccountID | Amount | Date |
| --- | --- | --- | --- |


# Project Description

Our project is a comprehensive full-stack banking application designed to offer robust financial management capabilities for users. The backend is developed using .NET and SQL Server, employing unit and integration testing, as well as DevOps GitHub testing, while adhering to the Repository design pattern. The frontend, built with the Next.js React framework, integrates seamlessly with the backend to deliver a user-friendly interface. This application features core functionalities including withdrawing and depositing funds, viewing transaction history, updating account information, and managing family accounts. Designed with useState and useContext, and enhanced with basic CSS for styling, the platform aims to simplify banking tasks and provide an intuitive user experience. 

## Getting started

## Setting up the frontend

## Prerequsites 
- To run the frontend server you will need `node` and `npm`

### Setting up frontend server

To set up the frontend you will need to ensure `node` and `npm` are installed on your computer. You can find instructions to install them [here](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm)

Once `node` and `npm` are installed, navigate to the directory `Bank-FrontEnd` and run the command

```npm install```

This will install any dependencies. Then run

```npm run dev```

This will start frontend server.


## Setting up the backend

## Prerequsites 
- To run the backend server you will need `.Net 8.0`
- To run the SQL server you will need a docker container running `Microsoft SQL Server 2022`
- To generate and view API documentation you will need `docfx`. Or you can view the API documentation on github [here](https://240708-net.github.io/P2-Team1/api/BankBackend.Controllers.html)

## Instructions

### Setting up SQL Server

To get started, you will need a server running Microsoft SQL Server 2022. You can setup a server with docker. You can find the instructions to install docker [here](https://docs.docker.com/engine/install/).

Once docker is installed, do the following

- run the command ` docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest `

- Replace `yourStrong(!)Password` with your password. 

This will start the docker container containing our SQL server image.

### Setting up connection string

You will also need to provide a connection string to connect to the SQL server. To do that

- Create an `appsettings.json` file under the `BankBackend/src` directory. 
- Fill the contents of `appsettings.json` with :

    ```json
    {
    "Logging": {
        "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
        }
    },
    "ConnectionStrings": {
        "BankDB": "Server=localhost;Database=YourDatabaseName;User=sa;Password=YourPassword;TrustServerCertificate=true;"
    },
    "AllowedHosts": "*"
    }
    ```

- Rplace `YourPassword` with your password, and `YourDatabaseName` with the database you wish to create in your SQL server. Other than these two your connecting string should look exactly like the one provided above.
- You can also add or modify any additional settings you wish. 

### Initializing database

The first time you run the backend server, you will need to initialize the database. You should do this with .NET Migrations

- Navigate to the directory `BankBackend/src` 
- run the command `dotnet ef migrations add Initial` where `Initial` can be replaced with a name of your choosing
- run the command `dotnet ef database update`

If any errors occur, make sure your docker image is running and check your connection string. 

### Building and testing the program

You can build and test the program by navigating to the directory `BankBackend` and running the command `dotnet build` to build the project and `dotnet test` to run test cases.

### Running the server

You can run the server by navigating to the directory `BankBackend` and running the command `dotnet run --project src/BankBackend.csproj`.

Your server should now be running on the indicated port on localhost. 

You can visit the SwaggerUI endpoint API documentation by visiting the `/swagger` endpoint.


## Additional Features

You can view the backend API documentation if you have `docfx` installed. `docfx` is a tool included with .NET to generate API documentation.

Install `docfx` with the command 

```dotnet tool update -g docfx```

Then navigate to the `BankBackend` directory and run the command 

```docfx docfx.json --serve```

You can view the docs and the indicated url.
