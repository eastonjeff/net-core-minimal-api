# net-core-minimal-api

Minimal API using Net 10, Entity Framework, Docker, pgAdmin & PostgreSQL. 

Follow the instructions in this file to set up your local environment for development & debugging. 

# File Structure

- Solution: ./
	- .env.template: used to create a local .env file for storing local (git ignored) secrets used in docker processes
	- docker-compose.yml: creates the network & images for back-end stack & local tooling
	- docker-compose.override.yml: used for running & building the project in DEBUG mode
- API Project: ./net-core-minimal-api
	- Program.cs: entry point for the net core API, dependency registration, endpoint registration, etc.
	- Dockerfile: defines the image & startup cmd
	- /Data folder: contains the dbContext & data models 
	- /Migrations folder: contains entity framework generated migrations & snapshots
	- /Services: repositories, DTO (data transfer object) models & endpoint validators

# Infrastructure & Tooling

The docker compose file is configured to run & launch the API, the Postgres database & pgAdmin. 

## Docker Installation

Install wsl via Powershell (Admin): wsl --install 

- (At time of this readme) Windows Subsystem for Linux 2.7.10
- Ubuntu 
- When prompted to create your default Unix user account, take note of the username & password you create. You will need these credentials to log in to your Linux environment.
- Close powershell, reopen it and run: wsl
- Confirm you are able to login to your Linux environment.

Install:Docker Desktop (I am using Version 4.82.0)

Launch Docker Desktop. Verify that the Docker Engine can start.

If not, follow these steps:

1. Open powershell and run: wsl --shutdown
2. Reopen Docker Desktop and go to settings > Resources > WSL Integration. Enable integration with your Linux distribution.

# Local Environment

## Create a Local SSL Certificate For Use In API Container

```
dotnet dev-certs https --trust
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx" -p "YourSecureCertPassword123"
```

## Update Local .env

Open the .env.template and follow the instructions to create a .env file. This file will be used to configure the database connection string and other environment variables when images are built & run. 

These environment variables are used in the docker-compose.yml file to set the database connection string, cert path and other settings.

# Entity Framework & Initial Migrations

IMPORTANT: Be sure to run your dotnet ef commands from the root of the project (not the solution)

## Install The DotNet EF Tool Globally

```
dotnet tool install --global dotnet-ef
```

## Creating The Database

Running Entity Framework migrations via the dotnet command line requires a valid connection string in appsettings.json or user secrets. 

We don't want connection strings hardcoded in appsettings.json, since usernames/passwords will leak into source control. Use user secrets to store your local overrides.

1. With your PostgreSQL DB & pgAdmin running in docker, open pgAdmin & register your server using the connection information from the docker compose / .env files.
	- NOTE: docker network db hostname is postgres-db which is what you should use when registering the server in pgAdmin (because pgAdmin is running in the docker network!)
2. Right click on the net-core-minimal-api project and select 'Manage User Secrets'
3. Copy the connection strings section from appsettings.json & replace secrets with the actual values (reference your .env file from above).
4. Use localhost for your db server name while running outside of the docker network
5. Update the database using the following command:

```
dotnet ef database update
```

6. Refresh the database in pgAdmin
7. Confirm the database has been created by logging into pgAdmin & exploring its schema

## Creating A New Migration

```
dotnet ef migrations add YourMigrationName
```

## Updating Your Database

```
dotnet ef database update
```

# Run API Locally (Docker Compose)

```
docker compose up -d
```

OR (if making changes & need to rebuild)

```
docker compose up --build -d
```

OR (if added a nuget package & need a cache refresh)

```
docker compose build --no-cache
docker compose up -d
```

This will pull the images for pgadmin, net10 runtime & postgres, build the project, create & launch the containers & use the docker compose override file to ensure the project is built using Debug configuration. 

NOTE: if wanting to do a release, simply force docker compose to run without the override, which will default to Release build configuration.

```
docker compose -f docker-compose.yml up -d --build
```

Open Docker Desktop & find the 'net-core-minimal-api' compose stack. It should have the 3 running containers below it. 

1. PgAdmin: http://localhost:8080
2. Local API: https://localhost:5001/scalar/
3. PostgresDb: http://localhost:5432

# Attaching A Debugger To The API

The API Dockerfile supports a runtime argument (BUILD_CONFIGURATION), which defaults to Release.

The docker-compose.override.yml file will override this argument to use the Debug build, which will enable clean debugging. 

When your local API container is running:

1. Go to Debug (Ctrl + Alt + P) -> Attach To Process
2. Select Docker (Linux Container)
3. Select net10_api Connection Target (Find)
4. Attach the dotnet process with Managed (.NET Core for Unix) code type

Breakpoints should now work.

To relaunch / attach the last debugger, use Shift + Alt + P
