# ControleFinanceiro
## Projeto .NET 5 com arquitetura micro-serviços com as ferramentas:
- Swagger
- EF Core
- SQL Server
- MongoDB
- Kafka
- ELK (Elastic, Kibana)
- Hangfire

## Migrations
- dotnet ef migrations add Initial
- dotnet ef migrations remove
- dotnet ef database update

### CreditCard.API
API CRUD com persistência SQL Server

### BankTransactions.API
API com persistência MongoDB CQRS + Produtor Kafka

### Wallet.API
API com persistência MongoDB CQRS + Consumidor Kafka


