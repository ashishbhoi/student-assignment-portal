# GEMINI Project Context: Student Classwork Portal

## 1. Project Overview

The Student Classwork Portal is a centralized web application designed for educational environments. It allows students to submit classwork and teachers to manage assignments and review submissions. The application is built to be fully self-contained (offline-first), secure, and deployable on Windows IIS.

## 2. Architecture & Tech Stack

- **Backend Framework**: .NET 9, ASP.NET Core Razor Pages.
- **Database**: Entity Framework Core with SQLite (`app.db`).
- **Authentication**: ASP.NET Core Identity with two distinct roles: `Student` and `Teacher`.
- **Frontend**:
  - **Styling**: Tailwind CSS (adhering to **Liquid Glass Design** principles: glassmorphism, gradients, translucency).
  - **Scripting**: Vanilla JavaScript, jQuery.
  - **Dependencies**: Managed via `pnpm` and served locally from `wwwroot/lib` (including jQuery, Prism.js, etc.) to ensure full offline functionality.
- **Deployment Target**: Windows Server IIS (requires .NET 9 Hosting Bundle, "No Managed Code" App Pool).

## 3. Key Files & Directories

- `Program.cs`: Application entry point; configures services, middleware, and authentication.
- `Data/ApplicationDbContext.cs`: The EF Core DbContext defining the database schema.
- `Data/SeedData.cs`: Logic for seeding the initial Teacher account.
- `Models/`: Core domain models, primarily:
  - `UserFile`: Represents a file submitted by a student.
  - `VirtualFolder`: Represents an assignment or category.
- `Areas/Identity/`: Identity-related pages (Login, Register, Manage) and models (`ApplicationUser`).
- `Pages/`: Razor Pages (`.cshtml`) and their PageModels (`.cshtml.cs`) containing UI and business logic.
- `wwwroot/`: Static assets.
  - `lib/`: Vendor scripts/styles copied by the `postinstall` script.
  - `js/site.js`: Custom application logic.
- `Styles/app.css`: Source Tailwind CSS file.
- `package.json`: Defines frontend dependencies and scripts (build, watch, postinstall).
- `appsettings.json` / `appsettings.Development.json`: Configuration files (Teacher credentials, connection strings).

## 4. User Roles & Permissions

### Teacher (Administrator)
- **Account Creation**: Seeded from `appsettings.json` on startup. No public registration exists for teachers.
- **Permissions**:
  - **User Management**: Create, edit, and delete student accounts; bulk import students from CSV.
  - **Assignment Management**: Create, rename, and delete "Virtual Folders" (assignments).
  - **Submission Review**: View and download all files submitted by any student.
  - **Reporting**: View real-time submission reports (submitted vs. pending).
  - **Resources**: Upload and share public resources accessible to all students.

### Student
- **Account Creation**: Self-registration via a public page (if enabled) or created by the teacher. Can update their own password.
- **Permissions**:
  - **File Management**: Create files (in-browser), upload files, download, and delete their *own* submissions.
  - **Dashboard**: View active assignments for their specific class/section.
  - **Privacy**: **Cannot** view files submitted by other students.

## 5. Functional Requirements & Conventions

- **File Support**:
  - **Code**: `.java`, `.sql`, `.py`, `.cs`, `.html`, `.js`, `.css`, `.c`, `.cpp`, `.xml`, `.json` (rendered with syntax highlighting via Prism.js).
  - **Documents**: `.odt`, `.ods`, `.odp`, `.pdf`.
  - **Text**: `.txt`, `.md`.
  - **Archives**: `.zip`.
- **File Handling Rules**:
  - Uploading a file for a specific assignment **replaces** any previous submission for that assignment.
  - Creating a file without an extension defaults to `.txt`.
- **Design System**:
  - **Liquid Glass**: Use transparency, background blur, and vibrant multi-color gradients (e.g., Indigo-Purple-Teal mesh).
  - **Responsiveness**: Must be fully functional on mobile and desktop.
- **Offline Constraint**: All frontend assets must be local. No external CDNs are allowed.

## 6. Development Workflow

### Prerequisites
- .NET 9 SDK
- Node.js
- pnpm

### Setup & Commands
1.  **Install Dependencies**:
    ```bash
    pnpm install  # Installs JS deps & runs postinstall to copy assets to wwwroot/lib
    dotnet restore # Restores .NET packages
    ```
2.  **Build CSS**:
    ```bash
    pnpm run css:build # Compiles Tailwind CSS
    ```
3.  **Database Operations**:
    ```bash
    dotnet ef migrations add <MigrationName> # Create a new migration
    dotnet ef database update                # Apply migrations to app.db
    ```
4.  **Running the Application**:
    - **Verification**: Use `dotnet build` to verify compilation in the CLI.
    - **Development Server**: Use `dotnet watch run` in a separate terminal for hot reloading.
    - **Caution**: Do not run `dotnet run` (which blocks) directly in the agent's shell tools unless in background or strictly necessary.

## 7. Deployment (IIS)

1.  **Environment**: Windows Server with IIS and .NET 9 Hosting Bundle installed.
2.  **App Pool**: Create a pool with **.NET CLR version** set to **No Managed Code**.
3.  **Configuration**: before the first run on the server, update `appsettings.json` with the desired `TeacherUsername` and `TeacherPassword` to seed the admin account.