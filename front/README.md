# 🍣 Sushi Market — Frontend

React + TypeScript frontend for the Sushi Market e-commerce application.

The application provides a responsive customer-facing interface and an administrative dashboard for managing products, categories, locations and promotional content.

---

## ✨ Features

### 🛒 Customer Application

* Product and category browsing
* Product details
* Location information
* Responsive interface
* Ukrainian and English localization
* Google Maps integration
* Google authentication

### 🛠️ Admin Dashboard

* Product management
* Category management
* CRUD interfaces
* Interactive tables and forms
* Drag-and-drop reordering
* Responsive admin interface

### ↕️ Drag & Drop

The admin dashboard uses **@dnd-kit** for interactive drag-and-drop functionality.

It allows administrators to reorder managed content through an intuitive interface.

### 🔵 Google Login

The application supports authentication through a Google account using **Google OAuth 2.0**.

The frontend integrates the Google authentication flow with the backend authentication system.

### 🗺️ Google Maps

The application uses **react-google-maps** to integrate Google Maps functionality into the interface.

Maps can be used to display and work with location-based information.

### 🌍 Localization

The interface supports:

* 🇺🇦 Ukrainian
* 🇬🇧 English

Localization is implemented using **i18next**.

---

## 🧰 Technologies

### Core

* **React**
* **TypeScript**
* **React Router**

### State Management

* **MobX**

MobX is used for application state management and separation of UI state from presentation components.

### UI

* **Ant Design**
* **SCSS / SASS**
* **BEM**

Ant Design provides reusable UI components, while SCSS/SASS and BEM are used for custom styling and component-specific layouts.

### Libraries & Integrations

* **@dnd-kit** — drag-and-drop functionality
* **i18next** — localization
* **react-google-maps** — Google Maps integration
* **Google OAuth 2.0** — authentication

---

## 📱 Responsive Design

The application is designed to provide a consistent experience across:

* Desktop
* Tablet
* Mobile

The administrative dashboard also includes responsive layouts for tables, forms and management interfaces.

---

# 🚀 Getting Started

## Prerequisites

Install:

* [Node.js](https://nodejs.org/)
* npm
* Git

---

## 📥 Clone the Repository

```bash
git clone https://github.com/InnetaSh/react-sushi_market-app.git
```

Navigate to the frontend:

```bash
cd sushi_market_front
```

Install dependencies:

```bash
npm install
```

---

## ⚙️ Configuration

Some frontend integrations require environment variables.

Create a local environment file if required by the project:

```text
.env
```

Do not commit environment files containing private API keys or credentials.

For Google Maps and Google authentication, configure the required public client-side configuration according to the project's environment setup.

---

## ▶️ Run the Application

Start the development server:

```bash
npm start
```

The application will be available at the local development URL displayed by the terminal.

---

## 📁 Frontend Structure

A simplified project structure:

```text
sushi_market_front/
│
├── public/
│
├── src/
        ├── @types/
        │
        ├── api/
        │   ├── categoryApi.js
        │   ├── locationApi.js
        │   ├── newsApi.js
        │   ├── productApi.js
        │   ├── promotionApi.js
        │   └── userApi.js
        │
        ├── assets/
        │   └── styles/
        │       ├── abstracts/
        │       ├── base/
        │       └── index.scss
        │
        ├── components/
        │   ├── layout/
        │   ├── sections/
        │   └── UI/
        │
        ├── contexts/
        │
        ├── hooks/
        │
        ├── img/
        │
        ├── models/
        │
        ├── pages/
        │
        ├── routes/
        │
        ├── stores/
        │
        ├── App.js
        ├── index.js
        └── index.tsx
│
├── package.json
├── tsconfig.json
└── README.md
```

The project follows a component-based architecture with reusable components, custom hooks and MobX stores.

---

## 🔗 Backend

The frontend communicates with the Sushi Market ASP.NET Core Web API.

For backend setup and architecture, see:

➡️ **[Backend README](../sushi_market_back/README.md)**

---

## 🎯 Frontend Goals

The frontend was developed with a focus on:

* Reusable React components
* Type-safe development with TypeScript
* Centralized state management with MobX
* Responsive UI
* Multilingual support
* Reusable Ant Design components
* Maintainable SCSS architecture
* Interactive drag-and-drop functionality
* Integration with external services
