FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /source
COPY . .

RUN dotnet restore "WebApiSecond.csproj"
RUN dotnet publish "WebApiSecond.csproj" -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 5020
EXPOSE 5021
ENV ASPNETCORE_URLS=http://+:5020
ENV ASPNETCORE_HTTP_PORT=https://+:5021

ENTRYPOINT [ "dotnet","WebApiSecond.dll" ]