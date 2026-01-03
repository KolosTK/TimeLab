## Local Database Setup

This project uses a **PostgreSQL database** for development. To configure your local environment:

### 1. Create `appsettings.Development.json`

This file **should NOT be committed to Git**. It stores your **local database credentials**.


```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=timelab_db;Username=postgres;Password=YOUR_PASSWORD"
  }
}
