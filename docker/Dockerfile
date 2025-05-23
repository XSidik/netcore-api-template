# Use the .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY netcore-api-template/. .

# Restore
RUN dotnet restore netcore-api-template.csproj

# Publish
RUN dotnet publish ./netcore-api-template.csproj -c Release -o /app/out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "netcore-api-template.dll"]
