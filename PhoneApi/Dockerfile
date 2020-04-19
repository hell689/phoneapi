FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet PhoneApi.dll