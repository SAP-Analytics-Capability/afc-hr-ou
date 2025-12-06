FROM amazonlinux:latest
# RUN yum -y install iputils

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /masterdata
COPY . ./
ENTRYPOINT ["dotnet", "masterdata.dll"]