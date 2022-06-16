#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/LogUltra.Log.API/LogUltra.Log.API.csproj", "Presentation/LogUltra.Log.API/"]
COPY ["Utility/Database/LogUltra.Log.Service/LogUltra.Log.Service.csproj", "Utility/Database/LogUltra.Log.Service/"]
COPY ["Providers/LogUltra.MongoDb/LogUltra.MongoDb.csproj", "Providers/LogUltra.MongoDb/"]
COPY ["Core/LogUltra.Models/LogUltra.Models.csproj", "Core/LogUltra.Models/"]
COPY ["Utility/Format/LogUltra.TemplateParser/LogUltra.TemplateParser.csproj", "Utility/Format/LogUltra.TemplateParser/"]
COPY ["Core/LogUltra.Core.Abstraction/LogUltra.Core.Abstraction.csproj", "Core/LogUltra.Core.Abstraction/"]
RUN dotnet restore "Presentation/LogUltra.Log.API/LogUltra.Log.API.csproj"
COPY . .
WORKDIR "/src/Presentation/LogUltra.Log.API"
RUN dotnet build "LogUltra.Log.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogUltra.Log.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogUltra.Log.API.dll"]