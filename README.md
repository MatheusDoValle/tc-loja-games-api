# TC – Loja de Games API

usa Entity Framework Core
usa migrations para criar o banco

Utiliza **Entity Framework Core** para gerenciar os modelos de **Usuários** e **Jogos**, e **migrations** para criar o banco de dados automaticamente.

## Requisitos
- .NET SDK 8

## Como testar
1) Restaurar e compilar o projeto:

1.dotnet restore
dotnet build

2. Restaurar a ferramenta do EF:
dotnet tool restore

3. Criar o banco de dados via migrations (SQLite):
dotnet tool run dotnet-ef database update --project TcLojaGames.Infra --startup-project TcLojaGames.Api

4. Rodar a API:
dotnet run --project TcLojaGames.Api

5.Acessar o Swagger

Sugestão de teste
Criar um Jogo via POST
Listar Jogos via GET
