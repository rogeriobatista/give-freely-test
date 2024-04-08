FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install -y locales-all

ENV LANG C.UTF-8
ENV LC_ALL C.UTF-8

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "EmployeeManagementSystem.Api/EmployeeManagementSystem.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EmployeeManagementSystem.Api/EmployeeManagementSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeManagementSystem.Api/EmployeeManagementSystem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeManagementSystem.Api.dll"]
