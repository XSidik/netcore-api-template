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
