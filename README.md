# 🛒 eCommerce_dpei API

The **eCommerce_dpei API** is a robust backend solution for an e-commerce platform built with **ASP.NET Core**. It provides comprehensive features for managing users, products, categories, carts, orders, addresses, and the checkout process.

---

## 🚀 Features

### 🔐 Authentication & Authorization
- Customer registration and login via **JWT**
- **Role-based access** for Customers and Admins
- Secure password hashing with **BCrypt**
- Retrieve current user info: `GET /api/auth/me`

### 📦 Product Management
- Full **CRUD** support: `GET`, `POST`, `PUT`, `DELETE`
- Pagination: `GET /api/products?pageNumber=1&pageSize=10`
- Admin-only access for product changes
- Product image upload and delete

### 🗂️ Category Management
- Full CRUD for product categories
- Admin-only access for create, update, delete

### 🛒 Cart Management
- Add, update, remove items from cart
- Retrieve current cart contents
- Stock validation on item addition

### 📬 Address Management
- Add, edit, delete addresses
- Set default address

### 📦 Order Management
- Create and cancel orders
- Retrieve personal or admin-level orders
- Admin-only order status management
- Auto stock updates on order changes

### 💳 Checkout
- View cart summary and shipping addresses
- Process orders from cart

---

## 🛠️ Technologies Used

- **Framework**: ASP.NET Core
- **Database**: Entity Framework Core (EcommerceContext)
- **Authentication**: JWT, BCrypt.Net
- **DI**: ASP.NET Core built-in
- **Data Mapping**: AutoMapper
- **Validation**: Custom `ValidatorFilter`
- **Architecture**: Repository Pattern + IUnitOfWork
- **File Upload**: Multipart form-data for product images

---

## 📋 Prerequisites

- **.NET SDK**: v6.0 or later
- **Database**: SQL Server (or EF Core compatible)
- **Configuration**:
  - Set `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience` in `appsettings.json`
  - Configure connection string

---

## ⚙️ Setup Instructions

1. **Clone the repository**  
```bash
git clone https://github.com/your-username/eCommerce_dpei.git
cd eCommerce_dpei
Restore dependencies

bash
نسخ
تحرير
dotnet restore
Configure the database

Update appsettings.json with your DB connection string

Apply migrations:

bash
نسخ
تحرير
dotnet ef migrations add InitialCreate
dotnet ef database update
Run the app

bash
نسخ
تحرير
dotnet run
📡 API Base URL
https://localhost:5001 or http://localhost:5000

📚 API Endpoints Summary
🔐 Authentication
Method	Endpoint	Description
POST	/api/auth/register/customer	Register a customer
POST	/api/auth/login	Login and get JWT
GET	/api/auth/me	Get logged-in user details

📦 Products
Method	Endpoint
GET	/api/products?pageNumber=1&pageSize=10
GET	/api/products/{id}
POST	/api/products (Admin)
PUT	/api/products/{id} (Admin)
DELETE	/api/products/{id} (Admin)
POST	/api/products/{productId}/images
DELETE	/api/images/{imageId}

🗂️ Categories
Method	Endpoint
GET	/api/categories
GET	/api/categories/{id}
POST	/api/categories (Admin)
PUT	/api/categories/{id} (Admin)
DELETE	/api/categories/{id} (Admin)

🛒 Cart
Method	Endpoint
POST	/api/cart
GET	/api/cart
PUT	/api/cart/{productId}
DELETE	/api/cart/{productId}

📬 Addresses
Method	Endpoint
GET	/api/address
POST	/api/address
PUT	/api/address/{id}
DELETE	/api/address/{id}
PUT	/api/address/{id}/default

📦 Orders
Method	Endpoint
GET	/api/order
GET	/api/order/{id}
POST	/api/order
PUT	/api/order/{id}/cancel
GET	/api/order/admin?status=pending (Admin)
PUT	/api/order/admin/{id}/status (Admin)

💳 Checkout
Method	Endpoint
GET	/api/checkout/cart-summary
GET	/api/checkout/shipping-addresses
POST	/api/checkout/process

🔐 Security Highlights
JWT: Authenticated access for protected routes

BCrypt: Secure password storage

Role-Based Authorization: Admin-only restrictions

ValidatorFilter: Custom input validation middleware

🤝 Contributing
Fork the repository

Create your branch: git checkout -b feature/your-feature

Commit changes: git commit -m "Add your feature"

Push to the branch: git push origin feature/your-feature

Open a Pull Request

📬 Contact
For support or questions, reach out to:
📧 osamaahmed52136@gmail.com

