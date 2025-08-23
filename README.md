# 📚 Student Classwork Portal

![.NET](https://img.shields.io/badge/.NET-9-blueviolet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Razor%20Pages-blue)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-Core-green)
![Tailwind CSS](https://img.shields.io/badge/Tailwind%20CSS-3-cyan)
![License](https://img.shields.io/badge/License-GPL--3.0--only-red)

A modern web portal for managing student assignments and submissions, sharing resources, and streamlining communication between teachers and students.

---

## ✨ Key Features

- **Role-Based Access Control**: Separate dashboards and functionalities for **Teachers** and **Students**.
- **Comprehensive User Management**: Teachers can create, edit, and manage student accounts, including bulk import from a CSV file.
- **Assignment Management**: Teachers can create, update, and manage assignments, specifying details like subject, chapter, and topic.
- **Resource Sharing**: Teachers can upload and share public resources (e.g., notes, links) accessible to all students.
- **Student Submissions**: Students can view assignments, create files (e.g., `.txt`, `.java`, `.sql`), and submit their work.
- **Submission Tracking**: A detailed assignment view for teachers shows a real-time report of which students have and have not submitted their work.
- **Modern UI**: A clean and responsive user interface built with **Tailwind CSS**.

---

## 🛠️ Tech Stack

- **Backend**: ASP.NET 9 Razor Pages
- **Database**: Entity Framework Core with SQLite
- **Authentication**: ASP.NET Core Identity
- **Frontend Styling**: Tailwind CSS
- **Build Tooling**: Node.js/pnpm for frontend asset compilation

---

## 🚀 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js](https://nodejs.org/) (which includes npm)
- [pnpm](https://pnpm.io/installation) (recommended package manager)

### Installation

1.  **Clone the repository:**

    ```sh
    git clone https://github.com/ashishbhoi/student-assignment-portal.git
    cd StudentClassworkPortal
    ```

2.  **Configure Teacher Credentials:**
    Set up the initial teacher account by providing credentials in `appsettings.Development.json`.

    ```json
    {
      "AppSettings": {
        "TeacherEmail": "teacher@example.com",
        "TeacherPassword": "YourSecurePassword123!"
      }
      // ... other settings
    }
    ```

3.  **Install Frontend Dependencies:**

    ```sh
    pnpm install
    ```

4.  **Build Frontend Assets:**
    Compile the Tailwind CSS file.

    ```sh
    pnpm run css:build
    ```

5.  **Restore .NET Dependencies:**

    ```sh
    dotnet restore
    ```

6.  **Apply Database Migrations:**
    This will create the `app.db` SQLite database file and apply the latest schema.

    ```sh
    dotnet ef database update
    ```

7.  **Run the Application:**
    ```sh
    dotnet run
    ```

The application will be available at `https://localhost:5001` or a similar port. The default teacher account will be created on the first run.

---

## 📄 Usage

- **Teacher Login**: Use the email and password you configured in `appsettings.Development.json`.
- **Student Login**: Teachers can create new student accounts from their dashboard.

Once logged in, you can explore the features corresponding to your role.

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! Feel free to check the [issues page](https://github.com/ashishbhoi/student-assignment-portal/issues).

---

## 📜 License

This project is licensed under the GPL-3.0-only License.
