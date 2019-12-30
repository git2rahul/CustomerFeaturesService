FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src

COPY CustomerFeaturesService/CustomerFeaturesService.csproj CustomerFeaturesService/
COPY CustomerFeature.Data/CustomerFeature.Data.csproj CustomerFeature.Data/

RUN dotnet restore "CustomerFeaturesService/CustomerFeaturesService.csproj"

COPY . .
WORKDIR "/src/CustomerFeaturesService"
RUN dotnet build "CustomerFeaturesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerFeaturesService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerFeaturesService.dll"]
