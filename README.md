# Todo Management API

A RESTful API for managing todo items, built with ASP.NET Core 8 Web API.

## Getting Started

1. **Clone the repository**
   ```bash
   git clone <your-repo-url>
   ```

2. **Configure the database**
   - Open `appsettings.json`
   - Update `ConnectionStrings:DefaultConnection` with your SQL Server connection string.

3. **Run database migrations**
   - Open *Package Manager Console* in Visual Studio
   - Run:
     ```
     Update-Database
     ```

4. **Start the API**
   - Run the project (`dotnet run` or via Visual Studio)

## Authentication Workflow

- **Signup**
  - `POST /api/auth/signup`
  - Fields:
    - `UserName` (required)
    - `Email` (required)
    - `Password` (min 6 characters)

- **Login**
  - `POST /api/auth/login`
  - Fields:
    - `Email` (or `UserName`)
    - `Password`
  - Response: JWT token

- **Authorize Requests**
  - Add JWT token to the `Authorization` header for protected endpoints:
    ```
    Authorization: Bearer <your-token>
    ```

## Notes

- All Todo APIs require a valid JWT token.
- Use correct `pageNumber` and `pageSize` for pagination when fetching lists.

## Project Features

✅ JWT Authentication - Secure API endpoints  
✅ Repository Pattern - Generic repository implementation  
✅ Entity Framework Core - Code-first approach with SQL Server  
✅ AutoMapper - For object mapping between DTOs and entities  
✅ Global Exception Handling - Custom middleware for error management  
