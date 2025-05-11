🛒 eCommerce_dpei Full-Stack Application (React + ASP.NET Core)
eCommerce_dpei is a comprehensive, full-stack e-commerce platform that combines a React frontend with a secure and scalable ASP.NET Core backend API. This application offers a complete suite of features for online shopping, user authentication, product management, order processing, and administrative control, providing a seamless e-commerce experience.

🚀 Key Features
Authentication & Authorization
Customer Registration & Login: JWT-based authentication for secure user login and registration.

Role-Based Access Control (RBAC): Distinct roles for Customers and Admins, with secure enforcement at both frontend and backend levels.

Password Security: Passwords are hashed using BCrypt for secure storage.

User Profile: Retrieve the logged-in user's profile using the GET /api/auth/me endpoint.

Product & Category Management
Product CRUD Operations: Admins can manage products (create, read, update, delete) via the React Admin Panel, integrated with the backend API.

Category Management: Admins can manage product categories via the backend API and React Admin Panel.

Pagination & Search: Supports product pagination and search, powered by the backend API.

Product Image Upload: Admins can upload and delete product images from the React UI.

Cart, Checkout & Orders
Cart Management: Users can manage their shopping cart (add, update, remove items) via the React frontend.

Cart Summary: Live cart summary display, updated dynamically through API calls.

Checkout Flow: Complete the checkout process via the backend API, including stock validation and order creation.

Address Management
Address CRUD: Users can add, edit, or delete shipping addresses from the React UI.

Default Address: Set a default address, integrated with the backend API.

Checkout Process
Cart Summary & Shipping: View cart and shipping addresses before checkout.

Order Processing: Execute order processing through the backend API, ensuring smooth transactions.

💻 Technologies Used
Frontend (React)
React.js (Vite): Fast development with modern React and Vite.

Axios: For API communication between the frontend and backend.

UI Styling: React Bootstrap and optional Ant Design.

State Management: Managed using React Context (with potential for future Redux integration).

Backend API (ASP.NET Core)
ASP.NET Core 6: Robust API development framework for modern web applications.

JWT: Secure token-based authentication and authorization.

BCrypt.Net: Password hashing for secure storage.

SQL Server: Database storage, accessed using Entity Framework Core (EF Core).

Repository & Unit of Work Patterns: For clean and maintainable code structure.

File Upload: Handling file uploads with Multipart form-data.

AutoMapper: For efficient data mapping between objects.

Custom Validation: ValidatorFilter middleware for request validation.

📋 Prerequisites
.NET SDK: Version 6.0 or later.

Node.js & npm: Required for frontend development.

SQL Server: Or any EF Core-compatible database.

Backend Configuration: Ensure the appsettings.json is configured with:

Jwt:Key

Jwt:Issuer

Jwt:Audience

Database Connection String

⚙️ Setup Instructions
1. Clone the Repository
bash
نسخ
تحرير
git clone https://github.com/your-username/eCommerce_dpei.git
cd eCommerce_dpei
2. Backend Setup (ASP.NET Core API)
bash
نسخ
تحرير
cd backend
dotnet restore
dotnet ef database update
dotnet run
The backend API will be accessible at:

https://localhost:5001

http://localhost:5000

3. Frontend Setup (React.js)
bash
نسخ
تحرير
cd frontend
npm install
npm run dev
The frontend UI will be available at:

http://localhost:5173

Ensure the React app is configured to call the backend API at http://localhost:5000.

📡 API Base URL (Frontend Usage)
http://localhost:5000

📚 API Endpoints Summary
Authentication Endpoints
Method	Endpoint	Description
POST	/api/auth/register/customer	Register a new customer
POST	/api/auth/login	User login & JWT retrieval
GET	/api/auth/me	Get current user information

Product Endpoints
Method	Endpoint	Description
GET	/api/products?pageNumber=1&pageSize=10	Get list of products
GET	/api/products/{id}	Get product details
POST	/api/products	Add a new product (Admin)
PUT	/api/products/{id}	Update product (Admin)
DELETE	/api/products/{id}	Delete product (Admin)
POST	/api/products/{productId}/images	Upload product images
DELETE	/api/images/{imageId}	Delete product image

Category Endpoints
Method	Endpoint	Description
GET	/api/categories	Get list of categories
GET	/api/categories/{id}	Get category details
POST	/api/categories	Add a new category (Admin)
PUT	/api/categories/{id}	Update category (Admin)
DELETE	/api/categories/{id}	Delete category (Admin)

Cart Endpoints
Method	Endpoint	Description
POST	/api/cart	Add item to cart
GET	/api/cart	Get cart details
PUT	/api/cart/{productId}	Update cart item
DELETE	/api/cart/{productId}	Remove item from cart

Address Endpoints
Method	Endpoint	Description
GET	/api/address	Get list of addresses
POST	/api/address	Add a new address
PUT	/api/address/{id}	Update address
DELETE	/api/address/{id}	Delete address
PUT	/api/address/{id}/default	Set default address

Order Endpoints
Method	Endpoint	Description
GET	/api/order	Get list of orders
GET	/api/order/{id}	Get order details
POST	/api/order	Create new order
PUT	/api/order/{id}/cancel	Cancel order
GET	/api/order/admin?status=pending	Admin: Get pending orders
PUT	/api/order/admin/{id}/status	Admin: Update order status

Checkout Endpoints
Method	Endpoint	Description
GET	/api/checkout/cart-summary	Get cart summary
GET	/api/checkout/shipping-addresses	Get shipping addresses
POST	/api/checkout/process	Process order checkout

🔐 Security Highlights
JWT Tokens: Secure communication between the frontend and backend.

BCrypt: Safe password hashing to prevent unauthorized access.

Role-Based Authorization: Admin-only restrictions for sensitive operations.

Custom ValidatorFilter: Middleware used to validate requests and ensure data integrity.

🌐 Full Stack User Flow Example
Registration/Login: The user registers or logs in via the React UI, receiving a JWT from the backend.

API Calls: The JWT is stored in local storage and used for subsequent API calls.

Product Browsing: Users browse products and add them to the cart via the /api/cart endpoint.

Checkout: Users complete the checkout process by invoking the backend API at /api/checkout/process.

Admin Control: Admins manage products, categories, orders, and users via the React Admin Panel.

🤝 Contributing Guidelines
Create a Feature Branch:

bash
نسخ
تحرير
git checkout -b feature/your-feature
Commit Changes:

bash
نسخ
تحرير
git commit -m "Add your feature"
Push Branch:

bash
نسخ
تحرير
git push origin feature/your-feature
Open a Pull Request for review and merging.

📬 Contact Information
For any inquiries, please contact:
📧 osamaahmed52136@gmail.com

