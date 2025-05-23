# netcore-api-template
this is the example netcore API template

# How to clone and run

1. Clone this repository by running `git clone https://github.com/nursidik/netcore-api-template.git`
2. Open the project in Visual Studio 2022 or Visual Studio Code
3. Make sure you have .NET 9 SDK installed
4. Open terminal in the project directory and run `dotnet restore`
5. Run `dotnet run` to start the application
6. Open a web browser and navigate to `https://localhost:5214/swagger/` to see the Swagger UI

Note: 
- Make sure you have PostgreSQL installed and running on your machine
- You need to update the PostgreSQL connection string in `appsettings.json` and `appsettings.Development.json` with your own database credentials

# Running the Application using Docker

1. Ensure Docker is installed and running on your machine.
2. Navigate to the project directory and run `docker-compose -f docker/docker-compose-postgresql.yml up --build -d` to start the PostgreSQL container.
4. run command: `docker-compose -f docker/docker-compose.yml up --build` to start the application
6. Open a web browser and navigate to `http://localhost:5005/swagger/` to see the Swagger UI

Note: 
- The application will be exposed on port 5005. Make sure this port is available on your machine.
- The Docker setup assumes you have a PostgreSQL database running. Update the connection string in `appsettings.json` and `appsettings.Development.json` if necessary, change the host to `db` it is the name of postgres container.

