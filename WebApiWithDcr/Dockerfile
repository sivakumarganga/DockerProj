FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /source
COPY . .

RUN dotnet restore "WebApiWithDcr.csproj"
RUN dotnet publish  "WebApiWithDcr.csproj" -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as serve
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_HTTP_PORT=https://+:5001
ENV CustomEnvFromDocker=123456

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT [ "dotnet", "WebApiWithDcr.dll" ]

#docker build --rm -t SidDev/webapiwithdcr:latest .
# docker run --rm -p 5000:5000 -p 5001:5001 SidDev/webapiwithdcr:latest
