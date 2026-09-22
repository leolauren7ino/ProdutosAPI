# ProdutosAPI

API REST em **ASP.NET Core** para gestão de produtos, categorias, clientes e pedidos, construída como projeto de estudo prático em C#/.NET, com foco em arquitetura de dados relacional, boas práticas de API e integração com RPA (UiPath) e front-end.

> Projeto desenvolvido durante o início do meu estágio em TI, como forma de praticar os conceitos de C#/.NET, Entity Framework Core, autenticação JWT e automação, aplicando-os num domínio de negócio real (vendas/estoque).

---

## Tecnologias utilizadas

- **ASP.NET Core Web API** (.NET 10)
- **Entity Framework Core** + **SQL Server (LocalDB)**
- **JWT (JSON Web Token)** para autenticação e autorização
- **BCrypt.Net** para hash seguro de senhas
- **xUnit** para testes automatizados
- **HTML + JavaScript puro** (fetch API) como front-end de consumo
- **UiPath Studio** para automação (RPA) consumindo a API

---

## Arquitetura de dados

```mermaid
erDiagram
    CATEGORIA ||--o{ PRODUTO : possui
    CLIENTE ||--o{ ENDERECO : possui
    CLIENTE ||--o{ PEDIDO : realiza
    PEDIDO ||--o{ ITEM_PEDIDO : contém
    PRODUTO ||--o{ ITEM_PEDIDO : está_em

    CATEGORIA {
        int Id
        string Nome
    }
    PRODUTO {
        int Id
        string Nome
        decimal Preco
        int Estoque
        int CategoriaId
    }
    CLIENTE {
        int Id
        string Nome
        string Email
        string Telefone
    }
    ENDERECO {
