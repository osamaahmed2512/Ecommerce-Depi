🛒 eCommerce_dpei: Full-Stack E-Commerce Platform

eCommerce_dpei is a modern, full-stack e-commerce application built with a React frontend and a secure, scalable ASP.NET Core backend API. It offers a robust suite of features for online shopping, user management, product administration, and order processing, delivering a seamless e-commerce experience.

🚀 Key Features
🔐 Authentication & Authorization

Customer Registration & Login: Secure JWT-based authentication.
Role-Based Access Control (RBAC): Separate roles for Customers and Admins, enforced on both frontend and backend.
Password Security: Passwords hashed with BCrypt.
User Profile: Retrieve logged-in user details via GET /api/auth/me.

🛍️ Product & Category Management
Product CRUD: Admins can create, read, update, and delete products using the React Admin Panel.
Category Management: Full CRUD operations for product categories.
Pagination & Search: Efficient product browsing with backend-powered pagination and search.
Image Upload: Admins can upload/delete product images via the React UI.

🛒 Cart, Checkout & Orders
Cart Management: Add, update, or remove items with real-time cart summary updates.
Checkout Flow: Seamless checkout with stock validation and order creation via the backend API.
Order Management: View, cancel, or update orders; admins can manage pending orders and statuses.

📍 Address Management
Address CRUD: Users can manage shipping addresses.
Default Address: Set a default address for checkout, integrated with the backend.

💻 Technologies Used
Frontend (React)
React.js (Vite): Fast, modern frontend development.
Axios: For API communication.
UI Styling: React Bootstrap (optional Ant Design).
State Management: React Context (scalable to Redux).
Backend (ASP.NET Core)
ASP.NET Core 6: Robust API framework.
JWT: Secure token-based authentication.
BCrypt.Net: Password hashing.
SQL Server: Database with Entity Framework Core.
Patterns: Repository & Unit of Work for clean code.
File Upload: Supports multipart form-data.
AutoMapper: Efficient object mapping.
Custom Validation: Middleware for request validation.

📋 Prerequisites
.NET SDK: Version 6.0 or later.
Node.js & npm: For frontend development.
SQL Server: Or any EF Core-compatible database.
Backend Configuration:
Update appsettings.json with:
Jwt:Key, Jwt:Issuer, Jwt:Audience
Database connection string
⚙️ Setup Instructions
1. Clone the Repository


git clone https://github.com/your-username/eCommerce_dpei.git
cd eCommerce_dpei
2. Backend Setup (ASP.NET Core API)



cd backend
dotnet restore
dotnet ef database update
dotnet run
API Access:
https://localhost:5001
http://localhost:5000
3. Frontend Setup (React.js)


cd frontend
npm install
npm run dev
UI Access: http://localhost:5173
Ensure the React app points to the backend API at http://localhost:5000.
📡 API Base URL
http://localhost:5000

📚 API Endpoints
Authentication
Method	Endpoint	Description
POST	/api/auth/register/customer	Register a new customer
POST	/api/auth/login	User login & JWT retrieval
GET	/api/auth/me	Get current user information
Products
Method	Endpoint	Description
GET	/api/products?pageNumber=1&pageSize=10	List products with pagination
GET	/api/products/{id}	Get product details
POST	/api/products	Add product (Admin)
PUT	/api/products/{id}	Update product (Admin)
DELETE	/api/products/{id}	Delete product (Admin)
POST	/api/products/{productId}/images	Upload product images
DELETE	/api/images/{imageId}	Delete product image
Categories
Method	Endpoint	Description
GET	/api/categories	List categories
GET	/api/categories/{id}	Get category details
POST	/api/categories	Add category (Admin)
PUT	/api/categories/{id}	Update category (Admin)
DELETE	/api/categories/{id}	Delete category (Admin)
Cart
Method	Endpoint	Description
POST	/api/cart	Add item to cart
GET	/api/cart	Get cart details
PUT	/api/cart/{productId}	Update cart item
DELETE	/api/cart/{productId}	Remove item from cart
Addresses
Method	Endpoint	Description
GET	/api/address	List addresses
POST	/api/address	Add address
PUT	/api/address/{id}	Update address
DELETE	/api/address/{id}	Delete address
PUT	/api/address/{id}/default	Set default address
Orders
Method	Endpoint	Description
GET	/api/order	List orders
GET	/api/order/{id}	Get order details
POST	/api/order	Create new order
PUT	/api/order/{id}/cancel	Cancel order
GET	/api/order/admin?status=pending	Admin: List pending orders
PUT	/api/order/admin/{id}/status	Admin: Update order status
Checkout
Method	Endpoint	Description
GET	/api/checkout/cart-summary	Get cart summary
GET	/api/checkout/shipping-addresses	Get shipping addresses
POST	/api/checkout/process	Process order checkout
🔐 Security Highlights
JWT Tokens: Secure frontend-backend communication.
BCrypt: Password hashing for data protection.
RBAC: Admin-only access for sensitive operations.
ValidatorFilter: Middleware for request validation.
🌐 User Flow Example
Register/Login: User signs up/logs in via React UI, receiving a JWT.
API Calls: JWT stored in local storage for authenticated requests.
Product Browsing: Users browse/add products to cart (/api/cart).
Checkout: Complete checkout via /api/checkout/process.
Admin Panel: Admins manage products, categories, and orders.
🤝 Contributing
Create a Feature Branch:


git checkout -b feature/your-feature
Commit Changes:
\

git commit -m "Add your feature"
Push Branch:


git push origin feature/your-feature
Open a Pull Request for review.
📬 Contact
For inquiries, reach out to:

📧 osamaahmed52136@gmail.com