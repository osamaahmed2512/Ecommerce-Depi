ECommerce API
Overview
The eCommerce_dpei API is a robust backend solution for an e-commerce platform built using ASP.NET Core. It provides comprehensive functionality for managing users, products, categories, carts, orders, addresses, and checkout processes. The API incorporates secure authentication and authorization using JWT (JSON Web Tokens) and password hashing with BCrypt. It also supports role-based access control, allowing differentiation between Customer and Admin roles.
Features

User Authentication & Authorization:

Customer registration and login with JWT-based authentication.
Role-based access (Customer and Admin).
Secure password storage using BCrypt.
Endpoint to retrieve current user details (GET /api/auth/me).


Product Management:

CRUD operations for products (GET, POST, PUT, DELETE).
Pagination support for listing products (GET /api/products).
Admin-only access for creating, updating, and deleting products.
Product image management (POST /api/products/{productId}/images, DELETE /api/images/{imageId}).


Category Management:

CRUD operations for categories (GET, POST, PUT, DELETE).
Admin-only access for creating, updating, and deleting categories.


Cart Management:

Add, update, and remove items from the cart (POST, PUT, DELETE).
Retrieve cart contents (GET /api/cart).
Stock validation to prevent adding unavailable quantities.


Order Management:

Create and cancel orders (POST /api/order, PUT /api/order/{id}/cancel).
Retrieve user orders or specific order details (GET /api/order, GET /api/order/{id}).
Admin-only endpoints to view all orders and update order status (GET /api/order/admin, PUT /api/order/admin/{id}/status).
Automatic stock updates upon order creation or cancellation.


Address Management:

CRUD operations for user addresses (GET, POST, PUT, DELETE).
Set default address (PUT /api/address/{id}/default).


Checkout Process:

Retrieve cart summary and shipping addresses (GET /api/checkout/cart-summary, GET /api/checkout/shipping-addresses).
Process checkout to create orders (POST /api/checkout/process).



Technologies Used

Framework: ASP.NET Core
Database: Entity Framework Core (with EcommerceContext)
Authentication: JWT, BCrypt.Net
Dependency Injection: Built-in ASP.NET Core DI
Data Mapping: AutoMapper
Validation: Custom ValidatorFilter for input validation
Repository Pattern: Used with IUnitOfWork for data access
File Upload: Multipart form-data for product images

Prerequisites

.NET SDK: Version 6.0 or later
Database: SQL Server or any database compatible with EF Core
Configuration:
Update appsettings.json with JWT settings (Jwt:Key, Jwt:Issuer, Jwt:Audience).
Configure database connection string in appsettings.json.



Setup Instructions

Clone the Repository:
git clone https://github.com/your-username/eCommerce_dpei.git
cd eCommerce_dpei


Install Dependencies:
dotnet restore


Configure Database:

Update the connection string in appsettings.json.
Apply migrations to set up the database:dotnet ef migrations add InitialCreate
dotnet ef database update




Run the Application:
dotnet run


API Base URL:

The API will be available at https://localhost:5001 or http://localhost:5000 (depending on configuration).



API Endpoints
Below is a summary of the main API endpoints. All endpoints requiring authentication expect a JWT in the Authorization header (Bearer <token>).
Authentication

POST /api/auth/register/customer: Register a new customer.
POST /api/auth/login: Login and receive a JWT.
GET /api/auth/me: Get details  Retrieve current user details (requires authentication).

Products

GET /api/products?pageNumber=1&pageSize=10: List products with pagination.
GET /api/products/{id}: Get a specific product.
POST /api/products: Create a new product (Admin only).
PUT /api/products/{id}: Update a product (Admin only).
DELETE /api/products/{id}: Delete a product (Admin only).
POST /api/products/{productId}/images: Add images to a product.
DELETE /api/images/{imageId}: Delete a product image.

Categories

GET /api/categories: List all categories.
GET /api/categories/{id}: Get a specific category.
POST /api/categories: Create a new category (Admin only).
PUT /api/categories/{id}: Update a category (Admin only).
DELETE /api/categories/{id}: Delete a category (Admin only).

Cart

POST /api/cart: Add an item to the cart.
GET /api/cart: Get cart contents.
PUT /api/cart/{productId}: Update cart item quantity.
DELETE /api/cart/{productId}: Remove an item from the cart.

Orders

GET /api/order: Get user orders.
GET /api/order/{id}: Get a specific order.
POST /api/order: Create a new order.
PUT /api/order/{id}/cancel: Cancel an order.
GET /api/order/admin?status=pending: Get all orders (Admin only).
PUT /api/order/admin/{id}/status: Update order status (Admin only).

Addresses

GET /api/address: Get user addresses.
POST /api/address: Add a new address.
PUT /api/address/{id}: Update an address.
DELETE /api/address/{id}: Delete an address.
PUT /api/address/{id}/default: Set an address as default.

Checkout

GET /api/checkout/cart-summary: Get cart summary.
GET /api/checkout/shipping-addresses: Get user addresses for checkout.
POST /api/checkout/process: Process checkout and create an order.

Security

JWT Authentication: Ensures secure access to protected endpoints.
BCrypt: Passwords are hashed before storage.
Role-Based Authorization: Restricts certain endpoints to Admin users.
Input Validation: Uses a custom ValidatorFilter to validate incoming requests.

Contributing
Contributions are welcome! Please follow these steps:

Fork the repository.
Create a new branch (git checkout -b feature/your-feature).
Commit your changes (git commit -m "Add your feature").
Push to the branch (git push origin feature/your-feature).
Create a Pull Request.

License
This project is licensed under the MIT License.
Contact
For questions or support, please contact your-email@example.com.
