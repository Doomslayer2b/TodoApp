# TodoApp API

A RESTful API for managing todos and categories, built with .NET 8 and Entity Framework Core.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoft-sql-server&logoColor=white)

## 🚀 Features

- ✅ Full CRUD operations for Todos
- ✅ Category management with 1:N relationships
- ✅ DTO validation with Data Annotations ([Required], [MaxLength], [Range])
- ✅ Error handling and proper HTTP status codes
- ✅ Swagger/OpenAPI documentation
- ✅ SQL Server database with EF Core migrations

## 🛠️ Tech Stack

- **.NET 8** - Web API Framework
- **Entity Framework Core** - ORM
- **SQL Server** - Database
- **Swagger/OpenAPI** - Interactive API Documentation
- **C#** - Programming Language

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or LocalDB)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Optional: [Postman](https://www.postman.com/) for API testing

## 🏃 Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/Doomslayer2b/TodoApp.git
cd TodoApp
```

### 2. Configure the database connection

Open `appsettings.json` and update the connection string if needed:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TodoAppDb;Trusted_Connection=true;"
  }
}
```

### 3. Apply database migrations
```bash
dotnet ef database update
```

### 4. Run the application
```bash
dotnet run
```

The API will start at `http://localhost:5000`

### 5. Explore the API

Open your browser and navigate to:
```
http://localhost:5000/swagger
```

## 📚 API Endpoints

### Todos

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| GET | `/api/todos` | Get all todos | - |
| GET | `/api/todos/{id}` | Get a specific todo | - |
| POST | `/api/todos` | Create a new todo | `CreateTodoDto` |
| PUT | `/api/todos/{id}` | Update an existing todo | `UpdateTodoDto` |
| DELETE | `/api/todos/{id}` | Delete a todo | - |

### Categories

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| GET | `/api/categories` | Get all categories | - |
| GET | `/api/categories/{id}` | Get a specific category | - |
| POST | `/api/categories` | Create a new category | `CreateCategoryDto` |
| PUT | `/api/categories/{id}` | Update a category | `UpdateCategoryDto` |
| DELETE | `/api/categories/{id}` | Delete a category | - |

## 📊 Database Schema

### Todo

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | int | Primary Key | Unique identifier |
| Title | nvarchar(200) | Required | Todo title |
| Description | nvarchar(1000) | Optional | Detailed description |
| IsCompleted | bit | Required | Completion status |
| CreatedAt | datetime2 | Required | Creation timestamp |
| CategoryId | int | Foreign Key | Reference to Category |

### Category

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | int | Primary Key | Unique identifier |
| Name | nvarchar(100) | Required | Category name |

**Relationship:** One Category can have many Todos (1:N)

## 📝 Example Requests

### Create a Todo

**Request:**
```http
POST /api/todos
Content-Type: application/json

{
  "title": "Complete .NET project",
  "description": "Build a full-stack todo API",
  "categoryId": 1
}
```

**Response (201 Created):**
```json
{
  "id": 1,
  "title": "Complete .NET project",
  "description": "Build a full-stack todo API",
  "isCompleted": false,
  "createdAt": "2026-01-08T10:30:00Z",
  "categoryId": 1,
  "categoryName": "Work"
}
```

### Get All Todos

**Request:**
```http
GET /api/todos
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "title": "Complete .NET project",
    "description": "Build a full-stack todo API",
    "isCompleted": false,
    "createdAt": "2026-01-08T10:30:00Z",
    "categoryId": 1,
    "categoryName": "Work"
  },
  {
    "id": 2,
    "title": "Buy groceries",
    "description": "Milk, eggs, bread",
    "isCompleted": false,
    "createdAt": "2026-01-08T11:00:00Z",
    "categoryId": 2,
    "categoryName": "Personal"
  }
]
```

### Update a Todo

**Request:**
```http
PUT /api/todos/1
Content-Type: application/json

{
  "title": "Complete .NET project",
  "description": "Build a full-stack todo API with React frontend",
  "isCompleted": true,
  "categoryId": 1
}
```

**Response (200 OK):**
```json
{
  "id": 1,
  "title": "Complete .NET project",
  "description": "Build a full-stack todo API with React frontend",
  "isCompleted": true,
  "createdAt": "2026-01-08T10:30:00Z",
  "categoryId": 1,
  "categoryName": "Work"
}
```

## 🧪 Testing the API

### Using Swagger (Recommended)
1. Run the application
2. Navigate to `http://localhost:5000/swagger`
3. Try out the endpoints directly in the browser

### Using Postman
1. Import the collection (optional)
2. Set base URL: `http://localhost:5000`
3. Test each endpoint

### Using curl
```bash
# Get all todos
curl -X GET "http://localhost:5000/api/todos"

# Create a todo
curl -X POST "http://localhost:5000/api/todos" \
  -H "Content-Type: application/json" \
  -d '{"title":"Test Todo","categoryId":1}'
```

## 🎓 What I Learned

Building this project helped me learn:
- RESTful API design principles
- Entity Framework Core migrations and relationships
- Data validation with DTOs
- Proper error handling and HTTP status codes
- API documentation with Swagger
- SQL Server database design

## 🔮 Future Enhancements

- [ ] Add JWT authentication and authorization
- [ ] Implement user accounts and multi-tenancy
- [ ] Add due dates and reminders
- [ ] Add search and filtering capabilities
- [ ] Add pagination for large datasets
- [ ] Implement sorting by multiple fields
- [ ] Add tags/labels for todos
- [ ] Build a React frontend
- [ ] Deploy to Azure/AWS

## 👨‍💻 Author

**Andres S. Nieves Fonseca**

Information Systems Student | Aspiring Software Developer

- 📧 Email: Andynievespr@gmail.com
- 💼 LinkedIn: [Connect with me](https://www.linkedin.com/in/andr%C3%A9s-nieves-596ba7212 )
- 🐙 GitHub: [@Doomslayer2b](https://github.com/Doomslayer2b)
- 📍 Location: Caguas, Puerto Rico

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 🙏 Acknowledgments

- Built as part of my software development portfolio
- Thanks to the .NET community for excellent documentation
- Special thanks to Microsoft Learn for comprehensive tutorials

---

⭐ If you found this project helpful, please consider giving it a star!

**Last Updated:** January 2026
