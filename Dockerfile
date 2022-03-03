FROM --platform=linux/x86-64 mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["EventLogger.csproj", "./"]
RUN dotnet restore "EventLogger.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EventLogger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventLogger.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventLogger.dll"]
