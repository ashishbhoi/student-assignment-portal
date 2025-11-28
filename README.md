# 📚 Student Classwork Portal

![.NET](https://img.shields.io/badge/.NET-9-blueviolet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Razor%20Pages-blue)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-Core-green)
![Tailwind CSS](https://img.shields.io/badge/Tailwind%20CSS-3-cyan)
![License](https://img.shields.io/badge/License-GPL--3.0--only-red)

A modern web portal for managing student assignments and submissions, sharing resources, and streamlining communication
between teachers and students. The application is designed to be fully self-contained, requiring no internet connection
to run, making it ideal for offline or LAN environments.

---

## ✨ Key Features

- **Role-Based Access Control**: Separate dashboards and functionalities for **Teachers** and **Students**.
- **Comprehensive User Management**: Teachers can create, edit, and manage student accounts, including bulk import from
  a CSV file.
- **Assignment Management**: Teachers can create, update, and manage assignments, specifying details like subject,
  chapter, and topic.
- **Resource Sharing**: Teachers can upload and share public resources (e.g., notes, links) accessible to all students.
- **Student Submissions**: Students can view assignments, create files (e.g., `.txt`, `.java`, `.sql`), and submit their
  work.
- **Submission Tracking**: A detailed assignment view for teachers shows a real-time report of which students have and
  have not submitted their work.
- **Modern UI**: A stunning, responsive user interface featuring a **Liquid Glass Design** (glassmorphism), vibrant gradients, and fluid interactions built with **Tailwind CSS**.
- **Offline First**: All dependencies are served locally, ensuring the application is fully functional without an
  internet connection.

---

## 🛠️ Tech Stack

- **Backend**: ASP.NET 9 Razor Pages
- **Database**: Entity Framework Core with SQLite
- **Authentication**: ASP.NET Core Identity
- **Frontend Styling**: Tailwind CSS
- **Frontend Dependencies**: pnpm for managing local, offline-first libraries (jQuery, Prism.js, etc.)

---

## 🚀 Local Development Setup

Follow these instructions to get a copy of the project up and running on your local machine for development and testing
purposes.

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js](https://nodejs.org/) (which includes npm)
- [pnpm](https://pnpm.io/installation) (recommended package manager)

### Installation

1. **Clone the repository:**

   ```sh
   git clone https://github.com/ashishbhoi/student-assignment-portal.git
   cd StudentClassworkPortal
   ```

2. **Configure Teacher Credentials:**
   Set up the initial teacher account by providing credentials in `appsettings.Development.json`.

   ```json
   {
     "AppSettings": {
       "TeacherUsername": "teacheradmin",
       "TeacherPassword": "P@ssw0rd"
     }
   }
   ```

3. **Install Dependencies:**
   This single command installs all frontend (`pnpm`) and backend (`dotnet`) dependencies. The `postinstall` script will
   automatically copy necessary libraries from `node_modules` to `wwwroot/lib`.

   ```sh
   pnpm install
   dotnet restore
   ```

4. **Build Frontend Assets:**
   Compile the Tailwind CSS file. For development, you can use the `watch` command to automatically rebuild on changes.

   ```sh
   pnpm run build  # For a single build
   pnpm run watch # To watch for changes
   ```

5. **Apply Database Migrations:**
   This will create the `app.db` SQLite database file and apply the latest schema.

   ```sh
   dotnet ef database update
   ```

6. **Run the Application:**

   ```sh
   dotnet run
   ```

The application will be available at `https://localhost:5001` or a similar port.

---

## 🖥️ Hosting on Windows IIS

To deploy the application to a production environment on Windows, download the latest `IIS_Release.zip` from the
[GitHub Releases page](https://github.com/ashishbhoi/student-assignment-portal/releases).

### IIS Prerequisites

1. Windows Server with **IIS (Internet Information Services)** enabled.
2. **[.NET 9 Hosting Bundle](https://dotnet.microsoft.com/download/dotnet/9.0)** installed on the server. This is a
   critical step that installs the necessary runtimes for IIS to host ASP.NET Core applications.

### Deployment Steps

1. **Download and Extract:**
   Download the `IIS_Release.zip` file from the latest release and extract its contents to a folder on the server (e.g.,
   `C:\inetpub\wwwroot\StudentPortal`).

2. **Create Application Pool:**

   - Open **IIS Manager**.
   - Go to **Application Pools** and click **Add Application Pool...**.
   - Name it (e.g., `StudentPortalPool`) and set the **.NET CLR version** to **No Managed Code**. This is required for
     self-contained ASP.NET Core applications.
   - Click **OK**.

3. **Create the Website:**

   - In IIS Manager, right-click on the **Sites** folder and select **Add Website...**.
   - **Site name:** Enter a descriptive name (e.g., `Student Classwork Portal`).
   - **Application pool:** Select the `StudentPortalPool` you just created.
   - **Physical path:** Browse to the folder where you extracted the release files.
   - **Binding:** Configure the desired hostname and port (e.g., Port 80 for standard HTTP).
   - Click **OK**.

4. **Configure `appsettings.json`:**

   - In the deployment folder, open the `appsettings.json` file with a text editor.
   - Set the `TeacherUsername` and `TeacherPassword` for the initial administrator account. This is a critical step for
     the
     first run.

   ```json
   {
     "AppSettings": {
       "TeacherUsername": "teacherusername",
       "TeacherPassword": "A_Very_Strong_Password!@#"
     }
   }
   ```

5. **Start the Website:**
   The website should start automatically. You can now browse to the configured address to access the portal. The
   database (`app.db`) will be created automatically in the deployment folder on the first visit.

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! Feel free to check
the [issues page](https://github.com/ashishbhoi/student-assignment-portal/issues).

---

## 📜 License

This project is licensed under the GPL-3.0-only License.
