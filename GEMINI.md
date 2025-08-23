# GEMINI Project Context: Student Classwork Portal

## Project Overview

This is a full-stack web application, the "Student Classwork Portal," built with .NET 9 and ASP.NET Core Razor Pages. It serves as a platform for a teacher to manage assignments and for students to submit their work. The application uses Entity Framework Core with a SQLite database for data persistence and ASP.NET Core Identity for authentication and role-based access control (Teachers and Students).

The frontend is styled using Tailwind CSS, which is compiled into a static asset using `pnpm`. The project is configured for deployment on a Windows IIS server.

Key functionalities include user management (including bulk student import), assignment creation, resource sharing, and submission tracking.

## Building and Running

### Prerequisites

- .NET 9 SDK
- Node.js
- pnpm

### Commands

1.  **Install Frontend Dependencies:**
    ```sh
    pnpm install
    ```

2.  **Build CSS:**
    This command compiles the Tailwind CSS.
    ```sh
    pnpm run css:build
    ```

3.  **Restore .NET Dependencies:**
    ```sh
    dotnet restore
    ```

4.  **Apply Database Migrations:**
    This will create and update the `app.db` SQLite database file.
    ```sh
    dotnet ef database update
    ```

5.  **Build the Application:**
    ```sh
    dotnet build
    ```

    To run the application, use `dotnet run`. Note that this command will occupy the terminal until the application is stopped.

## Development Conventions

- **Backend**: The application follows the standard ASP.NET Core Razor Pages structure. Business logic is contained within the PageModels (`.cshtml.cs` files). Data models are defined in the `Models` and `Areas/Identity/Data` directories.
- **Database**: Database schema changes are managed through Entity Framework Core migrations. The `Data/ApplicationDbContext.cs` file is the primary point of configuration for the database context.
- **Frontend**: All styling is done via Tailwind CSS. The main stylesheet is `Styles/app.css`, which is compiled to `wwwroot/css/app.css`.
- **Configuration**: Application settings, including the initial teacher credentials for development, are managed in `appsettings.Development.json`.
- **User Roles**: The application uses a two-role system: "Teacher" (administrator) and "Student". The teacher account is seeded on the first run, and the teacher can then create student accounts.
