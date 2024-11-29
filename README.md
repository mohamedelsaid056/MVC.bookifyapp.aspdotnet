
# 📚 Bookify - Advanced Book Rental Management System

<div align="center">

![Bookify Logo](https://github.com/user-attachments/assets/cd01dc09-1f43-4dd3-a2ac-95b7cc745eaf)

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET Core](https://img.shields.io/badge/.NET%20Core-7.0-purple.svg)](https://dotnet.microsoft.com/download)
[![EF Core](https://img.shields.io/badge/EF%20Core-7.0-blue.svg)](https://docs.microsoft.com/ef/core)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)](https://github.com/yourusername/bookify)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://github.com/yourusername/bookify/graphs/commit-activity)

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



## 📑 Table of Contents
- [Overview](#-overview)
- [System Architecture](#-system-architecture)
- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Setup Instructions](#-setup-instructions)
- [Project Structure](#-project-structure)
- [Security](#-security)
- [API Documentation](#-api-documentation)
- [Contributing](#-contributing)
- [Deployment](#-deployment)
- [Troubleshooting](#-troubleshooting)
- [License](#-license)
- [Support](#-support)

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

## Features

- User authentication and authorization using native identity pages 
- Book management
- Rental system
- Automated tasks using Hangfire (background jobs)
- Logging with Serilog
- WhatsApp integration
- Email notifications

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- Identity Framework
- Hangfire
- Serilog
- WhatsApp API (via IWhatsAppClient)

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server 

### Installation

1. Clone the repository
   ```
   git clone https://github.com/yourusername/Bookify.git
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

5. Apply migrations to create the database
   ```
   dotnet ef database update
   ```

6. Run the application
   ```
   dotnet run
   ```

## Configuration

- The application uses `appsettings.json` for configuration. Make sure to update any necessary settings, such as connection strings, API keys, etc.
- Serilog is configured to read from the configuration file.
- Hangfire dashboard is accessible at `/hangfire` and is restricted to admin users only.

## Scheduled Tasks

The application uses Hangfire to run the following recurring jobs:

- `PrepareExpirationAlert`: Runs daily at 2 PM
- `RentalsExpirationAlert`: Runs daily at 2 PM

## Security

- HTTPS is enforced in production
- X-Frame-Options header is set to "Deny"
- Custom authorization is implemented for the Hangfire dashboard

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests.



This project is licensed under the [MIT License](LICENSE).



## Acknowledgments

- [Hangfire](https://www.hangfire.io/) for background job processing
- [Serilog](https://serilog.net/) for structured logging


