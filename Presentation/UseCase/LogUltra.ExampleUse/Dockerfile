#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Presentation/UseCase/LogUltra.ExampleUse/LogUltra.ExampleUse.csproj", "Presentation/UseCase/LogUltra.ExampleUse/"]

COPY ["Providers/LogUltra.MongoDb/LogUltra.MongoDb.csproj", "Providers/LogUltra.MongoDb/"]
COPY ["Core/LogUltra.Models/LogUltra.Models.csproj", "Core/LogUltra.Models/"]
COPY ["Utility/Format/LogUltra.TemplateParser/LogUltra.TemplateParser.csproj", "Utility/Format/LogUltra.TemplateParser/"]
COPY ["Core/LogUltra.Core.Abstraction/LogUltra.Core.Abstraction.csproj", "Core/LogUltra.Core.Abstraction/"]
COPY ["Providers/LogUltra.Console/LogUltra.Console.csproj", "Providers/LogUltra.Console/"]
COPY ["Providers/LogUltra.File/LogUltra.File.csproj", "Providers/LogUltra.File/"]
RUN dotnet restore "Presentation/UseCase/LogUltra.ExampleUse/LogUltra.ExampleUse.csproj"
COPY . .
WORKDIR "/src/Presentation/UseCase/LogUltra.ExampleUse"
RUN dotnet build "LogUltra.ExampleUse.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogUltra.ExampleUse.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Presentation/UseCase/LogUltra.ExampleUse/LogUltra/", "/app/LogUltra/"]
ENTRYPOINT ["dotnet", "LogUltra.ExampleUse.dll"]