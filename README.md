# Task Management System

A clean architecture implementation of a Task Management System in .NET 9, designed for assessment purposes.

## ✅ Features
- Create, update, delete, and retrieve tasks
- Assign task to a user
- Validation (due date must be in the future)
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
├── Infrastructure     # In-memory DB & Logging
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
https://localhost:5001/swagger
```

---

## 📮 Sample Endpoints

| Method | Endpoint                    | Description             |
|--------|-----------------------------|-------------------------|
| GET    | /api/tasks                  | Get all tasks           |
| GET    | /api/tasks/{id}            | Get task by ID          |
| GET    | /api/users/{userId}/tasks  | Get tasks by assignee   |
| POST   | /api/tasks                 | Create a new task       |
| PUT    | /api/tasks/{id}           | Update a task           |
| DELETE | /api/tasks/{id}           | Delete a task           |

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
- Swagger (Swashbuckle)

---

## 📂 Notes
- Uses in-memory List<TaskItem> for simplicity
- Focus on clean separation of concerns
- No actual database needed

---

## 🧑 Author
Ridho Gumelar Bagaskara
