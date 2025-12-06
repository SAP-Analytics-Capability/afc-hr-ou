#!/bin/bash
echo "executing build_script.sh"

echo "Who Am I:"
whoami

echo "I'm in"
echo "pwd"
pwd

echo "what is inside the folder:"
ls -alp

#mvn package -f src/docker_helloworld
#cp src/docker_helloworld/target/HelloWorld-1.0.jar container_orchestration/containers/docker_helloworld

# Servizio Master Data
# dotnet publish src/services/masterdata/masterdata.csproj -c Release -o out
cp -r src/masterdata/out/* container_orchestration/containers/masterdata

cp -r src/bwsync/out/* container_orchestration/containers/bwsync

cp -r src/hrsync/out/* container_orchestration/containers/hrsync

cp -r src/rulesmngt/out/* container_orchestration/containers/rulesmngt

cp -r src/snowsync/out/* container_orchestration/containers/snowsync