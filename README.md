# Gym Management System

A desktop-based Gym Management System built using **C# Windows Forms** and **SQL Server**. This application helps gym administrators manage members, payments, registrations, and member records efficiently.

---

## Features

* Member Registration
* Add New Members
* Update & Delete Member Information
* View All Members
* Payment Management
* Login System
* Simple and User-Friendly Interface

---

## Technologies Used

* **C#**
* **Windows Forms (.NET Framework 4.7.2)**
* **SQL Server**
* **Visual Studio**

---

## Project Structure

```text
Gym Management System/
│
├── AddMember.cs
├── Login.cs
├── MainForm.cs
├── Payment.cs
├── Registration.cs
├── UpdateDelete.cs
├── ViewMembers.cs
├── Program.cs
└── Gym Management System.csproj
```

---

## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/alif00007/gym-management-system.git
```

### 2. Open the Project

1. Open **Visual Studio**
2. Click **Open a project or solution**
3. Select:

```text
Gym Management System.csproj
```

---

## Database Setup

1. Open **SQL Server Management Studio (SSMS)**
2. Create a database named:

```sql
GymDb
```

3. Create the required tables such as:

* MemberTbl
* PaymentTbl
* RegistrationTbl

4. Update your SQL connection string inside the project files if needed:

```csharp
SqlConnection Con = new SqlConnection(@"Your SQL Connection String");
```

---

## Running the Application

1. Build the project in Visual Studio
2. Press:

```text
F5
```

or click:

```text
Start
```

---

## Screens Included

The system contains forms for:

* Login
* Registration
* Add Member
* Update/Delete Member
* Payment Management
* View Members

---

## Future Improvements

* Dashboard with analytics
* Attendance tracking
* Membership expiration alerts
* Email/SMS notifications
* Improved UI design

---

## Author

**M. ALIF HASAN**

---

## License

This project is created for learning and educational purposes.
