# Webshop API Specification

## General Information
**Version:** 1.0  
**Base URL:** `https://localhost:7279/api/`  
**Description:**  
Webshop API provides an interface for managing products, categories, customers, and orders in an e-commerce application. The API supports authentication via JWT tokens and provides CRUD operations for products, categories, customers, and orders.

---

## Endpoints
- [Authentication Endpoints (Login/Register)](AuthEndpoints.md)
- [Customer Endpoints](CustomerEndpoints.md)
- [Order Endpoints](OrderEndpoints.md)
- [ProductCategory Endpoints](ProductCategoriesEndpoints.md)
- [Product Endpoints](ProductEndpoints.md)

## Authentication Overview

All API endpoints requiring authentication are protected with JWT (JSON Web Token). To use these protected endpoints, you must first log in and obtain a JWT token, which is then used in every subsequent request.

#### **Steps to log in and obtain a JWT token:**

1. **Login:** Use `POST /api/auth/login` with the user's email and password.
2. **Token:** Upon successful login, a JWT token is returned in the response.
3. **Use token:** Include the received JWT token in the header of all subsequent API requests that require authentication. Use the format:

#### Authorization: Bearer<JWT_TOKEN>

#### **Token Structure:**

The JWT token consists of three parts: header, payload, and signature. It may contain information such as the user's ID and roles (e.g., "Admin" or "Customer") that are used to authorize the user for specific endpoints.

#### **Example of a Token:**

After logging in, you will receive a JWT token that can be used for authorized requests:

```json
{
"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI3NmRmNTE0ZS00OWFiLTQwZWUtODEyYi1mYTFjMWQ1NzIyMzQiLCJyb2xlIjoiQWRtaW4ifQ.Ji_F0WR5YYJYZB0pTTMQ_o8fEkwTADf8dp0K2Dh2A2E"
}
````
## Example Requests
1. Get a list of products (public endpoint):

    Method: GET

    URL: /api/products

   Example Request:

  ````
  curl -X GET "https://localhost:7279/api/products"
  ````

2. Add a product (requires JWT token):

    Method: POST

    URL: /api/products

    Authorization: Bearer Token (example of token in the request header)

    Example Request:

````
curl -X POST "https://localhost:7279/api/products" \
-H "Authorization: Bearer <JWT_TOKEN>" \
-H "Content-Type: application/json" \
-d '{"name": "Sako 90 Adventure", "category": "Rifles", "price": 3000}'
````