
# Getting Started

## Prerequisites

Make sure the following tools are installed on your system:

- **.NET 8 SDK** – Required for local development or manual builds (optional with Docker)
- **Docker** – Recommended: Docker Desktop on Windows/macOS
- **Docker Compose** – Comes with Docker Desktop or can be installed separately
- **Visual Studio 2022** or **Visual Studio Code** – IDEs for coding, building, testing, and debugging

### Verify Installation

```bash
dotnet --version
docker --version
docker-compose --version
```

---

## Step 1: Environment Setup

This project is fully configured to run with Docker Compose.  
Just ensure the following ports are available on your machine:

- **8080 / 8081** → API
- **5432** → PostgreSQL

### Check for Port Availability

```bash
# Windows
netstat -an | findstr "8080 8081 5432"

# Linux / macOS
lsof -i :8080,8081,5432
```

---

## Step 2: Start the Services

Run the full environment using Docker Compose:

```bash
docker-compose up -d
```

---

## Step 3: Create the Database

Navigate to the `src` folder and apply the migrations:

```bash
dotnet ef database update   --project Ambev.DeveloperEvaluation.ORM   --startup-project Ambev.DeveloperEvaluation.WebApi
```

This will initialize the PostgreSQL schema.

---

## Step 4: Access the API

Once all services are running, open your browser and visit:

🔗 [http://localhost:8080/swagger](http://localhost:8080/swagger)

> The API is available via both **HTTP (8080)** and **HTTPS (8081)**.

---

## Shutting Down

To stop and remove all containers and volumes:

```bash
docker-compose down -v
```

This will also clean up the database and any persistent data.


