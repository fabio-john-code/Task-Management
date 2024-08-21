docker run -d --name task-management-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=senhaSA" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU15-ubuntu-20.04
