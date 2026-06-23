# APIProject - Yummy Restaurant API & Web UI

![.NET](https://img.shields.io/badge/.NET-6.0-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91?style=for-the-badge\&logo=dotnet\&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-Core-68217A?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=for-the-badge\&logo=microsoftsqlserver\&logoColor=white)

## Overview

**APIProject** is a restaurant management web application developed with **ASP.NET Core 6**.
The project is built with a layered structure that separates the **Web API** side and the **Web UI** side.

The application focuses on managing restaurant-related data such as categories, products, chefs, contact information, features, messages, reservations, services, images, and testimonials.

This project is suitable for learning and practicing modern ASP.NET Core development, RESTful API design, Entity Framework Core, SQL Server database operations, DTO usage, validation, and MVC-based web interfaces.

---

## Project Structure

```text
APIProject/
├── APIProje.WebApi/
│   ├── Controllers/
│   ├── Context/
│   ├── Dtos/
│   ├── Entities/
│   ├── Mapping/
│   ├── Migrations/
│   ├── ValidationRules/
│   ├── Program.cs
│   └── appsettings.json
│
├── APIProje.WebUI/
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   ├── ViewComponents/
│   ├── wwwroot/
│   ├── Program.cs
│   └── appsettings.json
│
└── APIProject.sln
```

---

## Technologies Used

* **ASP.NET Core 6**
* **ASP.NET Core Web API**
* **ASP.NET Core MVC**
* **Entity Framework Core**
* **Microsoft SQL Server**
* **AutoMapper**
* **FluentValidation**
* **Swagger / Swashbuckle**
* **Razor Views**
* **Bootstrap**
* **HTML / CSS / JavaScript**

---

## Features

### Web API Features

* RESTful API structure
* CRUD operations
* Entity Framework Core database operations
* SQL Server database integration
* DTO-based data transfer
* AutoMapper object mapping
* FluentValidation validation rules
* Swagger UI for API testing
* Product listing with category information
* Organized controller structure

### Web UI Features

* ASP.NET Core MVC architecture
* Razor View structure
* Shared layout usage
* ViewComponent support
* Static asset management with `wwwroot`
* Restaurant-themed user interface

---

## Main Modules

The project includes the following main modules:

* Categories
* Products
* Chefs
* Contacts
* Features
* Messages
* Reservations
* Services
* Images
* Testimonials

---

## Database

The project uses **SQL Server** with **Entity Framework Core**.

Default database name:

```text
ApiYummyDb
```

Database context file:

```text
APIProje.WebApi/Context/APIContext.cs
```

Example local SQL Server connection:

```csharp
Server=.\\SQLEXPRESS;Initial Catalog=ApiYummyDb;Integrated Security=True;TrustServerCertificate=True;
```

> Note: For a more professional production-ready structure, the connection string should be moved to `appsettings.json` or user secrets instead of being written directly inside the DbContext.

---

## API Endpoints

### Categories

| Method | Endpoint                              | Description            |
| ------ | ------------------------------------- | ---------------------- |
| GET    | `/api/Categories`                     | Lists all categories   |
| POST   | `/api/Categories`                     | Creates a new category |
| GET    | `/api/Categories/GetCategory?id={id}` | Gets a category by ID  |
| PUT    | `/api/Categories`                     | Updates a category     |
| DELETE | `/api/Categories?id={id}`             | Deletes a category     |

### Products

| Method | Endpoint                                  | Description                        |
| ------ | ----------------------------------------- | ---------------------------------- |
| GET    | `/api/Products`                           | Lists all products                 |
| POST   | `/api/Products`                           | Creates a new product              |
| GET    | `/api/Products/GetProduct?id={id}`        | Gets a product by ID               |
| PUT    | `/api/Products`                           | Updates a product                  |
| DELETE | `/api/Products?id={id}`                   | Deletes a product                  |
| POST   | `/api/Products/CreateProductWithCategory` | Creates a product with category    |
| GET    | `/api/Products/ProductListWithCategory`   | Lists products with category names |

### Chefs

| Method | Endpoint                     | Description        |
| ------ | ---------------------------- | ------------------ |
| GET    | `/api/Chefs`                 | Lists all chefs    |
| POST   | `/api/Chefs`                 | Creates a new chef |
| GET    | `/api/Chefs/GetChef?id={id}` | Gets a chef by ID  |
| PUT    | `/api/Chefs`                 | Updates a chef     |
| DELETE | `/api/Chefs?id={id}`         | Deletes a chef     |

### Contacts

| Method | Endpoint                           | Description                    |
| ------ | ---------------------------------- | ------------------------------ |
| GET    | `/api/Contacts`                    | Lists contact information      |
| POST   | `/api/Contacts`                    | Creates contact information    |
| GET    | `/api/Contacts/GetContact?id={id}` | Gets contact information by ID |
| PUT    | `/api/Contacts`                    | Updates contact information    |
| DELETE | `/api/Contacts?id={id}`            | Deletes contact information    |

### Features

| Method | Endpoint                           | Description           |
| ------ | ---------------------------------- | --------------------- |
| GET    | `/api/Features`                    | Lists all features    |
| POST   | `/api/Features`                    | Creates a new feature |
| GET    | `/api/Features/GetFeature?id={id}` | Gets a feature by ID  |
| PUT    | `/api/Features`                    | Updates a feature     |
| DELETE | `/api/Features?id={id}`            | Deletes a feature     |

---

## Getting Started

Follow these steps to run the project locally.

### Prerequisites

Make sure the following tools are installed on your computer:

* .NET 6 SDK
* Visual Studio 2022 or Visual Studio Code
* SQL Server or SQL Server Express
* SQL Server Management Studio
* Entity Framework Core CLI

Install EF Core CLI if it is not already installed:

```bash
dotnet tool install --global dotnet-ef
```

---

## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/Houzcetin/APIProject.git
```

### 2. Navigate to the Project Folder

```bash
cd APIProject
```

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Configure the Database Connection

Open the database context file:

```text
APIProje.WebApi/Context/APIContext.cs
```

Check the SQL Server connection string and update it according to your local SQL Server setup.

Example:

```csharp
Server=.\\SQLEXPRESS;Initial Catalog=ApiYummyDb;Integrated Security=True;TrustServerCertificate=True;
```

### 5. Apply Database Migrations

```bash
dotnet ef database update --project APIProje.WebApi
```

### 6. Run the Web API

```bash
dotnet run --project APIProje.WebApi
```

After running the API, Swagger can be opened from:

```text
https://localhost:7081/swagger
```

or

```text
http://localhost:5228/swagger
```

### 7. Run the Web UI

Open a second terminal and run:

```bash
dotnet run --project APIProje.WebUI
```

The Web UI can be opened from:

```text
https://localhost:7164
```

or

```text
http://localhost:5223
```

---

## Example API Requests

### Create Category

```http
POST /api/Categories
Content-Type: application/json
```

```json
{
  "categoryName": "Main Course"
}
```

### Create Product

```http
POST /api/Products
Content-Type: application/json
```

```json
{
  "productName": "Margherita Pizza",
  "description": "Classic pizza with tomato sauce, mozzarella cheese, and basil.",
  "price": 12.99,
  "imageUrl": "image-url-here",
  "categoryID": 1
}
```

### Create Chef

```http
POST /api/Chefs
Content-Type: application/json
```

```json
{
  "nameSurname": "Gordon Ramsay",
  "title": "Executive Chef",
  "description": "Experienced chef specialized in modern restaurant cuisine.",
  "imageURL": "image-url-here"
}
```

---

## Validation

The project uses **FluentValidation** for validating incoming data.

For example, product data can be checked before being saved to the database. This helps keep the API cleaner, safer, and more reliable.

---

## Object Mapping

The project uses **AutoMapper** to map entities and DTOs.

This helps avoid repeating manual property assignments and keeps controller code cleaner.

Example usage:

```csharp
CreateMap<Product, CreateProductDto>().ReverseMap();
```

---

## Swagger

Swagger is enabled for API testing and documentation.

When the Web API project is running, open:

```text
https://localhost:7081/swagger
```

Swagger allows you to test GET, POST, PUT, and DELETE requests directly from the browser.

---

## Future Improvements

Planned improvements for this project:

* Move connection string to `appsettings.json`
* Add authentication and authorization
* Add admin panel
* Add repository and service layers
* Add global exception handling
* Add response wrapper structure
* Add logging
* Add unit tests
* Add integration tests
* Add Docker support
* Add deployment documentation

---

## Author

**Oğuz Çetin**

GitHub: [Houzcetin](https://github.com/Houzcetin)

---

## License

This project does not currently include a license file.

If you plan to use this project publicly or professionally, consider adding a license such as MIT.
