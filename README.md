# TC – Loja de Games API

usa Entity Framework Core
usa migrations para criar o banco

Utiliza **Entity Framework Core** para gerenciar os modelos de **Usuários** e **Jogos**, e **migrations** para criar o banco de dados automaticamente.

## Requisitos
- .NET SDK 8

## Como testar: Restaurando e compilando o projeto:

1. dotnet restore
dotnet build

2. Restaurar a ferramenta do EF:
dotnet tool restore

3. Criar o banco de dados via migrations (SQLite):
dotnet tool run dotnet-ef database update --project TcLojaGames.Infra --startup-project TcLojaGames.Api

4. Rodar a API:
dotnet run --project TcLojaGames.Api

5. Acessar o Swagger

Sugestão de teste
Criar um Jogo via POST
Listar Jogos via GET



## Qualidade de Software (Testes Unitários + TDD)

Este projeto possui um projeto de testes unitários (**TcLojaGames.Tests**) para validar regras de negócio do módulo de autenticação.

### O que foi testado (regras de negócio)

**AuthValidation**
- A senha deve conter **letras, números e caractere especial** (senhas fracas devem falhar).

**AuthService**
- Não permite cadastro com **e-mail já existente**.
- Normaliza o e-mail (`trim` + `lowercase`) antes de consultar/salvar.
- Armazena a senha como **hash BCrypt** (não salva senha em texto puro).
- Login falha para:
  - usuário inexistente
  - senha incorreta
- Login retorna um **AuthResponse** (token + expiração) quando credenciais são válidas.

### TDD aplicado
Foi aplicado **TDD no módulo de autenticação** (AuthValidation/AuthService), garantindo que as regras de negócio fossem validadas por testes automatizados.

### Como rodar os testes

Na pasta raiz do projeto (onde está o arquivo `TcLojaGames.sln`), execute:

```powershell
dotnet test