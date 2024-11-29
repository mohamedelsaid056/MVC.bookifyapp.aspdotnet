
# 📚 Bookify - Advanced Book Rental Management System

<div align="center">


[![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-purple.svg)](https://dotnet.microsoft.com/download)
[![EF Core](https://img.shields.io/badge/EF%20Core-8.0-blue.svg)](https://docs.microsoft.com/ef/core)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)](https://github.com/mohamedelsaid056/MVC.bookifyapp.aspdotnet)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://github.com/yourusername/bookify/graphs/commit-activity)

![Bookify Logo](https://github.com/user-attachments/assets/3184d889-057b-4257-8a5d-1e452d6356b5)

</div>

```mermaid
graph TD
    A[Client] -->|HTTP| B(Web Application)
    B --> C{Authentication}
    C -->|Admin| D[Book Management ,Rental Management]
    C -->|Admin| E[Rental Management]
    C -->|User| F[View Books]
    C -->|User| G[Rent Books]
    B --> H[Hangfire Dashboard]
    H -->|Scheduled Jobs| I[Background Tasks]
    I --> J[Email Notifications]
    I --> K[WhatsApp Notifications]
    B --> L[Serilog Logging]
    M[(Database SQL)] --> B
    M --> H
    N[Role Management] --> C
```





## 📋 Overview

Bookify is an enterprise-grade book rental management system designed to streamline library operations and enhance user experience. The system provides comprehensive solutions for book inventory management, subscriber handling, rental processing, and administrative operations.

### Key Benefits
- 🎯 Streamlined rental operations
- 📊 Advanced reporting and analytics
- 🔐 Secure user authentication
- 📱 Modern, responsive interface
- 🔄 Real-time updates
- 📈 Scalable architecture

## 🏗 System Architecture


Bookify is a robust ASP.NET Core web application designed for managing book rentals. It features user authentication, book management, and an automated rental system. With integrated Hangfire for background jobs, Serilog for logging, and support for WhatsApp and email notifications, Bookify offers a comprehensive solution for libraries or book rental services.

## 📚 Core Features

### Book Management
- 📖 Inventory tracking
- 🏷️ Categorization
- 📸 Image management
- 📊 Stock monitoring

### Rental System
- 🔄 Rental processing
- ⏰ Due date management
- 💰 Fine calculation
- 📱 SMS/WhatsApp notifications

### User Management
- 👥 Role-based access
- 🔐 Secure authentication
- 📧 Email verification
- 👤 Profile management

## 🔧 Technology Stack

- **Backend Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity
- **Frontend**: 
  - Bootstrap
  - jQuery
  - javascript for Ajax calls 
  - DataTables "both client and server"
- **File Storage**: Cloudinary ,
- **Background Jobs**: Hangfire
- **Mapping**: AutoMapper, manual mapping
-  **WhatsApp API** (via IWhatsAppClient)


## 🔐 Security

### Authentication
- ASP.NET Core Identity
- Custom claim providers
- **User Management**: Complete user lifecycle management
- **Password Hashing**: Secure password storage using industry-standard hashing
- **Account Confirmation**: Email verification system
- **Password Recovery**: Secure password reset functionality
- 

### Authorization
#### 1. Role-Based Access Control (RBAC)


```csharp
public static class AppRoles
{
    public const string Admin = "Admin";
    public const string Reception = "Reception";
    public const string Archive = "Archive";
    public const string User = "User";
}

// Controller-level authorization
[Authorize(Roles = AppRoles.Reception)]
public class RentalsController : Controller
{
    // Only the Reception role can access this controller
}

// Action-level authorization
[Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Archive}")]
public IActionResult ManageBooks()
{
    // Only Admin and Archive roles can access this action
}
```


####  Data Protection
using Data protection that built in library .NET 
using extra package "hashids"


#### Security Best Practices
- **HTTPS Enforcement**: All communications are encrypted using SSL/TLS
- **Anti-forgery Tokens**: Protection against CSRF attacks
- **XSS Prevention**: Content security policies and input sanitization
- **SQL Injection Prevention**: Use of parameterized queries and EF Core
- **Secure Headers**: Implementation of security headers (HSTS, X-Frame-Options, etc.)
- **Audit Logging**: Tracking of security-relevant events





## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 (recommended) or VS Code

### Installation

1. Clone the repository
   ```
   git clone https://github.com/mohamedelsaid056/MVC.bookifyapp.aspdotnet
   ```

2. Navigate to the project directory
   ```
   cd Bookify
   ```

3. Restore dependencies
   ```
   dotnet restore
   ```

4. Update the connection string in `appsettings.json` to point to your database
 ```
   json
{
"CloudinarySettings": {
"Cloud": "your_cloud_name",
"ApiKey": "your_api_key",
"ApiSecret": "your_api_secret"
},
"MailSettings": {
// Configure your email settings
}
}
 ```

6. Apply migrations to create the database
   ```
   dotnet ef database update
   ```

7. Run the application
   ```
   dotnet run
   ```

## Configuration

- The application uses `appsettings.json` for configuration. Make sure to update any necessary settings, such as connection strings, API keys, etc.
- Serilog is configured to read from the configuration file.
- Hangfire dashboard is accessible at `/hangfire` and is restricted to admin users only.



## Glimpse of the working solution

**Demo Link**: -http://mohamedbookifyapp1.runasp.net/

**username** : admin@bookify.com

**password** : P@ssword123


![1](https://github.com/user-attachments/assets/18dbb497-25b7-412c-b905-1c126afcba39)

![2](https://github.com/user-attachments/assets/15d61640-9e67-4beb-945e-806dc0af4350)

![3](https://github.com/user-attachments/assets/135fd1b2-b842-4f8d-b07e-3cc33bc70bc0)

![user](https://github.com/user-attachments/assets/0fc4c671-66fa-427c-98ed-eb2de401fc9e)

![search](https://github.com/user-attachments/assets/99271bbb-04ca-4778-8098-71ee3c243f11)


![report ](https://github.com/user-attachments/assets/15528aa4-959c-4e31-8a8a-7e658e2826e2)


<div align="center">
Made with ❤️ by the Bookify Team
</div>


