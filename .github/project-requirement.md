# **Project Requirements: Student Classwork Portal**

## **1\. Project Overview**

The objective is to create a web application that serves as a centralized repository for students to submit their classwork. A single teacher, acting as the administrator, will manage, view, and organize these submissions. The application should be secure, user-friendly, and built on a modern .NET 9 technology stack for deployment on a Windows IIS server.

## **2\. User Roles & Permissions**

There are two distinct user roles for this application:

### **2.1. Student**

- **Authentication**: Can create a new account and log in. Has the ability to change their password.
- **Permissions**: Can only access and manage their own files. They cannot view files submitted by other students.

### **2.2. Teacher (Administrator)**

- **Authentication**: Has a pre-assigned administrator account, seeded from the application's configuration files.
- **Permissions**: Full administrative rights over the application. Can view and manage all student accounts and all submitted files across the entire platform. This is the sole administrative user.

## **3\. Functional Requirements**

### **3.1. Student Features**

- **Account Management**:
  - CREATE: Students must be able to register for a new account using a unique identifier (e.g., username or student ID) and a password.
  - UPDATE: Students must have a secure way to change their password after logging in.
- **File Management**:
  - CREATE: Students can create new files directly in the browser. The file type is inferred from the extension provided in the file name (e.g., `Main.java`, `query.sql`). If no extension is given, it defaults to a plain text file (`.txt`).
  - UPLOAD: Students can upload a single file for a specific assignment. If a file for that assignment already exists, uploading a new one will replace the previous submission.
  - DOWNLOAD: Students must be able to download any file they have previously created or uploaded.
  - DELETE: Students must be able to delete their own submitted files.
- **Dashboard**:
  - The dashboard only displays active assignments for the student's class and section, filtering out general public resources.

### **3.2. Teacher (Administrator) Features**

- **Dashboard & Reporting**:
  - The main dashboard provides an at-a-glance view of the **four most recent assignments**.
  - A comprehensive browser allows the teacher to view all assignments, **grouped by class and section**.
  - Selecting an assignment opens a detailed view with a **submission report** (submitted vs. not submitted counts) and a list of all students in the class, showing their individual submission status.
- **File Access**:
  - From the assignment details page, the teacher can view and download any submitted file.
- **Assignment Management**:
  - The teacher has the ability to **CREATE**, **RENAME**, and **DELETE** virtual folders, which represent assignments.

## **4\. File System & Structure**

### **4.1. Supported File Types**

The application must support the storage and handling of the following file extensions:

- **Code**: .java, .sql, .py, .cs, .html, .js, .css, .c, .cpp, .xml, .json
- **Documents**: .odt, .ods, .odp (LibreOffice Suite), .pdf
- **Text**: .txt, .md
- **Archives**: .zip

### **4.2. Code Syntax Highlighting**

- When viewing code-based files (e.g., .java, .sql) within the application's file viewer, the code should be rendered with appropriate syntax highlighting to improve readability.

## **5\. Technical Stack & Deployment**

### **5.1. Backend**

- **Framework**: .NET 9
- **Architecture**: The application should be structured in a way that is compatible with deployment on a Windows server.

### **5.2. Frontend & CSS**

- **Styling**: All styling must be implemented using **Tailwind CSS**. No other CSS frameworks or custom CSS files should be used unless absolutely necessary for a specific component that Tailwind cannot style.
- **Dependencies**: All third-party frontend libraries (e.g., jQuery, Prism.js) must be managed via a package manager (`pnpm`) and served locally within the project. This is a firm requirement to ensure the application can run in a completely offline/LAN environment without relying on external CDNs.

### **5.3. Deployment Environment**

- **Server**: The final application must be configured and packaged for deployment on **Windows Internet Information Services (IIS)**.

## **6\. User Interface (UI) & User Experience (UX)**

### **6.1. Design Philosophy**

- **Aesthetic**: The UI should be modern, fluid, and visually engaging.
- **Theme**: Adhere to **Liquid Glass Design** principles. Use translucency, background blur (glassmorphism), and vibrant multi-color gradients to create depth and visual interest.
- **Color Palette**: Use a vibrant, multi-color gradient background (e.g., Indigo-Purple-Teal mesh) with semi-transparent white/dark surfaces for content containers to create a "glass" effect. High contrast text is essential for readability.
- **Responsiveness**: The application must be fully responsive and functional on various screen sizes, from mobile devices to desktop monitors.