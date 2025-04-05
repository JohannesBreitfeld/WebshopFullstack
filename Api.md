# Webshop.API Specification

````
  Title: Webshop.API
  Version: 1.0
  ````

## Authentication

### <span style="color:green">POST</span> /api/auth/register

**Description**:  
Registers a new user.

**Request Body**:
```json
{
    "password" (string): The user's password.
    "customer" (object): Customer details.
    {
        "firstName" (string): First name.
        "lastName" (string): Last name.
        "email" (string): Email address.
        "streetAddress" (string): Street address.
        "postalCode" (integer): Postal code.
        "city" (string): City.
    }
}
```
**Responses**:

- **200 OK**: Registration successful.
  - *Response Body*:
    ```json
    {
        "token": "jwt-token",
        "customer": {
            "id": 15,
            "firstName": "string",
            "lastName": "string",
            "email": "string",
            "streetAdress": "string",
            "postalCode": 12345,
            "city": "string"
            }
    }
    ````
   
- **400 Bad Request** – Missing or invalid data (e.g., password too short, email already in use).
- **500 Internal Server Error** – Something went wrong on the server.

### <span style="color:green">POST</span> /api/auth/login

**Description**:  
Authenticates a user.

**Request Body**:
```json
{
    "email" (string): User's email address.
    "password" (string): User's password.
}
```
**Responses**:

- **200 OK**: Authentication successful.
  - *Response Body*:
    ```json
    {
        "token": "jwt-token",
        "customer": {
            "id": 15,
            "firstName": "string",
            "lastName": "string",
            "email": "string",
            "streetAdress": "string",
            "postalCode": 12345,
            "city": "string"
            }
    }
    ```
- **401 Unauthorized** – Missing or invalid data (e.g., password is incorrect or no corresponding email can be found).
- **500 Internal Server Error** – Something went wrong on the server.

## Customers

### <span style="color:blue">GET</span> /api/Customers

**Description**:  
Retrieves a list of customers.

**Responses**:

- **200 OK**: Successfully retrieved list.
  - *Response Body*:
    ```json
    {
    "items": [
        {
        "id": 1,
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "streetAdress": "string",
        "postalCode": 0,
        "city": "string"
        },
        {
        "id": 2,
        "firstName": "string",
        "lastName": "string",
        "email": "string@string.com",
        "streetAdress": "string",
        "postalCode": 43933,
        "city": "string"
        },
        ...
        ]
    }
    ```

### <span style="color:green">POST</span> /api/Customers

**Description**:  
Creates a new customer.

**Request Body**:
```json
{
    "firstName" (string): First name.
    "lastName" (string): Last name.
    "email" (string): Email address.
    "streetAddress" (string): Street address.
    "postalCode" (integer): Postal code.
    "city" (string): City.
}
````
**Responses**:

- **200 OK**: Customer created successfully.
  - *Response Body*:
    ```json
    {
        "id": 16,
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "streetAdress": "string",
        "postalCode": 0,
        "city": "string"
    }   
    ```
- **400 Bad Request**:
The customer could not be created, possibly due to invalid input data or a conflict.
   - *Response Body*:
        ```Json
        {
            "message": "Failed to create customer"
        }
        ````
- **500 Internal Server Error** – Something went wrong on the server.
### <span style="color:blue">GET</span> /api/Customers/{id}

**Description**:  
Retrieves details of a specific customer.

**Path Parameters**:

- `id` (integer): Customer ID.

**Responses**:

- **200 OK**: Successfully retrieved customer details.
  - *Response Body*:
   ```json
    {
        "id": 16,
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "streetAdress": "string",
        "postalCode": 0,
        "city": "string"
    }   
    ```
- **404 Not Found**: Id matching with path parameter could not be found.
- **500 Internal Server Error** – Something went wrong on the server.

### <span style="color:orange">PUT</span> /api/Customers/{id}

**Description**:  
Updates details of a specific customer.

**Path Parameters**:

- `id` (integer): Customer ID.

**Request Body**:
```json
{
    "firstName" (string): First name.
    "lastName" (string): Last name.
    "email" (string): Email address.
    "streetAddress" (string): Street address.
    "postalCode" (integer): Postal code.
    "city" (string): City.
}
````
**Responses**:

- **200 OK**: Successfully retrieved customer details.
  - *Response Body*:
   ```json
    {
        "id": 16,
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "streetAdress": "string",
        "postalCode": 0,
        "city": "string"
    }   
    ```
- **404 Not Found**: Id matching with path parameter could not be found.
- **500 Internal Server Error** – Something went wrong on the server.

### <span style="color:red">DELETE</span> /api/Customers/{id}

**Description**:  
Deletes a specific customer.

**Path Parameters**:

- `id` (integer): Customer ID.

**Responses**:

- **204 No content**: Customer deleted successfully.
- **404 Not Found**: Id matching with path parameter could not be found.
- **500 Internal Server Error** – Something went wrong on the server.

# 📘 Orders API

Base URL: `/api/orders`

## Endpoints

---

### 🔹 `GET /api/orders`

Retrieve a list of all orders.

#### Response `200 OK`
```json
[
  {
    "id": 1,
    "customerId": 4,
    "dateTime": "2025-03-29T15:30:46.857Z",
    "products": [
      {
        "productId": 5,
        "productName": "Sako 90 Adventure",
        "quantity": 1
      }
    ]
  }
]
```

---

### 🔹 `POST /api/orders`

Create a new order.

#### Request Body
```json
{
  "customerId": 4,
  "products": [
    {
      "productId": 5,
      "quantity": 1
    }
  ]
}
```

#### Response `201 Created`
```json
{
  "id": 3,
  "customerId": 4,
  "dateTime": "2025-03-31T11:03:16.338Z",
  "products": [
    {
      "productId": 5,
      "productName": "Sako 90 Adventure",
      "quantity": 1
    }
  ]
}
```

#### Response `400 Bad Request`
```json
{
  "message": "Failed to create order"
}
```

---

### 🔹 `GET /api/orders/{orderId}`

Retrieve a single order by order ID.

#### Parameters
- `orderId` (integer) – The ID of the order.

#### Response `200 OK`
```json
{
  "id": 2,
  "customerId": 4,
  "dateTime": "2025-03-31T09:58:12.967Z",
  "products": [
    {
      "productId": 5,
      "productName": "Sako 90 Adventure",
      "quantity": 1
    }
  ]
}
```

#### Response `404 Not Found`
```json
{
  "message": "Could not find order with id 99"
}
```

---

### 🔹 `GET /api/orders/by-customer/{customerId}`

Retrieve all orders placed by a specific customer.

#### Parameters
- `customerId` (integer) – The ID of the customer.

#### Response `200 OK`
```json
[
  {
    "id": 1,
    "customerId": 4,
    "dateTime": "2025-03-29T15:30:46.857Z",
    "products": [
      {
        "productId": 5,
        "productName": "Sako 90 Adventure",
        "quantity": 1
      }
    ]
  },
  {
    "id": 3,
    "customerId": 4,
    "dateTime": "2025-03-31T11:03:16.338Z",
    "products": [
      {
        "productId": 4,
        "productName": "Mauser M25 Max Kulgevär",
        "quantity": 1
      }
    ]
  }
]
```

#### Response `404 Not Found`
```json
{
  "message": "Could not find any orders with for customer id 99"
}
```
# 📘 Product Categories API

Base URL: `/api/productcategories`

## Endpoints

---

### 🔹 `GET /api/productcategories`

Retrieve a list of all product categories.

#### Response `200 OK`
```json
[
  {
    "id": 1,
    "name": "Rifles"
  },
  {
    "id": 2,
    "name": "Scopes"
  }
]
```

---

### 🔹 `GET /api/productcategories/{id}`

Retrieve a single product category by ID.

#### Parameters
- `id` (integer) – The ID of the category.

#### Response `200 OK`
```json
{
  "id": 1,
  "name": "Rifles"
}
```

#### Response `404 Not Found`
```json
{
  "message": "Product category with id 99 not found"
}
```

---

### 🔹 `POST /api/productcategories`

Create a new product category.

#### Request Body
```json
{
  "name": "Ammunition"
}
```

#### Response `201 Created`
```json
{
  "id": 3,
  "name": "Ammunition"
}
```

#### Response `400 Bad Request`
```json
{
  "message": "Failed to create product category"
}
```

---

### 🔹 `PUT /api/productcategories/{id}`

Update an existing product category.

#### Parameters
- `id` (integer) – The ID of the category to update.

#### Request Body
```json
{
  "name": "Updated Category Name"
}
```

#### Response `200 OK`
```json
{
  "id": 2,
  "name": "Updated Category Name"
}
```

#### Response `404 Not Found`
```json
{
  "message": "Category with id 99 not found"
}
```

---

### 🔹 `DELETE /api/productcategories/{id}`

Delete a product category by ID.

#### Parameters
- `id` (integer) – The ID of the category to delete.

#### Response `204 No Content`

#### Response `404 Not Found`
```json
{
  "message": "Category with id 99 not found"
}
```
# 📘 Products API

Base URL: `/api/products`

## Endpoints

---

### 🔹 `GET /api/products`

Retrieve a list of all products.

#### Response `200 OK`
```json
[
  {
    "id": 1,
    "name": "Sako 90 Adventure",
    "category": "Rifles",
    "price": 3000
  },
  {
    "id": 2,
    "name": "Mauser M25",
    "category": "Rifles",
    "price": 2500
  }
]
```

---

### 🔹 `GET /api/products/{id}`

Retrieve a single product by ID.

#### Parameters
- `id` (integer) – The ID of the product.

#### Response `200 OK`
```json
{
  "id": 1,
  "name": "Sako 90 Adventure",
  "category": "Rifles",
  "price": 3000
}
```

#### Response `404 Not Found`
```json
{
  "message": "Product with id 99 not found"
}
```

---

### 🔹 `GET /api/products/by-name/{name}`

Retrieve a product by its name.

#### Parameters
- `name` (string) – The name of the product.

#### Response `200 OK`
```json
{
  "id": 1,
  "name": "Sako 90 Adventure",
  "category": "Rifles",
  "price": 3000
}
```

#### Response `404 Not Found`
```json
{
  "message": "Product with name Sako 90 Adventure not found"
}
```

---

### 🔹 `POST /api/products`

Create a new product.

#### Request Body
```json
{
  "name": "Sako 90 Adventure",
  "category": "Rifles",
  "price": 3000
}
```

#### Response `201 Created`
```json
{
  "id": 3,
  "name": "Sako 90 Adventure",
  "category": "Rifles",
  "price": 3000
}
```

#### Response `400 Bad Request`
```json
{
  "message": "Failed to create product"
}
```

---

### 🔹 `PUT /api/products/{id}`

Update an existing product by ID.

#### Parameters
- `id` (integer) – The ID of the product to update.

#### Request Body
```json
{
  "name": "Updated Sako 90 Adventure",
  "category": "Rifles",
  "price": 3500
}
```

#### Response `200 OK`
```json
{
  "id": 1,
  "name": "Updated Sako 90 Adventure",
  "category": "Rifles",
  "price": 3500
}
```

#### Response `404 Not Found`
```json
{
  "message": "Product with id 99 not found"
}
```

---

### 🔹 `DELETE /api/products/{id}`

Delete a product by ID.

#### Parameters
- `id` (integer) – The ID of the product to delete.

#### Response `204 No Content`

#### Response `404 Not Found`
```json
{
  "message": "Product with id 99 not found"
}
```
