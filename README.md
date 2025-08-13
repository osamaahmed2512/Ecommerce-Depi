🛒 eCommerce_dpei – Backend (ASP.NET Core Web API)

eCommerce_dpei is a secure, scalable, and modern e-commerce backend built with ASP.NET Core and Entity Framework Core.
It provides RESTful APIs for authentication, products, categories, carts, orders, addresses, and payments — with JWT authentication, role-based access control, and PayPal integration.

🚀 Key Features
🔐 Authentication & Security

JWT Authentication with refresh tokens.

Role-Based Access Control (RBAC): Admin and User roles.

Password hashing using BCrypt.

ASP.NET Identity integration.

Custom request validation with ValidatorFilter.

Test Admin Account

Email: admin5016@gmail.com
Password: Admin123@

🛍️ E-Commerce Features

Product Management – Full CRUD for products (Admin only), image upload/delete.

Category Management – Full CRUD for categories (Admin only).

Cart Management – Add/update/remove items, get current user cart.

Order Management – Create orders, update status (Admin), view orders.

Address Management – CRUD operations, set default address.

Checkout – Integrated with PayPal payment flow.

🛠️ Tech Stack

Language: C#

Framework: ASP.NET Core 8 Web API

Database: SQL Server + Entity Framework Core

Security: JWT, BCrypt, Refresh Tokens

Architecture: Clean Architecture, Repository Pattern, Unit of Work, Specification Pattern

Payments: PayPal Integration

Mapping: AutoMapper

📋 Prerequisites

.NET SDK: 8.0+

SQL Server

PayPal Developer Account (for payment features)

Postman (recommended for API testing)

⚙️ Setup Instructions
1️⃣ Clone the Repository
git clone https://github.com/your-username/eCommerce_dpei.git
cd eCommerce_dpei/backend

2️⃣ Configure Environment

Update appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=EcommerceDb;Trusted_Connection=True;"
},
"Jwt": {
  "Key": "your-secret-key",
  "Issuer": "your-app",
  "Audience": "your-app"
},
"PayPal": {
  "ClientId": "your-paypal-client-id",
  "ClientSecret": "your-paypal-client-secret"
}

3️⃣ Apply Migrations & Run
dotnet restore
dotnet ef database update
dotnet run


Backend runs at:

https://localhost:5001
http://localhost:5000

📡 API Endpoints
Authentication
Method	Endpoint	Description
POST	/api/auth/login	Login & get JWT token
POST	/api/auth/register	Register new user
POST	/api/auth/refreshToken	Refresh JWT token
POST	/api/auth/revokeToken	Revoke token (logout)
Products (Admin)
Method	Endpoint	Description
GET	/api/products/{id}	Get product by ID
POST	/api/products	Create product
PUT	/api/products/{id}	Update product
DELETE	/api/products/{id}	Delete product
POST	/api/products/{id}	Add product image
DELETE	/api/products/{productId}/image/{imageId}	Delete product image
Categories (Admin)
Method	Endpoint
GET	/api/category
GET	/api/category/{id}
POST	/api/category
PUT	/api/category/{id}
DELETE	/api/category/{id}
Cart
Method	Endpoint
POST	/api/cart
GET	/api/cart
PUT	/api/cart/{productId}
DELETE	/api/cart/{productId}
Orders
Method	Endpoint
GET	/api/order (User)
GET	/api/order/{id} (Admin)
POST	/api/order?AddressId={id} (User)
PUT	/api/order/admin/{id}/status (Admin)
Addresses
Method	Endpoint
GET	/api/address
POST	/api/address
PUT	/api/address/{id}
DELETE	/api/address/{id}
PUT	/api/address/{id}/default
Payments (PayPal)
Method	Endpoint
POST	/api/payment/paypal/create
POST	/api/payment/paypal/execute
POST	/api/payment/paypal/cancel
🔐 Testing as Admin

You can test Admin-only endpoints with:

Email: admin5016@gmail.com
Password: Admin123@


Login → Copy JWT token → Add to Authorization: Bearer {token} in Postman.

📬 Contact

For inquiries:
📧 osamaahmed52136@gmail.com