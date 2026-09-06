# 🍣 Sushi Market — Backend

ASP.NET Core Web API backend for the Sushi Market e-commerce application.

The backend provides business logic, authentication, database access, validation, media management, automatic translation and communication with external services.

---

## 🏗️ Architecture

The application follows a modular architecture based on **CQRS (Command Query Responsibility Segregation)** and **MediatR**.

A simplified request flow:

```text
HTTP Request
     │
     ▼
ASP.NET Core Controller
     │
     ▼
MediatR
     │
     ├── Command
     │      └── Command Handler
     │
     └── Query
            └── Query Handler
                    │
                    ▼
             Entity Framework Core
                    │
                    ▼
                 Database
```

Cross-cutting concerns are handled using:

* MediatR pipeline behaviors
* FluentValidation
* ASP.NET Core middleware
* Centralized exception handling
* Logging
* Authentication and authorization

This architecture keeps controllers lightweight and separates application logic into independent handlers.

---

## ✨ Features

### 🔐 Authentication & Authorization

* User registration and login
* ASP.NET Core Identity
* JWT Bearer authentication
* Access and refresh tokens
* Role-based authorization
* Google OAuth 2.0 authentication

Users can authenticate using either the standard application authentication flow or their Google account.

Sensitive authentication configuration is stored outside the repository using secure configuration mechanisms.

---

### 📦 Product & Category Management

The API provides CRUD operations for:

* Products
* Categories
* Locations
* Promotional content

The backend handles validation, persistence and business rules for administrative operations.

---

### 🌍 Translation

The backend integrates **TranslateAPI** for automatic translation of product and category:

* titles
* descriptions

The API key is stored using .NET User Secrets and is not committed to source control.

---

### ☁️ Cloudinary

Cloudinary is used for centralized cloud media storage.

Assets are organized into structured folders:

```text
sushi_market/
├── products/
├── locations/
└── promotions/
```

The application stores cloud URLs in the database instead of storing image files directly in the database.

Cloudinary `PublicId` configuration is used to preserve original filenames and maintain structured asset organization.

---

## 🗄️ Database

The application uses **Entity Framework Core** for database access.

Supported databases:

* SQL Server
* SQLite

Database schema changes are managed through EF Core migrations.

Apply migrations with:

```bash
dotnet ef database update
```

If the EF Core CLI is not installed:

```bash
dotnet tool install --global dotnet-ef
```

---

## 📨 Messaging

The backend uses **MassTransit** for message-based communication.

**RabbitMQ** is used as the message broker.

This allows services and application components to communicate asynchronously and provides a foundation for event-driven functionality.

---

## 🧰 Technologies

* **C#**
* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **ASP.NET Core Identity**
* **MediatR**
* **CQRS**
* **FluentValidation**
* **FluentResults**
* **JWT Bearer Authentication**
* **Refresh Tokens**
* **RabbitMQ**
* **Cloudinary**
* **TranslateAPI**
* **SQL Server**
* **SQLite**

---

## 🧪 Testing

The backend contains **200+ automated tests**.

Testing tools include:

* xUnit
* Moq
* FluentAssertions
* MassTransit TestHarness

The tests cover:

* Command handlers
* Query handlers
* Validators
* Controllers
* Business logic
* Authentication-related functionality
* Integration scenarios
* Message-based functionality

Run all tests:

```bash
dotnet test
```

---

# 🚀 Getting Started

## Prerequisites

Install:

* [.NET 8 SDK](https://dotnet.microsoft.com/)
* SQL Server or SQLite
* Git

---

## 📥 Clone the Repository

```bash
git clone https://github.com/InnetaSh/react-sushi_market-app.git
```

Navigate to the backend:

```bash
cd sushi_market_back
```

Restore dependencies:

```bash
dotnet restore
```

---

## 🔐 Configuration

Sensitive credentials are not stored in the repository.

The project uses **.NET User Secrets** for local development.

### Cloudinary

```bash
dotnet user-secrets set "Cloudinary:CloudName" "YOUR_CLOUD_NAME"
dotnet user-secrets set "Cloudinary:ApiKey" "YOUR_API_KEY"
dotnet user-secrets set "Cloudinary:ApiSecret" "YOUR_API_SECRET"
```

### TranslateAPI

```bash
dotnet user-secrets set "Translator:ApiKey" "YOUR_API_KEY"
```

### Google OAuth

Configure the Google OAuth credentials required by the application using User Secrets:

```bash
dotnet user-secrets set "Authentication:Google:ClientId" "YOUR_CLIENT_ID"
dotnet user-secrets set "Authentication:Google:ClientSecret" "YOUR_CLIENT_SECRET"
```


---

## ▶️ Run the Application

Start the backend API:

```bash
dotnet run
```

The API will start using the configured development environment.

---

## 📁 Backend Structure

A simplified project structure:

```text
## 📁 Backend Structure

```text
Sushi Market
│
├── sushi_market_back/                # ASP.NET Core Web API
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── CategoriesController.cs
│   │   ├── LocationsController.cs
│   │   ├── NewsController.cs
│   │   ├── ProductsController.cs
│   │   └── PromotionsController.cs
│   │
│   ├── Middleware/
│   ├── Models/
│   ├── Connected Services/
│   ├── appsettings.json
│   └── Program.cs
│
├── SushiMarket.BLL/                  # Business Logic Layer
│   ├── DTOs/
│   ├── Exceptions/
│   ├── Helpers/
│   ├── Mapping/
│   ├── MediatR/
│   ├── Resources/
│   ├── Seeders/
│   ├── Services/
│   ├── Validators/
│   └── DependencyInjection.cs
│
├── SushiMarket.DAL/                  # Data Access Layer
│   ├── Entities/
│   ├── Enums/
│   ├── Migrations/
│   └── SushiMarketDbContext.cs
│
├── SushiMarket.XUnitTest/            # Automated tests
│   ├── BLL/
│   ├── WebApi/
│   └── TestResults/
│
└── README.md
```

The exact structure may evolve as the project develops.
