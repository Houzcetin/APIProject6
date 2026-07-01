<h1 align="center">🍽️ APIProject6 — Yummy Restaurant API & Web UI</h1>

<p align="center">
  A full-stack restaurant management platform built with <b>ASP.NET Core 6</b>, featuring a RESTful Web API,
  an MVC front-end, and integrated <b>AI capabilities</b> (OpenAI &amp; Hugging Face) for content generation and message moderation.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-6.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 6" />
  <img src="https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core Web API" />
  <img src="https://img.shields.io/badge/Entity%20Framework-Core-68217A?style=for-the-badge" alt="EF Core" />
  <img src="https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/OpenAI-Integrated-412991?style=for-the-badge&logo=openai&logoColor=white" alt="OpenAI" />
  <img src="https://img.shields.io/badge/Hugging%20Face-Integrated-FFD21E?style=for-the-badge&logo=huggingface&logoColor=black" alt="Hugging Face" />
</p>

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Key Features](#-key-features)
- [AI &amp; Integrations](#-ai--integrations)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Modules](#-modules)
- [Getting Started](#-getting-started)
- [Configuration (API Keys &amp; Secrets)](#-configuration-api-keys--secrets)
- [Running the Applications](#-running-the-applications)
- [API Reference](#-api-reference)
- [Roadmap](#-roadmap)
- [Author](#-author)

---

## 🧭 Overview

**APIProject6** is a restaurant management web application composed of two independent ASP.NET Core 6 projects:

| Project | Role |
| ------- | ---- |
| **APIProject6.WebAPI** | RESTful Web API — data access, business rules, and persistence via Entity Framework Core + SQL Server. |
| **APIProject6.WebUI** | ASP.NET Core MVC front-end — a restaurant website plus an admin area that consumes the Web API over HTTP. |

Beyond standard CRUD, the platform adds **AI-powered features**: AI recipe generation, AI-assisted customer-support replies, and automatic **translation + toxicity moderation** of incoming contact messages.

It is an ideal reference for learning modern ASP.NET Core development, RESTful API design, EF Core, DTO/AutoMapper patterns, and integrating third-party AI services.

---

## ✨ Key Features

### Web API
- RESTful architecture with clean, resource-oriented controllers
- Full CRUD across all domain modules
- Entity Framework Core (Code-First) with SQL Server
- DTO-based data transfer with **AutoMapper**
- **FluentValidation** for request validation
- Interactive **Swagger / OpenAPI** documentation

### Web UI
- ASP.NET Core **MVC** with Razor Views and reusable **ViewComponents**
- Public restaurant website (hero, menu, chefs, gallery, events, testimonials, contact)
- Admin area for managing content
- Consumes the Web API via `IHttpClientFactory`
- Bootstrap-based responsive UI with static assets under `wwwroot`

### AI-Powered
- 🤖 **AI Recipe Generator** — turns ingredients into a formatted recipe (OpenAI)
- 💬 **AI Support Replies** — drafts polite, on-brand answers to customer messages (OpenAI)
- 🌐 **Auto-Translation** — translates incoming messages (TR → EN) via Hugging Face
- 🛡️ **Toxicity Moderation** — flags harmful messages using a Hugging Face classifier and tags them with a status

---

## 🧠 AI & Integrations

| Capability | Provider | Model / Endpoint | Where |
| ---------- | -------- | ---------------- | ----- |
| Recipe generation | OpenAI | Chat Completions API | `AIController.CreateRecipeWithOpenAI` |
| Support-reply drafting | OpenAI | Chat Completions API | `MessageController.AnswerMessageWithOpenAI` |
| Message translation (TR → EN) | Hugging Face | `Helsinki-NLP/opus-mt-tr-en` | `MessageController.SendMessage` |
| Toxicity classification | Hugging Face | `unitary/toxic-bert` | `MessageController.SendMessage` |

> **Note:** Hugging Face requests use the current Inference Providers router
> (`https://router.huggingface.co/hf-inference/models/...`). The legacy
> `api-inference.huggingface.co` host has been retired.

All AI keys are supplied through configuration and **never committed to source control** — see [Configuration](#-configuration-api-keys--secrets).

---

## 🏗️ Architecture

```text
APIProject6/
├── APIProject6.WebAPI/          # RESTful Web API
│   ├── Controllers/             # Categories, Products, Chefs, Messages, Reservations, Images, ...
│   ├── Context/                 # APIContext (EF Core DbContext)
│   ├── Dtos/                    # Data Transfer Objects
│   ├── Entities/                # Domain entities
│   ├── Mapping/                 # AutoMapper profiles
│   ├── Migrations/              # EF Core migrations
│   ├── ValidationRules/         # FluentValidation rules
│   └── Program.cs
│
├── APIProject6.WebUI/           # ASP.NET Core MVC front-end
│   ├── Controllers/             # Default (website), Admin, AI, Message, Gallery, ...
│   ├── Views/                   # Razor views
│   ├── ViewComponents/          # Reusable UI components
│   ├── Dtos/                    # Client-side DTOs
│   ├── wwwroot/                 # Static assets (CSS, JS, images)
│   └── Program.cs
│
└── APIProject6.sln
```

**Request flow:** `Browser → WebUI (MVC) → HttpClient → WebAPI → EF Core → SQL Server`,
with the WebUI also calling **OpenAI** and **Hugging Face** directly for AI features.

---

## 🧰 Tech Stack

- **ASP.NET Core 6** — Web API & MVC
- **Entity Framework Core** (Code-First) + **Microsoft SQL Server**
- **AutoMapper** · **FluentValidation**
- **Swagger / Swashbuckle**
- **OpenAI API** · **Hugging Face Inference API**
- **Razor Views** · **Bootstrap** · HTML / CSS / JavaScript

---

## 🧩 Modules

Categories · Products · Chefs · Contacts · Features · Messages · Reservations · Services · Images (Gallery) · Testimonials · Events · Notifications · Abouts

---

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Visual Studio 2022 or VS Code
- SQL Server or SQL Server Express
- EF Core CLI:
  ```bash
  dotnet tool install --global dotnet-ef
  ```
- *(Optional, for AI features)* An **OpenAI API key** and a **Hugging Face API token**

### 1. Clone the repository

```bash
git clone https://github.com/Houzcetin/APIProject6.git
cd APIProject6
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Configure the database

The EF Core `DbContext` lives in `APIProject6.WebAPI/Context/APIContext.cs` and uses a local SQL Server:

```csharp
Server=.\SQLEXPRESS;Initial Catalog=APIYummyDb6;Integrated Security=True;
```

Adjust it to match your environment (add `TrustServerCertificate=True;` if your setup requires it).

### 4. Apply migrations

```bash
dotnet ef database update --project APIProject6.WebAPI
```

---

## 🔐 Configuration (API Keys & Secrets)

AI features read their credentials from configuration keys `OpenAI:ApiKey` and `HuggingFace:ApiKey`.
**Never hard-code these into `appsettings.json` in a public repository.** Use .NET User Secrets:

```bash
cd APIProject6.WebUI

dotnet user-secrets init
dotnet user-secrets set "OpenAI:ApiKey"      "sk-your-openai-key"
dotnet user-secrets set "HuggingFace:ApiKey" "hf_your-huggingface-token"
```

Secrets are stored outside the repository (in your user profile) and are automatically loaded in the `Development` environment.

---

## ▶️ Running the Applications

Run the **Web API** and **Web UI** in two separate terminals.

**Terminal 1 — Web API**
```bash
dotnet run --project APIProject6.WebAPI
```
| URL | |
| --- | --- |
| Swagger | `https://localhost:7277/swagger` |
| HTTPS | `https://localhost:7277` |
| HTTP | `http://localhost:5027` |

**Terminal 2 — Web UI**
```bash
dotnet run --project APIProject6.WebUI
```
| URL | |
| --- | --- |
| Website | `https://localhost:7208` |
| HTTP | `http://localhost:5226` |

> The Web UI expects the Web API to be running at `https://localhost:7277`.

---

## 📡 API Reference

All modules follow the same RESTful convention. Using **Categories** as the representative example:

| Method | Endpoint | Description |
| ------ | -------- | ----------- |
| `GET`  | `/api/Categories` | List all categories |
| `POST` | `/api/Categories` | Create a category |
| `GET`  | `/api/Categories/GetCategory?id={id}` | Get a category by ID |
| `PUT`  | `/api/Categories` | Update a category |
| `DELETE` | `/api/Categories?id={id}` | Delete a category |

The same pattern applies to `Products`, `Chefs`, `Contacts`, `Features`, `Messages`, `Reservations`, `Images`, `Services`, `Testimonials`, `YummyEvents`, `Notifications`, and `Abouts`. A few modules expose extra actions, e.g.:

| Method | Endpoint | Description |
| ------ | -------- | ----------- |
| `POST` | `/api/Products/CreateProductWithCategory` | Create a product with its category |
| `GET`  | `/api/Products/ProductListWithCategory` | List products including category names |

**Example — create a product**

```http
POST /api/Products
Content-Type: application/json
```
```json
{
  "productName": "Margherita Pizza",
  "description": "Classic pizza with tomato sauce, mozzarella, and basil.",
  "price": 12.99,
  "imageUrl": "image-url-here",
  "categoryID": 1
}
```

> 💡 Explore and test every endpoint interactively via **Swagger UI** at `https://localhost:7277/swagger`.

---

## 🗺️ Roadmap

- [ ] Move the connection string from `APIContext` to `appsettings.json` / secrets
- [ ] Authentication & authorization (Identity / JWT)
- [ ] Repository & service layers
- [ ] Global exception handling & response wrapper
- [ ] Structured logging (Serilog)
- [ ] Unit & integration tests
- [ ] Docker support & deployment docs

---

## 👤 Author

**Oğuz Çetin**
GitHub: [@Houzcetin](https://github.com/Houzcetin)

---

<p align="center"><i>Built with ASP.NET Core 6 · Powered by OpenAI &amp; Hugging Face</i></p>
