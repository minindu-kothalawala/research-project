# Configurable Multi-Database Architecture

## Overview

This project demonstrates a **Configurable Multi-Database Architecture** developed using **ASP.NET Core Web API** and **ReactJS**. The primary objective of the project is to enable applications to switch between different database management systems through a simple configuration change, eliminating the need to modify application code.

The prototype currently supports **PostgreSQL** and **MongoDB**, while the architecture has been designed to be easily extended to support additional databases such as Microsoft SQL Server, MySQL, or others in the future.

This implementation was developed as part of an MSc research project investigating how configurable software architectures can improve flexibility, reduce maintenance effort, and help software vendors better satisfy customers with different cost and performance requirements.

---

# Problem Statement

Most enterprise applications are tightly coupled with a specific database management system. Although this approach simplifies the initial development process, it significantly reduces flexibility when business requirements change.

In product-based software development, different customers often have different priorities.

- Some customers require maximum performance and scalability.
- Others prioritise lower infrastructure and operational costs.
- Some organisations already have preferred database technologies.

When a software product is built around a single database technology, changing to another database usually requires extensive redevelopment, testing, and deployment effort.

This project addresses that limitation by introducing a configurable architecture that enables database switching without changing business logic.

---

# Project Objectives

The project aims to:

- Demonstrate a configurable multi-database architecture.
- Separate business logic from database implementation.
- Enable runtime database selection through configuration.
- Reduce redevelopment effort when changing database technologies.
- Improve maintainability and scalability.
- Provide a reusable architectural foundation for future database integrations.

---

# Technology Stack

## Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- MongoDB.Driver
- Dependency Injection
- Repository Pattern
- Factory Pattern

## Databases

- PostgreSQL
- MongoDB

---

# System Architecture

The solution follows a layered architecture to ensure clear separation of responsibilities.

<img width="392" height="713" alt="colored_2 drawio" src="https://github.com/user-attachments/assets/214034df-2686-4589-8a99-8e1b8f71391b" />


The **Service Layer** is completely independent of any database implementation.

The **Repository Factory** determines which repository implementation should be used by reading the application configuration.

---

# Project Structure

<img width="641" height="511" alt="Untitled Diagram-Page-1 drawio (1)" src="https://github.com/user-attachments/assets/8ed95210-de0a-446e-88aa-2bd9d3038ff0" />


---

# Design Patterns

## Repository Pattern

Provides a common interface for data access while hiding database-specific implementation details.

### Benefits

- Loose coupling
- Easier testing
- Better maintainability

---

## Factory Pattern

The Factory Pattern is the core of this research.

Instead of directly creating repository implementations throughout the application, a single factory determines which repository should be instantiated based on the selected database type.

This allows the remainder of the application to remain completely unaware of the underlying database technology.

---

## Dependency Injection

Dependency Injection is used throughout the application to manage object creation and dependencies.

This promotes:

- Better modularity
- Easier testing
- Greater extensibility

---

# Database Switching

The active database is controlled through the application configuration.

Example:

```json
{
  "DatabaseSettings": {
    "DatabaseType": "PostgreSQL"
  }
}
```

or

```json
{
  "DatabaseSettings": {
    "DatabaseType": "MongoDB"
  }
}
```

Changing the configuration and restarting the application is sufficient to switch databases.

No business logic or controller code needs to be modified.

---

# CRUD Functionality

The prototype supports complete CRUD functionality.

Operations include:

- Create Product
- Retrieve Products
- Retrieve Product by Id
- Update Product
- Delete Product

The same REST API endpoints are used regardless of the selected database.

---

# Extending the Architecture

The architecture has been intentionally designed for future expansion.

To add another database:

1. Create a new repository implementing the common repository interface.
2. Register the repository in Dependency Injection.
3. Update the Repository Factory.
4. Add the corresponding configuration option.

No changes are required in:

- Controllers
- Services
- Frontend

---

# Key Features

- Configuration-driven database selection
- Clean layered architecture
- Separation of business logic and data access
- Runtime database switching
- Reusable repository abstraction
- Easily extensible architecture
- RESTful API
- React-based frontend
- Database-independent business layer

---

# Research Contribution

This project proposes a practical software architecture that enables configurable database selection without requiring application code modifications.

Unlike traditional applications that are tightly coupled to a single database, this solution abstracts data access behind a common interface and dynamically selects the appropriate implementation through configuration.

The proposed architecture demonstrates how product-based software can be deployed to different customers with different database preferences while minimising redevelopment effort.

---

# Future Improvements

Potential future enhancements include:

- Support for Microsoft SQL Server
- Support for MySQL
- Support for Oracle Database
- Automatic database migration tools
- Docker-based deployment
- Cloud-native configuration management
- Performance benchmarking dashboard
- Automated integration tests
- Multi-tenant support

---

# Configuration

Update the database settings in `appsettings.json` before running the application.

Example:

```json
{
  "DatabaseSettings": {
    "DatabaseType": "PostgreSQL"
  }
}
```

or

```json
{
  "DatabaseSettings": {
    "DatabaseType": "MongoDB"
  }
}
```

Restart the application after changing the database type.

---
