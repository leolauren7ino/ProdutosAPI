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
    CATEGORIA ||--o{ PRODUTO : has
    CLIENTE ||--o{ ENDERECO : has
    CLIENTE ||--o{ PEDIDO : places
    PEDIDO
