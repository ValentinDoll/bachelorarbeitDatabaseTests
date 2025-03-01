FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

ENV ACCEPT_EULA=Y

RUN apt-get update && apt-get install -y mssql-tools unixodbc-dev


CMD /opt/mssql/bin/sqlservr
