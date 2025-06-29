# Task Management System

A clean architecture implementation of a Task Management System in .NET 9, designed for assessment purposes.

## ✅ Features
- Create, update, delete, and retrieve tasks
- Create, update, delete, and retrieve users
- Assign task to a user via endpoint
- Validation (due date must be in the future, email format)
- Logging using custom logger service
- SOLID-compliant & clean architecture
- RESTful API with Swagger UI
- Unit testing with xUnit & Moq

---

## 🧱 Project Structure
```
TaskManagementSystem/
├── Domain             # Entities & Interfaces
├── Application        # DTOs, Services, Validators
├── Infrastructure     # EF Core PostgreSQL Repos, DbContext
├── WebAPI             # REST Controllers & Program.cs
└── Tests              # xUnit test project
```

---

## 🚀 Getting Started

### 1. Clone Repository
```bash
git clone https://github.com/katsuragir/TaskManagementSystem.git
cd TaskManagementSystem
```

### 2. Run API
```bash
cd WebAPI
dotnet run
```

Then open in browser:
```
http://localhost:5215/swagger
```

---

## 📮 Sample Endpoints

| Method | Endpoint                            | Description             |
|--------|-------------------------------------|-------------------------|
| GET    | /api/tasks                          | Get all tasks           |
| GET    | /api/tasks/{id}                    | Get task by ID          |
| GET    | /api/users/{userId}/tasks          | Get tasks by assignee   |
| POST   | /api/tasks                          | Create a new task       |
| PUT    | /api/tasks/{id}                    | Update a task           |
| DELETE | /api/tasks/{id}                    | Delete a task           |
| PATCH  | /api/tasks/{taskId}/assign/{userId}| Assign task to a user   |
| GET    | /api/users                          | Get all users           |
| GET    | /api/users/{id}                    | Get user by ID          |
| POST   | /api/users                          | Create user             |
| PUT    | /api/users/{id}                    | Update user             |
| DELETE | /api/users/{id}                    | Delete user             |

---

## 🧪 Run Unit Tests
```bash
cd Tests
dotnet test
```

---

## 🔧 Tech Stack
- .NET 9 SDK
- C#
- xUnit + Moq
- FluentValidation
- PostgreSQL (via EF Core)
- Swagger (Swashbuckle)

---

## 📂 Notes
- Uses PostgreSQL (set up via EF Core migrations)
- Clean separation of concerns and SOLID principles

---

## 🧑 Author
Ridho Gumelar Bagaskara