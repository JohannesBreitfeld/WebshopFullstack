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