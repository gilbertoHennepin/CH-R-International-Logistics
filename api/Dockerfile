# Stage 1: build with the full .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:11.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish CountryRoute.Api/CountryRoute.Api.csproj -c Release -o /app/publish

# Stage 2: run with only the small runtime image
FROM mcr.microsoft.com/dotnet/aspnet:11.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "CountryRoute.Api.dll"]