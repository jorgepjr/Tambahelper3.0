FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
RUN apt-get update && apt-get dist-upgrade -y && apt-get install -y openjdk-11-jre

COPY . ./
RUN dotnet restore Dominio/Dominio.csproj 
RUN dotnet restore Test/Test.cshtml  

RUN dotnet test Test/Test.cshtml \
    /p:CollectCoverage=true \
    /p:CoverletOutputFormat=opencover
    
RUN dotnet tool install -g dotnet-sonarscanner
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet sonarscanner begin /k:"tambahelper" \
    /d:sonar.host.url="http://192.168.1.5:9001" \
    /d:sonar.verbose=true \
    /v:1.0.0 \
    /d:sonar.login="497d4ddb33b826b88f424db44bb7ec6775ed052d" \
    /d:sonar.cs.opencover.reportsPaths="./Test/coverage.opencover.xml" \
    /d:sonar.coverage.exclusions="**Should.cs"