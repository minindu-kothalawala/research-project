# Configurable Multi-Database Full Stack Application

## 1. Project Overview

This project aims to build a full-stack application that demonstrates a **configurable multi-database architecture** using a .NET backend and a React frontend.

The key objective is to allow switching between **MongoDB (NoSQL)** and **Microsoft SQL Server (MSSQL)** at runtime using only a configuration change, without modifying application code.

---

## 2. Core Requirements

- Backend must support:
  - MongoDB
  - MSSQL

- Database switching must:
  - Be controlled via configuration (appsettings.json)
  - Require NO code changes
  - Require NO recompilation

- Frontend must:
  - Perform full CRUD operations
  - Communicate with backend via REST API

---

## 3. Technology Stack

### Backend
- .NET 7+ (ASP.NET Core Web API)
- MongoDB (MongoDB.Driver)
- MSSQL (Entity Framework Core)

### Frontend
- ReactJS (Vite or Create React App)
- Axios or Fetch API

---

## 4. Architecture Overview

The backend must follow a **clean layered architecture**:

- Controllers (API layer)
- Services (business logic)
- Repositories (data access)
- Repository Interfaces
- Factory (for database selection)
- Models (domain entities)
- Configuration

### Key Design Patterns:
- Repository Pattern
- Factory Pattern
- Dependency Injection

---

## 5. Important Design Rules

1. Service layer must NOT depend on any database implementation.
2. Service layer must ONLY depend on repository interfaces.
3. Repository implementations must be separated:
   - MongoDB repository
   - MSSQL repository
4. Factory must be the ONLY place where database selection happens.
5. Switching database must work by changing configuration ONLY.

---

## 6. Domain Model

Create a simple entity:

### Product
- Id (string)
- Name (string)
- Description (string)
- Price (decimal)

This model must work for both MongoDB and MSSQL.

---

## 7. Repository Layer

### Interface: IProductRepository

Define async CRUD methods:
- GetAllAsync()
- GetByIdAsync(string id)
- CreateAsync(Product product)
- UpdateAsync(string id, Product product)
- DeleteAsync(string id)

### Implementations

#### MongoProductRepository
- Use MongoDB.Driver
- Use collection: "Products"

#### MssqlProductRepository
- Use Entity Framework Core
- Use DbContext with DbSet<Product>

---

## 8. Factory Pattern

Create a RepositoryFactory that:

- Reads "DatabaseType" from configuration
- Supports:
  - "MongoDB"
  - "MSSQL"
- Returns correct repository implementation

Factory must be used through Dependency Injection.

---

## 9. Service Layer

Create ProductService:

- Uses IProductRepository
- Contains business logic
- Must be database-agnostic

---

## 10. API Layer

Create ProductsController:

Endpoints:
- GET /api/products
- GET /api/products/{id}
- POST /api/products
- PUT /api/products/{id}
- DELETE /api/products/{id}

Use ProductService.

---

## 11. Configuration

Use appsettings.json:

```json
{
  "DatabaseType": "MongoDB",
  "ConnectionStrings": {
    "MongoDB": "...",
    "MSSQL": "..."
  }
}

## 12. Dependency Injection

Register the following services in the application:

- ProductService  
- RepositoryFactory  
- MongoDB client  
- MSSQL DbContext  

Ensure that the repository implementation is resolved dynamically based on the configuration.

---

## 13. Backend Expected Behavior

- CRUD operations must work correctly for both MongoDB and MSSQL.  

- Switching the database must require:
  - Changing the configuration (appsettings.json)
  - Restarting the application  

- No code modification should be required when switching databases.

---

## 14. Frontend Requirements

Create a React application with the following features:

### Features
- View all products  
- Add a product  
- Edit a product  
- Delete a product  

### Structure

#### Components
- ProductList  
- ProductForm  

#### API Service Functions
- getAllProducts  
- getProductById  
- createProduct  
- updateProduct  
- deleteProduct  

---

## 15. API Integration

- Base URL:  
  `http://localhost:<port>/api/products`

- Use:
  - Axios **or**
  - Fetch API  

---

## 16. Final Deliverable

The system must include:

### Backend
- Clean architecture  
- Database switching via configuration  
- Support for MongoDB and MSSQL  

### Frontend
- Fully functional CRUD user interface  

---

## 17. Validation Checklist

- [ ] Backend CRUD operations work  
- [ ] MongoDB integration works  
- [ ] MSSQL integration works  
- [ ] Database switching works via configuration only  
- [ ] No code changes required for switching databases  
- [ ] Frontend is connected and functioning correctly  

---

## 18. Key Focus

### Focus on:
- Clean architecture  
- Maintainability  
- Simplicity  
- Demonstrating database switchability  

### Avoid:
- Over-engineering  
- Complex UI design  
- Advanced optimizations  

---

## 19. Future Extensibility

Design the system so that new databases can be added easily, such as:

- PostgreSQL  
- MySQL  

---

## 20. Output Expectations

The system should generate:

- A complete backend project  
- A complete frontend project  
- Well-structured and maintainable code  
- Clear separation of concerns  