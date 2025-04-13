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