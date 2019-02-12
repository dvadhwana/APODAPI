
#Build Stage
FROM microsoft/dotnet:2.1-sdk-alpine3.7 as build-env
WORKDIR	 /DataMigration

#Restore
COPY azuretestapp/azuretestapp.csproj ./azuretestapp/
RUN dotnet restore azuretestapp/azuretestapp.csproj

COPY xunittestforazure/xunittestforazure.csproj ./xunittestforazure/
RUN dotnet restore xunittestforazure/xunittestforazure.csproj

#RUN ls -alR

#Copy Source
COPY . .

#Test
RUN dotnet test xunittestforazure/xunittestforazure.csproj

#Publish
RUN dotnet publish azuretestapp/azuretestapp.csproj -o /publish -c Release

# Runtime Image Stage
 FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine3.7
 COPY --from=build-env /publish /publish
 WORKDIR /publish
 EXPOSE 80
 ENTRYPOINT ["dotnet","azuretestapp.dll"]