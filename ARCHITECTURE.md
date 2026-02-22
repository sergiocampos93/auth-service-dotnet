# Auth Service - Architecture Notes

## Overview

Authentication microservice built using Clean Architecture principles.

Layers:

API → Application → Domain  
Infrastructure → Domain  
Domain → no dependencies  

---

## Responsibility per Layer

### API
- Exposes HTTP endpoints
- Handles authentication configuration
- Registers dependencies (DI)
- Does not contain business rules

### Application
- Contains use cases (Register, Login, Refresh)
- Orchestrates domain logic
- Defines contracts (interfaces)

### Domain
- Core entities (User, RefreshToken)
- Business rules
- No external dependencies

### Infrastructure
- Database access (EF Core + SQL Server)
- Token generation (JWT)
- External providers (Google OIDC in future)

---

## Dependency Rules

- Dependencies always point inward.
- Domain must not depend on other layers.
- Infrastructure implements interfaces defined in Application/Domain.
- Business rules must not depend on frameworks.

---

## Design Decisions

- SQL Server chosen due to familiarity and market alignment.
- JWT with refresh token rotation for session security.
- Role-based authorization for access control.