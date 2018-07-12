# Build stage TODO: Needed?
FROM microsoft/aspnetcore-build:2 AS build

# set working directory
WORKDIR /app

# Restore
COPY Source/Interface/Interface.csproj ./Source/Interface/
RUN dotnet restore Source/Interface/Interface.csproj
COPY Source/Service/Service.csproj ./Source/Service/
RUN dotnet restore Source/Service/Service.csproj
COPY Source/Client/Client.csproj ./Source/Client/
RUN dotnet restore Source/Client/Client.csproj
COPY Source/Process/Process.csproj ./Source/Process/
RUN dotnet restore Source/Process/Process.csproj
COPY Tests/Service.Test/Service.Test.csproj ./Tests/Service.Test/
RUN dotnet restore Tests/Service.Test/Service.Test.csproj
COPY Tests/Client.Test/Client.Test.csproj ./Tests/Client.Test/
RUN dotnet restore Tests/Client.Test/Client.Test.csproj

# Copy Source
COPY . .

# Test
ENV TEAMCITY_PROJECT_NAME=fake
#RUN dotnet test Test/Service.Test/Service.Test.csproj

# Publish
RUN dotnet publish Source/Process/Process.csproj -o /obj

# Runtime stage
FROM microsoft/aspnetcore:2

# set working directory
WORKDIR /app

# Copy compiled binaries
COPY --from=build /obj ./bin

# Copy configuration
COPY config/*.* ./config/

# Set default environment variables
ENV HTTP_PORT = 8080 
ENV MONGO_SERVICE_URI ""
ENV MONGO_SERVICE_HOST mongo
ENV MONGO_SERVICE_PORT 27017
ENV MONGO_DB app

EXPOSE 8080

# moved command to docker-compose.yml
#CMD ["dotnet", "./bin/Process.dll", "-c", "./config/config.yml"]

