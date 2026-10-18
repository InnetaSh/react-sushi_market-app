# 🍣 Sushi Market

A full-stack e-commerce web application for a modern sushi delivery service.

Sushi Market consists of a **React frontend** and an **ASP.NET Core backend**, providing a customer-facing application and a feature-rich administrative dashboard.

## 🏗️ Project Structure

```text
Sushi Market
│
├── sushi_market_back/       # ASP.NET Core Web API
│   └── README.md
│
├── sushi_market_front/      # React application
│   └── README.md
│
└── README.md
```

## 🚀 Main Features

* 🛒 Customer-facing sushi marketplace
* 🛠️ Administrative dashboard
* 🔐 JWT authentication and authorization
* 🔄 Refresh token authentication
* 🔵 Google OAuth 2.0 login
* 🌍 Ukrainian and English localization
* ☁️ Cloudinary media management
* 🌐 Automatic content translation
* 🗺️ Google Maps integration
* ↕️ Drag-and-drop content reordering
* 📱 Responsive design
* 🧪 Automated backend testing

## 🧰 Main Technologies

### Backend

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* MediatR / CQRS
* ASP.NET Core Identity
* JWT / Refresh Tokens
* FluentValidation
* FluentResults

### Frontend

* React
* TypeScript
* MobX
* Ant Design
* React Router
* i18next
* @dnd-kit
* react-google-maps
* SCSS / SASS
* BEM

### Services

* Cloudinary
* Google OAuth 2.0
* Google Maps
* TranslateAPI

### Testing

* xUnit
* Moq
* FluentAssertions

## 📂 Applications

### Backend

The backend is an ASP.NET Core Web API responsible for business logic, authentication, database access, validation and external service integrations.

➡️ **[Backend README](./sushi_market_back/README.md)**

### Frontend

The frontend is a React + TypeScript application providing the customer interface and administrative dashboard.

➡️ **[Frontend README](./sushi_market_front/README.md)**

## 🐳 Running with Docker

Run the entire application stack (Frontend, Backend, and PostgreSQL database) using Docker Compose:

### 1. Start the stack
```bash
docker compose up --build
```

### 2. Available Endpoints
* **Frontend**: [http://localhost:3000](http://localhost:3000)
* **Backend API / Swagger UI**: [http://localhost:5292/swagger](http://localhost:5292/swagger)
* **PostgreSQL Database**: `localhost:5432` (Database: `Userdb`, User: `postgres`)

Database migrations and initial seed data are applied automatically when the backend container starts.

### 3. Stop the stack
```bash
docker compose down
```

To remove persistent volumes as well:
```bash
docker compose down -v
```

## 🎯 Project Goals

The project was developed to practice and demonstrate modern full-stack development using **C#/.NET and React**, including:

* Clean separation of responsibilities
* CQRS architecture
* Secure authentication
* External service integration
* Cloud media storage
* Multilingual applications
* Reusable frontend components
* Automated testing
* Responsive UI development

## 👩‍💻 Author

**Inneta Shponka**

Full-stack developer focused on **C#, .NET and React**.
