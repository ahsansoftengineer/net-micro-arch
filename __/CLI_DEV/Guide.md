
### Parent Project (Class Library vs Web API)

| Feature	                    | Class Library 📦      | Web API 🌐
| ------------------------------|-----------------------|---------------|
| Middleware Configurations     | ✅ Yes                |❌ No          |
| Dependency Injection (DI)     | ✅ Yes                |❌ No          |
| Shared Business Logic	        | ✅ Yes                |❌ No          |
| Exposes HTTP Endpoints	    | ❌ No                 |✅ Yes         |
| Centralized Authentication    | ❌ No                 |✅ Yes         |
| Centralized Logging	        | ❌ No                 |✅ Yes         |
# 🏗️ Microservices Architecture with Shared Functionality

This project demonstrates a **.NET microservices architecture** with a **shared library** for common functionality and a **Web API** for reusable services.

## 📂 Project Structure  

| Folder/Project        | Type                | Description |
|-----------------------|---------------------|-------------|
| `CommonLibrary`       | Class Library       | Contains shared logic, DTOs, extensions, and utilities. |
| `CommonServicesAPI`   | Web API             | Centralized API for authentication, logging, etc. |
| `OrderService`        | Web API (Microservice) | Handles order management. |
| `ProductService`      | Web API (Microservice) | Manages product-related operations. |
| `API Gateway`         | API Gateway (Ocelot) | Routes requests to appropriate services. |
| `docker-compose.yml`  | Docker Configuration | Orchestrates microservices deployment. |

### Ports
| Projects                  | Http | Https | Reason                         |
|---------------------------|------|-------|--------------------------------|
| GLOB.Apps                 |      |       | Interfaces |
| GLOB.Contracts            |      |       | None |
| GLOB.Domain               |      |       | Class, Dto, Entity, Enum, VM |
| GLOB.Package              |      |       | Common Package None |
| GLOB.API                  |      |       | Generic Controller, Mapper |
| GLOB.Infra                |      |       | DBContext, RepoGeneric, UOW_Projectz, Seed |
| SBA.APIGateway            | 1100 | 1101  | Api Gateway |
| SBA.Auth                  | 1104 | 1105  | Authentication, Authorization |
| SBA.Jobz                  | 1102 | 1103  | Schedular, Jobs, Crone |
| SBA.Hierarchy             | 1106 | 1107  | Simple Entities |
| SBA.Userz                 | 1108 | 1109  | Users (Standard, Business, Creator)|
| SBA.Order                 | 1110 | 1111  | Standard, Business, Custom |
| NONE                      | - | -  | - |

### CURL COMMAND
- Undermentioned Commands only works with Bash
- Before Using that you have to Stop Https Middleware
- Running your app with http in Visual Studio
```bash
curl -X 'POST' 'http://localhost:5294/auth/register' -H 'accept: */*' -H 'Content-Type: Apps/json' -d '{   "firstName": "string", "lastName": "string", "email": "string", "password": "string" }'
curl -X 'POST' 'http://localhost:5294/auth/login' -H 'accept: */*' -H 'Content-Type: Apps/json' -d '{ "email": "string", "password": "string" }'
curl -X 'GET' 'http://localhost:5294/api/Dinners' -H 'accept: */*' -H 'Authorization: Bearer token.full.goeshere'
```