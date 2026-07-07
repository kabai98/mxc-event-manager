MXC Event Manager

A simple full-stack event management application built with ASP.NET Core Web API and Angular.

The application allows users to create, manage, and view events through a modern web interface. The backend provides REST API endpoints, user authentication, and database communication, while the frontend provides an interactive user interface.

The project uses SQL Server running in a Docker container, with Docker Compose used to simplify database setup and ensure a consistent development environment.

Main technologies:

ASP.NET Core Web API
Entity Framework Core
SQL Server (Docker)
ASP.NET Core Identity
Angular
TypeScript
Angular Material

The goal of the project is to demonstrate a complete full-stack application with a separated backend and frontend architecture, database persistence, and user authentication.

Before running the project, install:

- .NET 10 SDK
- Node.js
- npm
- Docker Desktop

The project uses SQL Server running inside a Docker container.

Recommended:

- Visual Studio 2026 / Rider
- Visual Studio Code for frontend development
- 
## 🐳 Database Setup

The application uses:

- SQL Server
- Docker Compose
- Entity Framework Core migrations

The SQL Server database runs inside a Docker container.

Start the database:

```bash
docker compose up -d
