FROM amazonlinux:latest

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /bwsync
COPY . ./
ENTRYPOINT ["dotnet", "bwsync.dll"]