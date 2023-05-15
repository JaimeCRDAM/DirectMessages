#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DirectMessages/DirectMessages.csproj", "DirectMessages/"]
RUN dotnet restore "DirectMessages/DirectMessages.csproj"
COPY . .
WORKDIR "/src/DirectMessages"
RUN dotnet build "DirectMessages.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DirectMessages.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DirectMessages.dll"]