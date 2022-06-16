#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Presentation/LogUltra.UI/LogUltra.UI.csproj", "Presentation/LogUltra.UI/"]
RUN dotnet restore "Presentation/LogUltra.UI/LogUltra.UI.csproj"
COPY . .
WORKDIR "/src/Presentation/LogUltra.UI"
RUN dotnet build "LogUltra.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogUltra.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogUltra.UI.dll"]