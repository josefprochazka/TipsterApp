# TipsterApp

A tip and feedback tracking app for cafés.  
Users can submit or update a tip for their table, rate the service, and view live statistics. Built with modern Blazor (.NET 8), featuring REST API, background services, and flexible data storage.

---

## Features

- Add or update tip for a specific **Table ID**
- Choose from predefined tip percentages or enter a **custom amount**
- **Input validation** (email, rating, amounts)
- Show **highest tip of the day** and **total cost**
- **Statistics page**:
  - Total amount of tips
  - Highest tip of the day (with percent and timestamp)
  - Average tip percentage
  - Average rating
- **REST API** with Swagger docs (secured with Bearer token)
- **Pluggable storage** – in-memory or JSON file (configurable)
- **Background service** to refresh statistics every 10 seconds

---

## How to run the app

1. Open `TipsterApp.sln` in Visual Studio
2. Press **F5** or start the `TipsterApp` project manually

---

## Switch storage type

Edit the `appsettings.json` and set the following:

```json
"StorageType": "InMemory"


## Autor

Josef Procházka  
prochazka@email.cz
