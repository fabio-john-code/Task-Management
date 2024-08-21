Instalar Docker Desktop

executar os comandos abaixo para criação do RabbitMQ 6.8.1 e MSSQL 22

docker run -d --name task-management-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management 

docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=senhaSA0821" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

Clonar o repositório https://github.com/fabio-john-code/Task-Management/

Acessar o sql server com o usuario sa e executar o script .\Task-Management\TaskWorker\Database\init.sql

Abrir a solução TaskRestAPI.sln no visual studio 22

Acertar o ip do Rabbit MQ nos appsettings.json do TaskWorker e TaskRestAPI

Acertar o ip do SQL server nos appsettings.json do TaskWorker

executar a solução com o docker-compose

executar no powershell na pasta task-app o seguinte comando

ng serve

abrir no browser o endereço informado
