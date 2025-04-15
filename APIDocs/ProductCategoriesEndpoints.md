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
Authorization Required: JWT Token (Role: Admin)

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
Authorization Required: JWT Token (Role: Admin)

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
Authorization Required: JWT Token (Role: Admin)

#### Parameters
- `id` (integer) – The ID of the category to delete.

#### Response `204 No Content`

#### Response `404 Not Found`
```json
{
  "message": "Category with id 99 not found"
}
```