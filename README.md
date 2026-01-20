# 🎮 TcLojaGames API

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![JWT](https://img.shields.io/badge/Auth-JWT-green)
![Swagger](https://img.shields.io/badge/OpenAPI-3.0-blue)

## 📌 Topicos
- [Tech Challenge](#-tech-challenge)
- [Visão Geral do Projeto](#-visão-geral-do-projeto)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Como testar: Restaurando e compilando o projeto](#-como-testar-restaurando-e-compilando-o-projeto)
- [Qualidade de Software](#qualidade-de-software-testes-unitários--tdd)
  - [O que foi testado](#o-que-foi-testado-regras-de-negócio) 
  - [TDD aplicado](#tdd-aplicado)
  - [Como rodar os testes](#como-rodar-os-testes)
- [Endpoints da API](#-endpoints-da-api)
  - [Autenticação](#-autenticação)
  - [Usuários](#-usuários)
  - [Jogos](#-jogos)
  - [Biblioteca](#-biblioteca)
- [Log e Erros](#-log-e-erros) 
- [Documentação / Apresentação](#-documentação--apresentação)
- [Alunos](#-alunos)

---
<br>
<br>

## 📌 Tech Challenge

- **Fase:** 1 
- **O problema:** FIAP Cloud Games (FCG) será uma plataforma de venda de jogos digitais e gestão de servidores para partidas online. Nesta primeira fase, você desenvolverá um serviço de cadastro de usuários e biblioteca de jogos adquiridos que servirá de base para as próximas fases do projeto.
Este desafio foi estruturado para aplicar os conhecimentos adquiridos nas disciplinas da fase.  
- **Desafio:** A FIAP decidiu lançar uma plataforma de games voltados para a educação de tecnologia. Ela possui a ideia de como o projeto deve funcionar e decidiu quebrá-lo em quatro fases para que o lançamento da FCG seja gradual e melhorado durante todo o processo de construção.
O objetivo desta fase é criar uma API REST em .NET 8 para gerenciar usuários e seus jogos adquiridos. O projeto precisa garantir persistência de dados, qualidade do software e boas práticas de desenvolvimento, preparando a base para futuras funcionalidades como matchmaking e gerenciamento de servidores.
Com esse MVP, a FIAP conseguirá seguir com o projeto avaliando o que deve ser melhorado e o que pode ser acrescentado para que o serviço seja robusto e suporte todos os alunos e alunas da FIAP, Alura e PM3.  

---
<br>
<br>

## 📌 Visão Geral do Projeto

API REST para **cadastro de usuários**, **autenticação JWT** e **gerenciamento de jogos digitais**.

Utiliza **Entity Framework Core** para gerenciar os modelos de **Usuários** e **Jogos**, e **migrations** para criar o banco de dados automaticamente.

---
<br>
<br>

## 📄Estrutura do Projeto

- **/TcLojaGames.Api**: Código fonte da API.
- **/TcLojaGames.Application**: Código fonte da aplicação.
- **/TcLojaGames.Domain**: Arquivos de modelo de domínio.
- **/TcLojaGames.Infra**: Arquivos de acesso a dados (banco de dados via Entity Framework).
- **TcLojaGames.Tests**: Arquivos de modelo de testes.

---
<br>
<br>

## 🎯 Como testar: Restaurando e compilando o projeto:

1. dotnet restore
```powershell
dotnet build
```

2. Restaurar a ferramenta do EF:
```powershell
dotnet tool restore
```

3. Criar o banco de dados via migrations (SQLite):
dotnet tool run
```powershell
dotnet-ef database update --project TcLojaGames.Infra --startup-project TcLojaGames.Api
```

4. Rodar a API:
```powershell
dotnet run --project TcLojaGames.Api
```

5. Acessar o Swagger
- Sugestão de teste
  - Criar um Jogo via POST
  - Listar Jogos via GET

---
<br>
<br>

## 🚀Qualidade de Software (Testes Unitários + TDD)

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
```

---
<br>
<br>

## 📑 Endpoints da API

## 🔐 Autenticação

A API utiliza autenticação **JWT Bearer**.

### Header padrão
```
Authorization: Bearer {seu_token}
```

Todos os endpoints, **exceto login e register**, exigem autenticação.

---

## 🔑 Usuários

### Registrar usuário
**POST** `/api/auth/register`

```json
{
  "name": "João Silva",
  "email": "joao@email.com",
  "password": "12345678"
}
```

---

### Login
**POST** `/api/auth/login`

```json
{
  "email": "joao@email.com",
  "password": "12345678"
}
```

**Response**
```json
{
  "accessToken": "string",
  "expiresAtUtc": "2026-01-18T12:00:00Z"
}
```

---

### Usuário logado
**GET** `/api/auth/me`

---

### Endpoint exclusivo Admin
**GET** `/api/auth/admin-only`

---

## 🎮 Jogos

### Listar jogos
**GET** `/api/jogos`

---

### Cadastrar jogo
**POST** `/api/jogos`

```json
{
  "descricao": "God of War",
  "genero": "Ação",
  "preco": 199.99
}
```

---

### Buscar jogo por ID
**GET** `/api/jogos/{id}`

---

### Atualizar jogo
**PUT** `/api/jogos/{id}`

---

### Remover jogo
**DELETE** `/api/jogos/{id}`

---

### Aplicar promoção
**POST** `/api/jogos/{id}/promocao`

```json
{
  "novoPreco": 149.99
}
```

---

## 📚 Biblioteca

### Minha biblioteca
**GET** `/api/biblioteca/me`

---

## 🛠️ Administração da Biblioteca

### Vincular jogo a usuário
**POST** `/api/admin/biblioteca`

```json
{
  "email": "cliente@email.com",
  "jogoId": "uuid"
}
```

---

### Listar biblioteca por usuário
**GET** `/api/admin/biblioteca?email=cliente@email.com`

---

### Remover jogo da biblioteca
**DELETE** `/api/admin/biblioteca?email=cliente@email.com&jogoId=uuid`

---
<br>
<br>

## 📁 Log e Erros

O tratamento de log e erro estão disponivel para visualizar no console e arquivo .json ( diários ) localizado **/TcLojaGames.Api/Logs/**, no formato :
- **info-YYYYMMDD.json** Fluxo normal do sistema / Regra de negócio / inconsistência
- **error-YYYYMMDD.json** Exceção tratada / erro conhecido / Falha grave / crash

---
<br>
<br>

## 📚 Documentação / Apresentação
- [Event Storming - DDD](https://miro.com/welcomeonboard/SSt5Y3VPdzdBS0g3WnM2RmFIUEZIc0JVeWJPUStUQlN3SmcxbE1uM2lOUWNpWGV1MnVaQ3lPTFRySlVYWms3cVIra3pyZUdDYzhyMzdEWjBVMUVnV2dwbGhFMXpFWWVEMkh6cUZLbDEyekNhT1JEdkNKc3VzcXV5aUVCRTNYMWhyVmtkMG5hNDA3dVlncnBvRVB2ZXBnPT0hdjE=?share_link_id=240620216519)
- [Video Apresentação](#-tclojagames-api)

---
<br>
<br>

## ✨ Alunos

- Pedro Delgado Henriques -rm369869
- Matheus Machado Pinheiro do Valle - rm369919
- Clovis Ribeiro Ramos - rm369652
 
---
