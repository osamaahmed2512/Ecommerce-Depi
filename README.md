🛒 eCommerce_dpei Full-Stack Application (React + ASP.NET Core)
eCommerce_dpei is a full-stack e-commerce platform combining a powerful React frontend and a secure ASP.NET Core backend API.
It delivers complete functionalities for online shopping, admin controls, user authentication, and seamless order processing.

🚀 Features (Full Stack Overview)
🔐 Authentication & Authorization
Customer Registration & Login via JWT (React UI integrated with backend API).

Role-based access for Customers & Admins (secured backend, enforced in frontend UI).

Secure password hashing with BCrypt (backend).

Retrieve current user info (GET /api/auth/me) and display in frontend profile/dashboard.

📦 Product & Category Management
Products: Full CRUD from React Admin Panel, integrated with ASP.NET Core API.

Categories: Manage categories from React Admin, using backend endpoints.

Pagination & Search: Browse products from frontend with pagination support.

Product Images: Upload & delete images using React forms connected to backend.

🛒 Cart, Checkout & Orders (Full Flow)
Add, update, remove items from the cart (React UI).

View live cart summary (React connected to backend APIs).

Checkout process through frontend using API /api/checkout/process.

Stock validation and updates on the backend.

Order creation and status management.

📬 Address Management
Add, edit, delete addresses (React UI).

Set default address (from React, using backend API).

💳 Checkout Process
View cart summary & shipping addresses in frontend.

Process orders from React checkout page via backend API.

💻 Technologies Used (Full Stack)
Layer	Technology
Frontend (React)	React.js (Vite), Axios, React Context/Auth
UI Styling	React Bootstrap, Ant Design (optional)
State Management	Context API (Ready for Redux integration)
Backend API	ASP.NET Core 6, JWT, BCrypt.Net
Database	SQL Server + Entity Framework Core (EF Core)
Architecture	Repository Pattern + Unit of Work (Backend)
File Upload	Multipart form-data (Backend)
Data Mapping	AutoMapper (Backend)
Validation	ValidatorFilter (Backend)

📋 Prerequisites (Full Stack)
.NET SDK: v6.0 or later.

Node.js & npm (for frontend).

SQL Server or compatible EF Core DB.

Setup backend appsettings.json with:

Jwt:Key

Jwt:Issuer

Jwt:Audience

Database connection string

⚙️ Setup Instructions (Full Stack)
1️⃣ Clone the repository:
bash
نسخ
تحرير
git clone https://github.com/your-username/eCommerce_dpei.git
cd eCommerce_dpei
2️⃣ Setup Backend (ASP.NET Core API)
bash
نسخ
تحرير
cd backend
dotnet restore
dotnet ef database update
dotnet run
Runs at:

arduino
نسخ
تحرير
https://localhost:5001
http://localhost:5000
3️⃣ Setup Frontend (React.js)
bash
نسخ
تحرير
cd frontend
npm install
npm run dev
Runs at:

arduino
نسخ
تحرير
http://localhost:5173
Ensure React is configured to call backend at http://localhost:5000.

📡 API Base URL (used by React frontend)
arduino
نسخ
تحرير
http://localhost:5000
📚 API Endpoints Summary
🔐 Authentication
Method	Endpoint	Description
POST	/api/auth/register/customer	Register Customer
POST	/api/auth/login	Login & Get JWT
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
JWT: Authenticated access between React & backend API.

BCrypt: Secure password storage.

Role-Based Authorization: Admin-only restrictions.

ValidatorFilter: Custom input validation middleware (backend).

🌐 Full Stack User Flow Example
User registers/login in React UI → Backend issues JWT.

React saves JWT in local storage → used in all protected API calls.

User browses, adds products to cart in React UI → API /api/cart.

User checks out → React calls API /api/checkout/process.

Orders & stock managed by backend automatically.

Admin manages everything from React Admin Panel using backend APIs.

🤝 Contributing
bash
نسخ
تحرير
git checkout -b feature/your-feature
git commit -m "Add your feature"
git push origin feature/your-feature
Open a Pull Request.

📬 Contact
📧 osamaahmed52136@gmail.com

