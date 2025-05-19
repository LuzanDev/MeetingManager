# 📅 MeetingManager API

API для бронирования комнат на встречи. Реализовано с использованием ASP.NET 8, Entity Framework Core, MSSQL и Swagger.

## ⚙️ Стек технологий

- ASP.NET Core Web API
- Entity Framework Core
- MS SQL Server (LocalDB или обычный)
- Swagger (Swashbuckle)
- JWT-аутентификация

---

## 🚀 Запуск проекта

### 1. Клонируй репозиторий

```bash
git clone https://github.com/LuzanDev/MeetingManager.git
cd MeetingManager
```
### 2. Создай appsettings.Development.json
В корне проекта создай файл appsettings.Development.json со следующим содержимым:
```bash
{
  "ConnectionStrings": {
    "MSSQL": "Server=localhost;Database=MeetingManagerDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True"
  },
  "JwtSettings": {
    "Key": "ezWVzQwGBOrM/q8EvG/g8vRHj50xn1e6+Rm2GJhVHNk=",
    "Issuer": "MeetingManager",
    "Audience": "MeetingManagerAudience",
    "ExpiresInMinutes": 60
  }
}
```
🔐 Либо добавь эти значения в User Secrets командой:
```bash
dotnet user-secrets set "ConnectionStrings:MSSQL" "Server=localhost;Database=MeetingManagerDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True"
dotnet user-secrets set "JwtSettings:Key" "ezWVzQwGBOrM/q8EvG/g8vRHj50xn1e6+Rm2GJhVHNk="
dotnet user-secrets set "JwtSettings:Issuer" "MeetingManager"
dotnet user-secrets set "JwtSettings:Audience" "MeetingManagerAudience"
dotnet user-secrets set "JwtSettings:ExpiresInMinutes" "60"
```
### 3. Применение миграций и запуск
1. Открой терминал в каталоге проекта и выполни:
```bash
dotnet ef database update
```
2. Запусти проект:
```bash
dotnet run
```
## 📘 Swagger

После запуска API будет доступен по адресу:
https://localhost:<порт>/swagger/index.html
```bash
Порт (`<порт>`) будет выведен в консоли при запуске приложения, например:
```
Now listening on: https://localhost:7196
```bash
➡ Скопируй этот адрес и добавь `/swagger/index.html` в конце, чтобы открыть Swagger UI.
```
## 🛡 Аутентификация
API использует JWT. После регистрации и логина ты получишь токен, который нужно использовать в Swagger:
1. Получи токен через /api/auth/login
2. Нажми Authorize в Swagger и введи:
``` bash
Bearer <твой_токен>
```
## 🖼 Примеры интерфейса Swagger

### 🔐 Авторизация

![Авторизация](docs/images/swagger_auth.png)

### 🔐 Список реализованных запросов

![Список реализованных запросов](docs/images/list-ends.png)

---

### 📝 Создание бронирования

**POST** `/api/bookings`

```json
Запрос:
{
  "roomId": 3,
   "startTime": "2025-05-22T10:00",
   "endTime": "2025-05-22T11:30",
   "bookedBy": "Sokolenko Roman"
}
Ответ:
![Создание бронирования](docs/images/create_booking.png)

---

### 📝 Выполнение тестов

![Выполнение тестов](docs/images/tests.png)

---
### 📝 Получение комнат

![Получение комнат](docs/images/rooms.png)




 