FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY CustomerFeaturesService/CustomerFeaturesApi.csproj CustomerFeaturesService/
COPY /CustomerFeature.Data/CustomerFeature.Repositories.csproj CustomerFeature.Data/
RUN dotnet restore "CustomerFeaturesService/CustomerFeaturesApi.csproj"
COPY . .
WORKDIR "/src/CustomerFeaturesService"
RUN dotnet build "CustomerFeaturesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerFeaturesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerFeaturesApi.dll"]
