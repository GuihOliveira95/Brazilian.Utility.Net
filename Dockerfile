FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

COPY . .

RUN dotnet publish -c Release -o /app/out Brazilian.Utility.Net.Api

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime

WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
EXPOSE 443

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

ENTRYPOINT ["dotnet", "Brazilian.Utility.Net.Api.dll"]