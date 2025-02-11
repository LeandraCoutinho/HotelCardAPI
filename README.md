# 🏨 HOTEL CARD: Sistema de Gestão de Hóspedes e Acessos

## 📖 Descrição

O **HOTEL CARD** é um sistema desenvolvido para gerenciar de forma eficiente os dados de hóspedes, controlar os acessos às áreas do hotel e monitorar o consumo de serviços. O sistema utiliza **cartões de acesso** para registrar as entradas e saídas dos hóspedes, além de registrar consumos, proporcionando maior segurança e controle.

### 🎯 **Objetivo:**

- Centralizar as informações dos hóspedes em um único sistema.
- Controlar os acessos às áreas restritas do hotel.
- Monitorar o consumo de serviços pelos hóspedes.
- Facilitar a geração de relatórios para a gestão do hotel.

### ⚡ **Funcionalidades:**

- **📋 Cadastro de Hóspedes:** Cadastro completo de hóspedes, incluindo dados pessoais, reservas e informações de contato.
- **💳 Gerenciamento de Cartões:** Emissão e gerenciamento de cartões de acesso para hóspedes.
- **🚪 Controle de Acessos:** Registro automático das entradas e saídas dos hóspedes por meio dos cartões.
- **🍽️ Monitoramento de Consumos:** Controle do consumo de serviços como restaurante.

---

## 🛠️ **Tecnologias Utilizadas**

O projeto foi desenvolvido utilizando as seguintes tecnologias:

- **C#**
- **.NET 8**
- **Entity Framework Core 8**
- **MySQL**
- **AutoMapper**
- **FluentValidation**
- **ScottBrady91.AspNetCore.Identity.Argon2PasswordHasher**

---

## 🚀 **Como Rodar o Projeto**

### 🔧 **1. Pré-requisitos**
Certifique-se de ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/)

### 📥 **2. Clonar o Repositório**

```sh
 git clone https://github.com/seu-usuario/hotel-card.git
 cd hotel-card
```

### 🗄️ **3. Configurar o Banco de Dados**

1. Crie um banco de dados MySQL chamado `hotel_card`.
2. Atualize a string de conexão no arquivo `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=hotel_card;User=root;Password=sua_senha;"
}
```

3. Rode as migrações do Entity Framework para criar as tabelas:

```sh
 dotnet ef database update
```

### ▶️ **4. Executar o Projeto**

Para rodar a API, execute o seguinte comando:

```sh
 dotnet run
```

## 🤝 **Contribuindo**

Se deseja contribuir com o projeto:

1. Faça um fork do repositório
2. Crie uma branch (`git checkout -b minha-feature`)
3. Faça as alterações necessárias
4. Faça commit das mudanças (`git commit -m 'Minha nova feature'`)
5. Envie um push para a branch (`git push origin minha-feature`)
6. Abra um Pull Request

---

