# Biblioteca Digital API

Este projeto implementa uma API RESTful para uma Biblioteca Digital, utilizando ASP.NET Core com arquitetura em camadas e integração com Docker. O sistema permite o gerenciamento de livros e seus respectivos gêneros literários.

## Arquitetura

O projeto segue uma arquitetura em camadas, dividida em três projetos principais:

```
MonolitoBackend/
├── MonolitoBackend.Core             # Entidades e interfaces
├── MonolitoBackend.Infrastructure   # Implementações e infraestrutura
└── MonolitoBackend.Api              # Endpoints e configurações da API
```

### Diagrama da Arquitetura

```
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│                 │     │                 │     │                 │
│  API (Web)      │ ──► │  Serviços       │ ──► │  Repositórios   │ ──► PostgreSQL
│                 │     │                 │     │                 │
└─────────────────┘     └─────────────────┘     └─────────────────┘
       │                        │                       │
       │                        │                       │
       ▼                        ▼                       ▼
┌─────────────────────────────────────────────────────────────────┐
│                                                                 │
│                          Entidades do Domínio                   │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Modelo de Domínio

- **Book (Livro)**
  - Id: int (chave primária)
  - Title: string (obrigatório)
  - Author: string (obrigatório)
  - ISBN: string (obrigatório)
  - PublishedYear: int (obrigatório)
  - GenreId: int (chave estrangeira)
  - Genre: Objeto de navegação

- **Genre (Gênero)**
  - Id: int (chave primária)
  - Name: string (obrigatório)
  - Description: string (opcional)
  - Books: Coleção de livros relacionados

## Requisitos

- Docker e Docker Compose
- .NET 7.0 SDK (para desenvolvimento local)

## Configuração e Execução

### Variáveis de Ambiente

Antes de executar o projeto, configure as seguintes variáveis de ambiente:

```bash
export DB_USER=bibliotecauser
export DB_PASSWORD=bibliotecapass
export DB_NAME=bibliotecadb
```

Ou crie um arquivo `.env` na raiz do projeto:

```
DB_USER=bibliotecauser
DB_PASSWORD=bibliotecapass
DB_NAME=bibliotecadb
```

### Executando com Docker

1. Clone o repositório:
   ```bash
   git clone [URL_DO_REPOSITORIO]
   cd biblioteca-digital-api
   ```

2. Execute usando Docker Compose:
   ```bash
   docker-compose up -d
   ```

3. Acesse a API via Swagger:
   ```
   http://localhost:5000/swagger
   ```

### Executando Localmente (sem Docker)

1. Instale o PostgreSQL e crie um banco de dados

2. Configure a string de conexão em `appsettings.Development.json` ou use variáveis de ambiente

3. Execute a API:
   ```bash
   cd MonolitoBackend.Api
   dotnet run
   ```

## Endpoints da API

### Endpoints de Gêneros (Genres)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/genres` | Lista todos os gêneros |
| GET | `/genres/{id}` | Obtém um gênero específico por ID |
| POST | `/genres` | Cria um novo gênero |
| PUT | `/genres/{id}` | Atualiza um gênero existente |
| DELETE | `/genres/{id}` | Remove um gênero |
| GET | `/genres/{id}/books` | Lista todos os livros de um gênero |

#### Exemplo de Criação de Gênero

```http
POST /genres
Content-Type: application/json

{
  "name": "Ficção Científica",
  "description": "Gênero literário que explora avanços científicos e tecnológicos"
}
```

### Endpoints de Livros (Books)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/books` | Lista todos os livros |
| GET | `/books/{id}` | Obtém um livro específico por ID |
| GET | `/books/by-genre/{genreId}` | Lista livros por gênero |
| POST | `/books` | Cria um novo livro |
| PUT | `/books/{id}` | Atualiza um livro existente |
| DELETE | `/books/{id}` | Remove um livro |

#### Exemplo de Criação de Livro

```http
POST /books
Content-Type: application/json

{
  "title": "Fundação",
  "author": "Isaac Asimov",
  "isbn": "9788576572664",
  "publishedYear": 1951,
  "genreId": 1
}
```

## Solução de Problemas

### Problemas com a Conexão ao Banco de Dados

Se você encontrar problemas na conexão com o PostgreSQL:

1. Verifique se o container do PostgreSQL está em execução:
   ```bash
   docker ps
   ```

2. Verifique os logs do container:
   ```bash
   docker logs biblioteca-postgres
   ```

3. Verifique se as variáveis de ambiente estão configuradas corretamente

### Migrações Não Aplicadas

Se as tabelas não forem criadas automaticamente:

1. Entre no container da API:
   ```bash
   docker exec -it biblioteca-api /bin/bash
   ```

2. Execute as migrações manualmente:
   ```bash
   dotnet ef database update
   ```

## Desenvolvimento

Para contribuir com o projeto:

1. Crie uma nova branch para suas alterações:
   ```bash
   git checkout -b feature/nova-funcionalidade
   ```

2. Implemente suas alterações

3. Execute os testes unitários:
   ```bash
   dotnet test
   ```

4. Envie um Pull Request
