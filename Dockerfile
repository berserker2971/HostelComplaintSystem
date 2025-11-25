FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HostelComplaintSystem.csproj", "./"]
RUN dotnet restore "HostelComplaintSystem.csproj"
COPY . .
RUN dotnet publish "HostelComplaintSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HostelComplaintSystem.dll"]
