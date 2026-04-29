# build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BankApi/BankApi.csproj", "BankApi/"]
RUN dotnet restore "BankApi/BankApi.csproj"

COPY . .
WORKDIR "/src/BankApi"
RUN dotnet publish -c Release -o /app/publish

# runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BankApi.dll"]
