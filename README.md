# Bookify

Bookify is a web application built with ASP.NET Core that manages book rentals and user accounts.

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

- .NET 6.0 SDK or later
- SQL Server (or your preferred database supported by EF Core)

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

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgments

- [Hangfire](https://www.hangfire.io/) for background job processing
- [Serilog](https://serilog.net/) for structured logging