FROM amazonlinux:latest

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /rulesmngt
COPY . ./
RUN apt install -y ca-certificates 
ENTRYPOINT ["dotnet", "rulesmngt.dll"]
