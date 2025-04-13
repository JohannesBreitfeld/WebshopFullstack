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