# net-core-minimal-api

Minimal API using Net 10, Docker, pgAdmin & PostgreSQL

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

## Environment

## Create an SSL Certificate For Use In API Container

1. dotnet dev-certs https --trust
2. dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx" -p "YourSecureCertPassword123"

## Update Local .env

Open the .env.template and follow the instructions to create a .env file. This file will be used to configure the database connection string and other environment variables. 

These environment variables are used in the docker-compose.yml file to configure the database connection string and other settings.

# Run Locally (Docker Compose)

docker compose up -d

OR (if making changes)

docker compose up --build -d

This will pull the images for pgadmin, net10 runtime & postgres.

Open Docker Desktop & find the 'net-core-minimal-api' compose stack. It should have the 3 running containers below it. 

1. PgAdmin: http://localhost:8080
2. Local API: https://localhost:5001/scalar/
3. PostgresDb: http://localhost:5432