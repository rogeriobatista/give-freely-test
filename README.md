## Employee Management System

Employee Management System is a fullstack web application based on C# .Net and React

The api of this project is based on the layered architecture model, where each part is separated into projects, isolating responsibilities.

For the data layer, the repository pattern was implemented, abstracting the database, giving more flexibility for future changes.

The frontend follows the componentized web application model, enabling high reuse and easy implementation of the microfrontend standard.

## Database

### Prerequisites and Setup

You will need to install Docker and Docker Compose

[Download Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Creating the database container

The first step is to navigate to the “db” directory.

Now you need to execute the following command:

```
docker-compose up -d
```

## Api

### Prerequisites and Setup

You will need to install the dotnet 8 SDK and Cli

[Download .Net SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Run the application

The first step is to navigate to the “api/EmployeeManagementSystem.Api” directory.

```
dotnet run
```

## Web

### Prerequisites and Setup

The first step is to navigate to the “frontend” directory.

Setup is very simple. Just install the dependencies:

```bash
npm i
```

### Run the application

```
npm run dev
```

## Login

The application automatically creates credentials for administrator access

Username
```
admin@ems.com
```

Password
```
p@ssw0rd
```
