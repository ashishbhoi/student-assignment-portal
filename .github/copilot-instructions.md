# Copilot Instructions: Student Classwork Portal

This document provides instructions for AI coding agents to effectively contribute to the Student Classwork Portal project.

## 1. Project Overview & Architecture

The Student Classwork Portal is a web application built with **.NET 9 and Razor Pages**. It allows students to submit assignments and a teacher to manage them.

- **Backend**: .NET 9, ASP.NET Core, Entity Framework Core.
- **Frontend**: Razor Pages (`.cshtml`), vanilla JavaScript. Frontend dependencies like jQuery and Prism.js are managed via `pnpm` and copied to `wwwroot/lib` to ensure the application is fully functional in an offline/LAN environment.
- **Styling**: **Tailwind CSS** is used for styling, following **Liquid Glass Design** principles (glassmorphism, gradients, translucency).
- **Database**: The application uses a SQLite database (`app.db`) managed via EF Core migrations. Data models are located in the `Models/` directory.
- **Authentication**: Implemented using ASP.NET Core Identity with two roles: `Student` and `Teacher`. User-related data models are in `Areas/Identity/`.

## 2. Key Files & Directories

- `StudentClassworkPortal.csproj`: Defines project dependencies, including .NET 9 and necessary packages.
- `Program.cs`: The application entry point. Configures services, middleware, and authentication.
- `Pages/`: Contains all Razor Pages for the application's UI. Business logic is contained within the PageModels (`.cshtml.cs` files).
- `Data/ApplicationDbContext.cs`: The EF Core DbContext, defining the database schema.
- `Models/`: Contains the core data models: `UserFile` (for student submissions) and `VirtualFolder` (for assignments).
- `package.json`: Lists frontend dependencies (like Tailwind CSS and Prism.js) managed with `pnpm`.
- `Styles/app.css`: The source file for Tailwind CSS, which is compiled to `wwwroot/css/app.css`.
- `wwwroot/js/site.js`: Location for custom application-specific JavaScript.

## 3. Developer Workflows

### Backend Development

- **Install Dependencies**:
  ```bash
  dotnet restore
  ```
- **Running the application**: For development, use the watch command to automatically rebuild on changes.
  ```bash
  dotnet watch run
  ```
- **Database Migrations**: To update the database schema after changing models in `Models/` or `Data/`:
  ```bash
  dotnet ef migrations add <MigrationName>
  dotnet ef database update
  ```

### Frontend Development

- **Install Dependencies**: Before working on the frontend, install Node.js dependencies using pnpm. This also runs a `postinstall` script to copy required assets into `wwwroot/lib`.
  ```bash
  pnpm install
  ```
- **Building CSS**: To compile Tailwind CSS, run the build script defined in `package.json`. This is necessary to see style changes.
  ```bash
  pnpm run css:build
  ```

## 4. Project-Specific Conventions

- **Teacher Account**: The administrator (`Teacher`) account is not created via a public registration page. It is seeded from the configuration file `appsettings.Development.json` on application startup. See `Data/SeedData.cs` for the implementation.
- **File Handling**:
  - Student file uploads for an assignment replace any previous submissions for that same assignment.
  - File uploads are restricted to specific types: `.java, .sql, .odt, .ods, .odp, .pdf, .txt, .md, .zip`.
- **Syntax Highlighting**: The application uses **Prism.js** for in-browser syntax highlighting of code files. The necessary assets are managed via `pnpm` and included in the frontend build process via the `postinstall` script in `package.json`.

## 5. Git Commit Message Convention

When asked to generate a git commit message, provide a **single-line commit message** with an emoji prefix indicating the change type:

- ✨ `feat:` - New feature
- 🐛 `fix:` - Bug fix
- 📝 `docs:` - Documentation
- 💄 `style:` - UI/style changes
- ♻️ `refactor:` - Refactor
- 🔧 `chore:` - Configuration changes
- ⬆️ `deps:` - Dependency updates
- 🗃️ `db:` - Database/migration changes
- 🔒 `security:` - Security fixes
- 🧹 `cleanup:` - Cleanup/chores

**Example**: `✨ feat: Add file upload validation for assignments`

## 6. GitHub Pull Request Convention

When asked to generate a GitHub PR title and description, follow this format:

### PR Title

Use a descriptive title with an emoji prefix matching the change type (same as commit conventions):

**Example**: `✨ feat: Add student dashboard with assignment overview`

### PR Description

Use this template with emojis to make it engaging and easy to read:

```markdown
## 🎯 What does this PR do?

A brief summary of the changes and their purpose.

## 🔄 Changes Made

- ✅ Change 1
- ✅ Change 2
- ✅ Change 3

## 🧪 How to Test

1. Step one
2. Step two
3. Step three

## 📸 Screenshots (if applicable)

Add screenshots or GIFs demonstrating UI changes.

## 📝 Additional Notes

Any extra context, breaking changes, or follow-up tasks.
```
