#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Calculator.Api/Calculator.Api.csproj", "Calculator.Api/"]
COPY ["Calculator.DataAccess/Calculator.DataAccess.csproj", "Calculator.DataAccess/"]
COPY ["Calculator.Domain/Calculator.Domain.csproj", "Calculator.Domain/"]
COPY ["Calculator.Sdk/Calculator.Sdk.csproj", "Calculator.Sdk/"]
RUN dotnet restore "Calculator.Api/Calculator.Api.csproj"
COPY . .
WORKDIR "/src/Calculator.Api"
RUN dotnet build "Calculator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calculator.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calculator.Api.dll"]