# Customer Relationship Management (CRM) System

A modern, full-featured CRM application built with .NET Blazor Server and Entity Framework Core, following Domain-Driven Design (DDD) principles.

## 📸 Screenshots

### Customer List View
![Customer List](images/Screenshot%202025-11-19%20112855.png)

### Add Customer Modal
![Add Customer Modal](images/Screenshot%202025-11-19%20112904.png)

### Edit Customer Modal
![Edit Customer Modal](images/Screenshot%202025-11-19%20113523.png)

### Delete Confirmation Modal
![Delete Confirmation](images/Screenshot%202025-11-19%20112917.png)

## 🚀 Features

- **Complete CRUD Operations**
  - ✅ Create new customer accounts
  - ✅ Read and display customer details
  - ✅ Update existing customer information
  - ✅ Delete customer accounts

- **User-Friendly Interface**
  - Modal-based forms for creating and editing customers
  - Confirmation dialog for delete operations
  - Real-time form validation
  - Responsive table layout with search and filtering capabilities
  - Bootstrap 5 UI components with icons

- **Data Management**
  - SQL Server database integration
  - Entity Framework Core for data access
  - Auto-generated unique customer IDs
  - Email uniqueness validation
  - Automatic timestamp tracking

## 📋 Customer Data Model

Each customer account includes 10 fields:

1. **Account ID** - Auto-generated primary key (int)
2. **First Name** - Required (string, max 100 chars)
3. **Last Name** - Required (string, max 100 chars)
4. **Email** - Required, unique (string, max 200 chars)
5. **Phone Number** - Optional (string, max 20 chars)
6. **Address** - Optional (string, max 250 chars)
7. **City** - Optional (string, max 100 chars)
8. **State** - Optional (string, max 100 chars)
9. **Country** - Optional (string, max 100 chars)
10. **Date Created** - Auto-generated timestamp (DateTime)

## 🏗️ Architecture

The application follows **Domain-Driven Design (DDD)** principles with clear separation of concerns:

```
├── Models/              # Domain entities
│   └── Customer.cs
├── Data/                # Database context and configurations
│   └── ApplicationDbContext.cs
├── Repositories/        # Data access layer
│   ├── ICustomerRepository.cs
│   └── CustomerRepository.cs
├── Services/            # Business logic layer
│   ├── ICustomerService.cs
│   └── CustomerService.cs
└── Components/
    └── Pages/           # UI layer
        └── Customer.razor
```

### Layers

- **Domain Layer**: Contains the `Customer` entity with data annotations for validation
- **Data Access Layer**: Repository pattern implementation for database operations
- **Business Logic Layer**: Service layer handles validation, business rules, and error handling
- **Presentation Layer**: Blazor Server components with interactive UI

## 🛠️ Technology Stack

- **.NET 10.0** - Latest .NET framework
- **Blazor Server** - Interactive web UI framework
- **Entity Framework Core 9.0** - ORM for database operations
- **SQL Server** - Database (LocalDB for development)
- **Bootstrap 5** - UI framework
- **Bootstrap Icons** - Icon library

## 📦 Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB is sufficient for development)
- Visual Studio Code or Visual Studio 2022

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/developer-evan/CRMSystem.git
cd CRMSystem
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Update Database Connection String

The application uses LocalDB by default. If you need to change the connection string, update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CrmAppDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

### 4. Apply Database Migrations

```bash
dotnet ef database update
```

This will create the `CrmAppDb` database and the `Customers` table.

### 5. Run the Application

```bash
dotnet run
```

The application will start on `http://localhost:5243` (or the port specified in `launchSettings.json`).

## 📖 Usage

### Creating a Customer

1. Click the **"Add New Customer"** button
2. Fill in the required fields (First Name, Last Name, Email)
3. Optionally add phone number, address, city, state, and country
4. Click **"Save"**

### Editing a Customer

1. Click the **"Edit"** button next to any customer in the table
2. Modify the customer information in the modal dialog
3. Click **"Update"**

### Deleting a Customer

1. Click the **"Delete"** button next to any customer
2. Confirm the deletion in the modal dialog
3. Click **"Delete"** to confirm

## ✨ Key Features in Detail

### Form Validation

- **Required fields**: First Name, Last Name, and Email must be provided
- **Email validation**: Must be in valid email format
- **Email uniqueness**: The system prevents duplicate emails
- **Phone number validation**: Must be in valid phone format if provided
- **Field length validation**: All fields have maximum length constraints

### Error Handling

- Validation errors are displayed within the modal dialog
- Duplicate email detection with user-friendly error messages
- Database operation errors are caught and displayed gracefully
- Success messages appear after successful operations

### User Experience

- Loading spinners during data operations
- Disabled buttons during processing to prevent duplicate submissions
- Modal dialogs for add/edit/delete operations
- Responsive table layout that works on all screen sizes
- Real-time data refresh after operations

## 🗃️ Database Schema

```sql
CREATE TABLE [Customers] (
    [AccountId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Email] nvarchar(200) NOT NULL,
    [PhoneNumber] nvarchar(20) NULL,
    [Address] nvarchar(250) NULL,
    [City] nvarchar(100) NULL,
    [State] nvarchar(100) NULL,
    [Country] nvarchar(100) NULL,
    [DateCreated] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    CONSTRAINT [PK_Customers] PRIMARY KEY ([AccountId])
);

CREATE UNIQUE INDEX [IX_Customer_Email] ON [Customers] ([Email]);
```

## 🧪 Testing

To test the application:

1. Create a few customer records with different data
2. Try editing a customer and changing their information
3. Test email uniqueness by creating two customers with the same email
4. Test form validation by leaving required fields empty
5. Delete a customer and verify it's removed from the list

## 🔧 Development

### Adding New Migrations

If you modify the `Customer` model, create a new migration:

```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

### Dropping and Recreating Database

```bash
dotnet ef database drop --force
dotnet ef database update
```

## 📝 Best Practices Implemented

- ✅ **Separation of Concerns**: Clear layer separation (Repository, Service, UI)
- ✅ **Dependency Injection**: All services and repositories are injected
- ✅ **Async/Await**: All database operations are asynchronous
- ✅ **Error Handling**: Comprehensive try-catch blocks with user-friendly messages
- ✅ **Data Validation**: Both client-side and server-side validation
- ✅ **Entity Tracking Management**: Proper use of AsNoTracking() to prevent EF Core tracking issues
- ✅ **Repository Pattern**: Abstraction of data access logic
- ✅ **Service Layer Pattern**: Business logic separated from data access
- ✅ **Clean Code**: Meaningful names, proper comments, and organized structure

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👤 Author

Evans Mogeni

## 🙏 Acknowledgments

- Built with AI assistance (GitHub Copilot)
- UI components from Bootstrap 5
- Icons from Bootstrap Icons
- Entity Framework Core documentation

---

**Note**: This application was developed as part of a technical assessment to demonstrate proficiency in .NET Blazor, Entity Framework Core, and modern software development practices including DDD principles and AI-assisted development.
