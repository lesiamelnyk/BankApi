FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BankApi/BankApi.csproj", "BankApi/"]
RUN dotnet restore "BankApi/BankApi.csproj"

COPY . .
WORKDIR "/src/BankApi"

RUN dotnet build "BankApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "BankApi.dll"]
