#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["LogUltra.UI/LogUltra.UI.csproj", "LogUltra.UI/"]
COPY ["LogUltra.Core/LogUltra.Core.csproj", "LogUltra.Core/"]
COPY ["LogUltra.Log.Service/LogUltra.Log.Service.csproj", "LogUltra.Log.Service/"]
COPY ["LogUltra.Models/LogUltra.Models.csproj", "LogUltra.Models/"]
COPY ["LogUltra.MongoDb/LogUltra.MongoDb.csproj", "LogUltra.MongoDb/"]
COPY ["LogUltra.TemplateParser/LogUltra.TemplateParser.csproj", "LogUltra.TemplateParser/"]
COPY ["LogUltra.Console/LogUltra.Console.csproj", "LogUltra.Console/"]
COPY ["LogUltra.File/LogUltra.File.csproj", "LogUltra.File/"]
RUN dotnet restore "LogUltra.UI/LogUltra.UI.csproj"
COPY . .
WORKDIR "/src/LogUltra.UI"
RUN dotnet build "LogUltra.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogUltra.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogUltra.UI.dll"]