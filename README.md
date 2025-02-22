# Angular eCommerce with .NET Backend

## Overview
This is a **full-stack eCommerce application** built using **Angular** for the frontend and **.NET** for the backend. The project includes user authentication, product management, shopping cart functionality, order processing, and payments.

## Features
### **Frontend (Angular)**
✅ User Authentication (Register/Login)
✅ Product Listing & Filtering
✅ Shopping Cart Management
✅ Order Placement & Payment Integration
✅ Responsive UI

### **Backend (.NET Core API)**
✅ User Authentication (JWT)
✅ Product Management (CRUD)
✅ Cart & Order Management
✅ Secure Payment Processing
✅ RESTful API with Swagger Documentation

## Tech Stack
- **Frontend**: Angular, TypeScript, Bootstrap/Tailwind CSS
- **Backend**: .NET Core Web API, Entity Framework Core, SQL Server
- **Database**: Microsoft SQL Server
- **Authentication**: JWT (JSON Web Tokens)
- **Payment Gateway**: (Stripe/PayPal - Optional)

## Installation & Setup
### **Backend (.NET Core API)**
1. **Clone the Repository:**
   ```sh
   git clone https://github.com/your-repo/angular-dotnet-ecommerce.git
   cd backend
   ```
2. **Setup Database:**
   - Ensure **SQL Server** is installed and running.
   - Configure the connection string in `appsettings.json`.
   - Run migrations:
     ```sh
     dotnet ef database update
     ```
3. **Run the API:**
   ```sh
   dotnet run
   ```
   The API will be available at: `http://localhost:5000`

### **Frontend (Angular)**
1. **Navigate to the Frontend Folder:**
   ```sh
   cd frontend
   ```
2. **Install Dependencies:**
   ```sh
   npm install
   ```
3. **Run the Angular Application:**
   ```sh
   ng serve --open
   ```
   The app will open at `http://localhost:4200`

## API Endpoints
| Method | Endpoint              | Description          |
|--------|----------------------|----------------------|
| POST   | `/api/auth/register` | User Registration   |
| POST   | `/api/auth/login`    | User Login (JWT)    |
| GET    | `/api/products`      | Get All Products    |
| POST   | `/api/cart`          | Add to Cart         |
| POST   | `/api/orders`        | Create an Order     |
| POST   | `/api/payments`      | Process Payment     |

## License
This project is licensed under the **MIT License**.

<!-- ## Contributors
- [Your Name](https://github.com/your-profile) -->

## Contact
For questions, feel free to reach out via **email** or open an **issue** in the repository.

Happy Coding! 🚀