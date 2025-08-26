# GEMINI Project Context: Student Classwork Portal

## Project Overview

This is a full-stack web application, the "Student Classwork Portal," built with .NET 9 and ASP.NET Core Razor Pages. It
serves as a platform for a teacher to manage assignments and for students to submit their work. The application uses
Entity Framework Core with a SQLite database for data persistence and ASP.NET Core Identity for authentication and
role-based access control (Teachers and Students).

The frontend is styled using Tailwind CSS. All frontend JavaScript libraries (jQuery, PrismJS, etc.) are managed via
`pnpm` and copied to the `wwwroot/lib` directory to ensure the application is fully functional in an offline/LAN
environment.

Key functionalities include user management (including bulk student import), assignment creation, resource sharing, and
submission tracking.

## Building and Running

### Prerequisites

- .NET 9 SDK
- Node.js
- pnpm

### Commands

1. **Install Dependencies:**
   This command installs both backend (.NET) and frontend (Node.js) dependencies. It will also automatically trigger
   a `postinstall` script that copies the necessary frontend library files from `node_modules` into the `wwwroot/lib`
   directory.

   ```sh
   pnpm install
   dotnet restore
   ```

2. **Build CSS:**
   This command compiles the Tailwind CSS.

   ```sh
   pnpm run css:build
   ```

3. **Apply Database Migrations:**
   This will create and update the `app.db` SQLite database file.

   ```sh
   dotnet ef database update
   ```

4. **Build and Run the Application:**

   ```sh
   dotnet build
   dotnet run
   ```

   Note that the `dotnet run` command will occupy the terminal until the application is stopped. Do not run it directly
   inside the Gemini CLI. Instead, run `dotnet build` to ensure the project compiles successfully.

## Development Conventions

- **Backend**: The application follows the standard ASP.NET Core Razor Pages structure. Business logic is contained
  within the PageModels (`.cshtml.cs` files). Data models are defined in the `Models` and `Areas/Identity/Data`
  directories.
- **Database**: Database schema changes are managed through Entity Framework Core migrations. The
  `Data/ApplicationDbContext.cs` file is the primary point of configuration for the database context.
- **Frontend**: All styling is done via Tailwind CSS. The main stylesheet is `Styles/app.css`, which is compiled to
  `wwwroot/css/app.css`. All third-party JavaScript libraries are managed via `pnpm` and served locally from the
  `wwwroot/lib` directory. Custom application scripts are located in `wwwroot/js/site.js`.
- **Configuration**: Application settings are managed in `appsettings.json`. For development, sensitive information and
  the initial teacher credentials are managed in `appsettings.Development.json`.
- **User Roles**: The application uses a two-role system: "Teacher" (administrator) and "Student". The teacher account
  is seeded on the first run, and the teacher can then create student accounts.
