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
Authorization Required: JWT Token (Role: Admin)

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
Authorization Required: JWT Token (Role: Admin)

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
### 🔹 `DELETE /api/products/soft-delete/{id}`

Sets SoftDeleted proerty to true for product by ID.  
Authorization Required: JWT Token (Role: Admin)

#### Parameters
- `id` (integer) – The ID of the product to soft delete.

#### Response `204 No Content`

#### Response `404 Not Found`
```json
{
  "message": "Product with id 99 not found"
}
```
---

### 🔹 `DELETE /api/products/{id}`

Delete a product by ID.  
Authorization Required: JWT Token (Role: Admin)

#### Parameters
- `id` (integer) – The ID of the product to delete.

#### Response `204 No Content`

#### Response `404 Not Found`
```json
{
  "message": "Product with id 99 not found"
}
```