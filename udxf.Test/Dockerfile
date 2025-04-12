FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["udxf.Test/udxf.Test.csproj", "udxf.Test/"]
COPY ["udxf.Utility/udxf.Utility.csproj", "udxf.Utility/"]
COPY ["udxf.Application/udxf.Application.csproj", "udxf.Application/"]
COPY ["udxf.Domain/udxf.Domain.csproj", "udxf.Domain/"]
RUN dotnet restore "./udxf.Test/udxf.Test.csproj"
COPY . .
WORKDIR "/src/udxf.Test"
RUN dotnet build "./udxf.Test.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet restore
CMD ["dotnet", "test"]