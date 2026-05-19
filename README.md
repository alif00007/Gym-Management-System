# 🏋️ Gym Management System

A desktop-based **Gym Management System** built with **C# Windows Forms** and **SQL Server**. Designed to help gym administrators manage members, track attendance, handle payments, and maintain complete member records — all from a clean, user-friendly interface.

---

## 📋 Table of Contents

- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Database Schema](#-database-schema)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
  - [Running the App](#running-the-app)
- [Usage](#-usage)
  - [Login Credentials](#login-credentials)
  - [Role-Based Access](#role-based-access)
- [Screenshots](#-screenshots)
- [Future Improvements](#-future-improvements)
- [Author](#-author)
- [License](#-license)

---

## ✅ Features

| Feature | Description |
|---|---|
| 🔐 Login System | Secure login for Admin and Members with role-based access |
| 📝 Member Registration | Self-registration form for new gym members |
| ➕ Add Member | Admin can add new members with full details |
| 👁️ View Members | Browse and search all registered members |
| ✏️ Update / Delete | Edit or remove existing member records |
| 💳 Payment Management | Record and track monthly membership payments |
| 📅 Attendance Tracking | Log and view member attendance records |
| 📊 Dashboard | At-a-glance summary of total members and monthly revenue |

---

## 🛠 Tech Stack

- **Language:** C#
- **UI Framework:** Windows Forms
- **Runtime:** .NET Framework 4.7.2
- **Database:** SQL Server (SQL Express)
- **IDE:** Visual Studio 2022

---

## 📁 Project Structure

```
Gym Management System/
│
├── Program.cs                  # Application entry point
├── Login.cs                    # Login form (Admin & Member auth)
├── MainForm.cs                 # Dashboard with navigation
├── Registration.cs             # New member self-registration
├── AddMember.cs                # Admin form to add members
├── ViewMembers.cs              # View all members in a data grid
├── UpdateDelete.cs             # Search, update, and delete members
├── Payment.cs                  # Monthly payment recording & history
├── Attendance.cs               # Attendance logging & viewing
│
├── Properties/
│   ├── AssemblyInfo.cs
│   ├── Resources.Designer.cs
│   └── Settings.Designer.cs
│
└── Gym Management System.csproj
```

---

## 🗄 Database Schema

Run the following SQL in **SQL Server Management Studio (SSMS)** after creating the `GymDb` database.

### MemberTbl

```sql
CREATE TABLE [dbo].[MemberTbl] (
    [MId]       INT           IDENTITY (1, 1) NOT NULL,
    [MName]     NVARCHAR (50) NOT NULL,
    [MPhone]    NVARCHAR (50) NOT NULL,
    [MGen]      NVARCHAR (6)  NOT NULL,
    [MAge]      INT           NOT NULL,
    [MAmount]   INT           NOT NULL,
    [MTiming]   NVARCHAR (50) NOT NULL,
    [MPassword] NVARCHAR (50) NULL,
    [Role]      NVARCHAR (20) NULL DEFAULT 'Member',
    PRIMARY KEY CLUSTERED ([MId] ASC)
);
```

### PaymentTbl

```sql
CREATE TABLE [dbo].[PaymentTbl] (
    [PId]           INT            IDENTITY (1, 1) NOT NULL,
    [PMonth]        NVARCHAR (50)  NOT NULL,
    [PMember]       NVARCHAR (50)  NOT NULL,
    [PAmount]       INT            NOT NULL,
    [PaymentMethod] NVARCHAR (50)  NULL,
    [TransactionId] NVARCHAR (100) NULL,
    [PaymentStatus] NVARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([PId] ASC)
);
```

### AttendanceTbl *(if applicable)*

```sql
CREATE TABLE [dbo].[AttendanceTbl] (
    [AId]     INT           IDENTITY (1, 1) NOT NULL,
    [AMember] NVARCHAR (50) NOT NULL,
    [ADate]   DATE          NOT NULL,
    [AStatus] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([AId] ASC)
);
```

---

## 🚀 Getting Started

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) (with .NET desktop development workload)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

---

### Installation

**1. Clone the repository:**

```bash
git clone https://github.com/alif00007/gym-management-system.git
```

**2. Open in Visual Studio:**

- Launch **Visual Studio**
- Click **Open a project or solution**
- Navigate to the cloned folder and select `Gym Management System.csproj`

---

### Database Setup

**1. Open SSMS and create the database:**

```sql
CREATE DATABASE GymDb;
```

**2. Run all the table creation scripts** from the [Database Schema](#-database-schema) section above.

**3. Update the connection string** in each `.cs` file that contains a `SqlConnection`. Replace the default with your own instance:

```csharp
SqlConnection Con = new SqlConnection(
    @"Data Source=YOUR_SERVER_NAME\SQLEXPRESS;
      Initial Catalog=GymDb;
      Integrated Security=True;
      TrustServerCertificate=True");
```

> 💡 Replace `YOUR_SERVER_NAME` with your actual machine/server name (e.g., `DESKTOP-XYZ\SQLEXPRESS`).

Files to update:
- `Login.cs`
- `MainForm.cs`
- `AddMember.cs`
- `ViewMembers.cs`
- `UpdateDelete.cs`
- `Payment.cs`
- `Attendance.cs`

---

### Running the App

1. **Build the solution** — go to `Build > Build Solution` (or press `Ctrl + Shift + B`)
2. **Run the application** — press `F5` or click the **Start** button in Visual Studio

---

## 🖥 Usage

### Login Credentials

| Role | Username | Password |
|------|----------|----------|
| Admin | `Admin` | `Admin` |
| Member | *(registered name)* | *(set during registration)* |

### Role-Based Access

| Feature | Admin | Member |
|---|:---:|:---:|
| Dashboard | ✅ | ✅ |
| Add Member | ✅ | ❌ |
| View All Members | ✅ | ❌ |
| Update / Delete Members | ✅ | ✅ *(own profile)* |
| Payment Management | ✅ | ✅ *(own payments)* |
| Attendance | ✅ | ✅ *(own records)* |

---

## 🔮 Future Improvements

- [ ] Dashboard analytics with charts and graphs
- [ ] Membership expiration alerts and notifications
- [ ] Email / SMS reminders for due payments
- [ ] Export member and payment data to Excel or PDF
- [ ] Improved modern UI design
- [ ] Search and filter across all modules
- [ ] Backup and restore database feature

---

## 👤 Author

**M. Alif Hasan**

- GitHub: [@alif00007](https://github.com/alif00007)

---

## 📄 License

This project is created for **learning and educational purposes**.  
Feel free to fork, modify, and build upon it.

---

> ⭐ If you found this project helpful, please consider giving it a star!
