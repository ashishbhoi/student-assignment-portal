# **Copilot Instructions: Student Classwork Portal**

## **1\. Your Role & Objective**

You are an expert full-stack developer. Your task is to build a complete web application based on the detailed requirements outlined below. The primary goal is to create a secure and modern portal for students to submit classwork, which will be managed by a single teacher administrator.

**Primary Technology Stack:**

*   **Backend**: .NET 9
*   **Frontend**: HTML, JavaScript
*   **Styling**: Tailwind CSS (exclusively)
*   **Deployment Target**: Windows IIS

## **2\. Core Architecture & Setup**

1.  **Project Initialization**:
    *   Create a new .NET 9 Web Application project using Razor Pages.
    *   Set up the project structure to be compatible with a Windows IIS deployment.
2.  **Frontend Setup**:
    *   Integrate Tailwind CSS into the project. Ensure the build process correctly purges unused CSS for production.
    *   Adhere strictly to **Material Design** principles for all UI components. Use a modern color palette with a clear primary and accent color.
    *   All pages and components **must be fully responsive**.

## **3\. Authentication and User Roles**

Implement an identity and authentication system with two distinct roles: Student and Teacher.

1.  **Student Role**:
    *   Build a public-facing registration page for new students.
    *   Implement a login page.
    *   Create a "Change Password" feature accessible only to logged-in students.
    *   Enforce authorization rules: A logged-in student can **only** view and manage their own data and files.
2.  **Teacher Role (Administrator)**:
    *   This is a single-user role. Do not build a registration page for the teacher. The teacher's account should be created via a database seed or a secure initial setup command.
    *   Enforce authorization rules: The logged-in teacher has full access to view all student data and manage all submitted files.

## **4\. Feature Implementation: Student Dashboard**

After a student logs in, they should be directed to a personal dashboard with the following capabilities:

1.  **File Creation**:
    *   Implement an in-browser file creation tool. It should infer the file type from the extension in the filename (e.g., `MyCode.java`, `report.md`). If no extension is provided, it should default to `.txt`.
2.  **File Upload**:
    *   Create a file upload component. If a student uploads a file for an assignment that already has a submission, the new file should **replace** the old one.
    *   Validate file types on upload to allow only: .java, .sql, .odt, .ods, .odp, .pdf, .txt, .md, .zip.
3.  **File Management**:
    *   Display a list of all files the student has created or uploaded for their assignments.
    *   For each file, provide "View", "Download", and "Delete" buttons.

## **5\. Feature Implementation: Teacher Dashboard**

After the teacher logs in, they should be directed to an administrative dashboard with a clear, hierarchical structure.

1.  **Dashboard Layout**:
    *   The primary view should display the **four most recent assignments** chronologically for quick access.
    *   Below the recent assignments, implement a browser where all assignments are **grouped by class and then by section**.
2.  **Assignment Details & Reporting**:
    *   Clicking on any assignment should navigate to a dedicated details page.
    *   This page must display a clear **submission report** at the top: number of students who have submitted, number who have not, and the total number of students in the class.
    *   Below the report, display a list of all students in the class, their submission status (Submitted / Not Submitted), and a link to view/download the file if it has been submitted.
3.  **Assignment (Virtual Folder) Management**:
    *   Implement functionality for the teacher to **CREATE**, **RENAME**, and **DELETE** virtual folders, which represent the assignments.

## **6\. File Viewing & Syntax Highlighting**

1.  **In-App File Viewer**:
    *   When the teacher or a student clicks on a text-based file (.java, .sql, .txt, .md), display its content directly in the browser instead of forcing a download.
2.  **Syntax Highlighting**:
    *   Integrate **Prism.js** to provide syntax highlighting for `.java` and `.sql` files when viewed in the app.
