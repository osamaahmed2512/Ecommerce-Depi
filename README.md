🛒 eCommerce_dpei Full-Stack Application (React + ASP.NET Core)
eCommerce_dpei is a robust, full-stack e-commerce platform that integrates a modern React frontend with a secure and scalable ASP.NET Core backend API.
This system provides comprehensive features for online shopping, user authentication, product management, order processing, and administrative control, ensuring a seamless and professional e-commerce experience.

🚀 Key Features (Full Stack Overview)
🔐 Authentication & Authorization
Customer registration and login using JWT tokens, integrated within the React frontend.

Role-based access control (RBAC) supporting Customers and Admins with secure enforcement in both frontend and backend layers.

Passwords are securely hashed using BCrypt.

Retrieve the current logged-in user profile via GET /api/auth/me and display it in the frontend dashboard.

📦 Product & Category Management
Complete CRUD operations on products directly from the React Admin Panel, fully integrated with the backend API.

Manage categories via React Admin, using the backend API.

Pagination and search functionalities implemented in the frontend, communicating with the backend API.

Product image upload and deletion using forms in React, interacting with the API.

🛒 Cart, Checkout & Orders (Full Transaction Flow)
Add, update, and remove items from the cart via the React UI.

Live cart summary display connected to backend APIs.

End-to-end checkout process through the frontend using the /api/checkout/process endpoint.

Stock validation and updates are performed on the backend.

Order creation and status management.

📬 Address Management
Manage addresses (add, edit, delete) through the React UI.

Set a default address using the backend API.

💳 Checkout Process
View cart summaries and shipping addresses within the frontend UI.

Execute order processing through the backend API directly from the React checkout page.

💻 Technologies Used (Full Stack)
Layer	Technologies
Frontend (React)	React.js (Vite), Axios, React Context/Auth
UI Styling	React Bootstrap, Ant Design (optional)
State Management	Context API (with flexibility for Redux integration)
Backend API	ASP.NET Core 6, JWT, BCrypt.Net
Database	SQL Server + Entity Framework Core (EF Core)
Architecture	Repository Pattern + Unit of Work (Backend)
File Upload	Multipart form-data (Backend)
Data Mapping	AutoMapper (Backend)
Validation	ValidatorFilter (Backend)

📋 Prerequisites
.NET SDK version 6.0 or later.

Node.js and npm (for frontend development).

SQL Server or any compatible EF Core-supported database.

Configuration of appsettings.json in the backend project with the following:

Jwt:Key

Jwt:Issuer

Jwt:Audience

Database Connection String

⚙️ Setup Instructions
1. Clone the Repository


git clone https://github.com/your-username/eCommerce_dpei.git
cd eCommerce_dpei
2. Backend Setup (ASP.NET Core API)


cd backend
dotnet restore
dotnet ef database update
dotnet run
Access the backend API at:

https://localhost:5001

http://localhost:5000

3. Frontend Setup (React.js)



cd frontend
npm install
npm run dev
Access the frontend UI at:

http://localhost:5173

Ensure the React app is configured to call the backend API at http://localhost:5000.

📡 API Base URL (Frontend Usage)

http://localhost:5000
📚 API Endpoints Summary
Authentication
Method	Endpoint	Description
POST	/api/auth/register/customer	Register a new customer
POST	/api/auth/login	User login & JWT retrieval
GET	/api/auth/me	Get the current user info

Products
Method	Endpoint
GET	/api/products?pageNumber=1&pageSize=10
GET	/api/products/{id}
POST	/api/products (Admin)
PUT	/api/products/{id} (Admin)
DELETE	/api/products/{id} (Admin)
POST	/api/products/{productId}/images
DELETE	/api/images/{imageId}

Categories
Method	Endpoint
GET	/api/categories
GET	/api/categories/{id}
POST	/api/categories (Admin)
PUT	/api/categories/{id} (Admin)
DELETE	/api/categories/{id} (Admin)

Cart
Method	Endpoint
POST	/api/cart
GET	/api/cart
PUT	/api/cart/{productId}
DELETE	/api/cart/{productId}

Addresses
Method	Endpoint
GET	/api/address
POST	/api/address
PUT	/api/address/{id}
DELETE	/api/address/{id}
PUT	/api/address/{id}/default

Orders
Method	Endpoint
GET	/api/order
GET	/api/order/{id}
POST	/api/order
PUT	/api/order/{id}/cancel
GET	/api/order/admin?status=pending (Admin)
PUT	/api/order/admin/{id}/status (Admin)

Checkout
Method	Endpoint
GET	/api/checkout/cart-summary
GET	/api/checkout/shipping-addresses
POST	/api/checkout/process

🔐 Security Highlights
JWT tokens ensure secure communication between the frontend and backend API.

BCrypt is used for safe password hashing.

Role-based authorization secures sensitive operations for Admins.

Custom ValidatorFilter middleware is used for backend request validation.

🌐 Full Stack User Flow Example
User registers or logs in via the React UI, receiving a JWT token from the backend.

The JWT is saved in local storage and used in all protected API calls.

Users browse products and add them to their cart via the React interface, calling the /api/cart endpoint.

Users complete the checkout process by invoking the backend API at /api/checkout/process.

The backend handles stock management and order creation.

Administrators manage products, categories, orders, and users from the React Admin Panel, using backend APIs.

🤝 Contributing Guidelines



git checkout -b feature/your-feature
git commit -m "Add your feature"
git push origin feature/your-feature
Please open a Pull Request for review and merging.

📬 Contact Information
For any inquiries, please contact:
📧 osamaahmed52136@gmail.com