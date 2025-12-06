FROM amazonlinux:latest

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /hrsync
COPY . ./
ENTRYPOINT ["dotnet", "hrsync.dll"]