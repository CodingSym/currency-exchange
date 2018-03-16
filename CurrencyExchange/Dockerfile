FROM microsoft/dotnet:latest
COPY . /app

WORKDIR /app/src/CurrencyExchange.WebApi
RUN ["dotnet", "build"]

EXPOSE 5000
ENV ASPNETCORE_URLS http://+:5000

ENTRYPOINT ["dotnet", "run"]