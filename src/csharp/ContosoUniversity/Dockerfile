# Setup: Base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ContosoUniversity/ContosoUniversity.csproj", "ContosoUniversity/"]
RUN dotnet restore "ContosoUniversity/ContosoUniversity.csproj"
COPY . .
WORKDIR "/src/ContosoUniversity"
RUN dotnet build "ContosoUniversity.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "ContosoUniversity.csproj" -c Release -o /app/publish

# Setup: Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContosoUniversity.dll"]

# Install Utilities
RUN apt-get update && \
    apt-get install -y wget
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN apt-get update && apt install curl && apt-get install -y \
    apt-transport-https \
    dotnet-sdk-3.1 \
    vim \
    jq     
RUN dotnet dev-certs https

# Setup: Rhino Development Certificate
RUN cd SSL \
    openssl verify -CAfile https.crt https.crt && \
    cp https.crt /usr/local/share/ca-certificates/ && \
    update-ca-certificates && \
    ls /etc/ssl/certs/https.pem && \
    openssl verify https.crt && \
    cat appsettings.json | jq  '. + { "Kestrel": { "Certificates": { "Default": { "Path": "/app/https.pfx", "Password": "" } } } }'


# Environment
ENV ASPNETCORE_URLS=http://+:80;https://+:443