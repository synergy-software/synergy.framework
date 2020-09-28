# Manage users through API

1. [Get empty list of users](#1-get-empty-list-of-users-1-step)
1. [Create a user](#2-create-a-user-2-steps)
1. [Get user](#3-get-user-2-steps)
1. [Get list of users](#4-get-list-of-users-1-step)
1. [Try to create user without login](#5-try-to-create-user-without-login-3-steps)
1. [Delete user](#6-delete-user-2-steps)

## 1. Get empty list of users (1 step)

| # | Step Actions | Status |
| - | - | - |
| 1 | Retrieve users | OK |

### 1.1. Retrieve users (1 request)

### 1.1.1. Request to [Get list of users]

- Request
```
GET  /api/v1/users
```

- Response
```
HTTP/1.1 200 OK
api-supported-versions: 1.0
Content-Type: application/json; charset=utf-8
Content-Length: 19
{
  "users": []
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is GET | OK |
| Convention: Returned HTTP status code is 200 (OK) | OK |
| Convention: Returned HTTP Content-Type is "application/json" | OK |
| Manual: Empty users list is returned | OK |



## 2. Create a user (2 steps)

| # | Step Actions | Status |
| - | - | - |
| 1 | Create new user | OK |
| 2 | Get created user pointed by "Location" header | OK |

### 2.1. Create new user (1 request)

### 2.1.1. Request to [Create a new user with login marcin@synergy.com]

- Request
```
POST  /api/v1/users
Content-Type: application/json; charset=utf-8
{
  "Login": {
    "Value": "marcin@synergy.com"
  }
}
```

- Response
```
HTTP/1.1 201 Created
Location: http://localhost/api/v1/users/aa1311498cda4bb6a3b0d1441d32df3b
api-supported-versions: 1.0
Content-Type: application/json; charset=utf-8
Content-Length: 129
{
  "user": {
    "id": "aa1311498cda4bb6a3b0d1441d32df3b",
    "login": {
      "value": "marcin@synergy.com"
    }
  }
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is POST | OK |
| Convention: Returned HTTP status code is 201 (Created) | OK |
| Convention: Location header (pointing to newly created element) is returned with response. | OK |
| Convention: Returned HTTP Content-Type is "application/json" | OK |
| Manual: User is created and its details are returned | OK |


### 2.2. Get created user pointed by "Location" header (1 request)

### 2.2.2. Request to [Get user located at http://localhost/api/v1/users/92244f5bb3fd470eb7551f2f1a7428ce]

- Request
```
GET  /api/v1/users/d08a07d9020943c3893309579e0c76c8
```

- Response
```
HTTP/1.1 200 OK
api-supported-versions: 1.0
Content-Type: application/json; charset=utf-8
Content-Length: 129
{
  "user": {
    "id": "d08a07d9020943c3893309579e0c76c8",
    "login": {
      "value": "marcin@synergy.com"
    }
  }
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is GET | OK |
| Convention: Returned HTTP status code is 200 (OK) | OK |
| Convention: Returned HTTP Content-Type is "application/json" | OK |
| Manual: User details are returned | OK |



## 3. Get user (2 steps)

| # | Step Actions | Status |
| - | - | - |
| 1 | Get user by id | OK |
| 2 | Negative test: Try to get user that do not exist | OK |

### 3.1. Get user by id (1 request)

### 3.1.1. Request to [Get user with id "92244f5bb3fd470eb7551f2f1a7428ce"]

- Request
```
GET  /api/v1/users/d08a07d9020943c3893309579e0c76c8
```

- Response
```
HTTP/1.1 200 OK
api-supported-versions: 1.0
Content-Type: application/json; charset=utf-8
Content-Length: 129
{
  "user": {
    "id": "d08a07d9020943c3893309579e0c76c8",
    "login": {
      "value": "marcin@synergy.com"
    }
  }
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is GET | OK |
| Convention: Returned HTTP status code is 200 (OK) | OK |
| Convention: Returned HTTP Content-Type is "application/json" | OK |
| Manual: User details are returned | OK |


### 3.2. Negative test: Try to get user that do not exist (1 request)

### 3.2.2. Request to [Get user with id "user-id-that-do-not-exist"]

- Request
```
GET  /api/v1/users/user-id-that-do-not-exist
```

- Response
```
HTTP/1.1 404 NotFound
api-supported-versions: 1.0
Content-Type: application/problem+json
{
  "type": "https://httpstatuses.com/404",
  "title": "User with id user-id-that-do-not-exist does not exist",
  "status": 404,
  "traceId": "0HLSPQATK73P7"
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is GET | OK |
| Convention: Returned HTTP status code is 404 (NotFound) | OK |
| Convention: Returned HTTP Content-Type is "application/problem+json" | OK |
| Manual: No user details are returned and 404 error (not found) is returned instead | OK |



## 4. Get list of users (1 step)

| # | Step Actions | Status |
| - | - | - |
| 1 | Retrieve users | OK |

### 4.1. Retrieve users (1 request)

### 4.1.1. Request to [Get list of users]

- Request
```
GET  /api/v1/users
```

- Response
```
HTTP/1.1 200 OK
api-supported-versions: 1.0
Content-Type: application/json; charset=utf-8
Content-Length: 152
{
  "users": [
    {
      "id": "d08a07d9020943c3893309579e0c76c8",
      "login": {
        "value": "marcin@synergy.com"
      }
    }
  ]
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is GET | OK |
| Convention: Returned HTTP status code is 200 (OK) | OK |
| Convention: Returned HTTP Content-Type is "application/json" | OK |
| Manual: Users list is returned | OK |



## 5. Try to create user without login (3 steps)

| # | Step Actions | Status |
| - | - | - |
| 1 | Negative test: Create user with a null login | OK |
| 2 | Negative test: Create user with an empty login | OK |
| 3 | Negative test: Create user with a whitespace login | OK |

### 5.1. Negative test: Create user with a null login (1 request)

### 5.1.1. Request to [Create a new user with login null]

- Request
```
POST  /api/v1/users
Content-Type: application/json; charset=utf-8
{
  "login": null
}
```

- Response
```
HTTP/1.1 400 BadRequest
Content-Type: application/json; charset=utf-8
Content-Length: 351
{
  "errors": {
    "login": [
      "Error converting value {null} to type 'Synergy.Sample.Web.API.Services.Users.Domain.Login'. Path 'login', line 1, position 13."
    ]
  },
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "0HM2G07BRCKOL"
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is POST | OK |
| Convention: Returned HTTP status code is 400 (BadRequest) | OK |
| Convention: error JSON contains "title" node | OK |
| Convention: error JSON contains "traceId" node | OK |
| Convention: There is NO "Location" header returned in response | OK |
| Manual: User is NOT created and error is returned | OK |


### 5.2. Negative test: Create user with an empty login (1 request)

### 5.2.2. Request to [Create a new user with login ""]

- Request
```
POST  /api/v1/users
Content-Type: application/json; charset=utf-8
{
  "login": ""
}
```

- Response
```
HTTP/1.1 400 BadRequest
Content-Type: application/json; charset=utf-8
Content-Length: 349
{
  "errors": {
    "login": [
      "Error converting value \"\" to type 'Synergy.Sample.Web.API.Services.Users.Domain.Login'. Path 'login', line 1, position 11."
    ]
  },
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "0HM2G07BRCKOM"
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is POST | OK |
| Convention: Returned HTTP status code is 400 (BadRequest) | OK |
| Convention: error JSON contains "title" node | OK |
| Convention: error JSON contains "traceId" node | OK |
| Convention: There is NO "Location" header returned in response | OK |
| Manual: User is NOT created and error is returned | OK |


### 5.3. Negative test: Create user with a whitespace login (1 request)

### 5.3.3. Request to [Create a new user with login "  "]

- Request
```
POST  /api/v1/users
Content-Type: application/json; charset=utf-8
{
  "login": "  "
}
```

- Response
```
HTTP/1.1 400 BadRequest
Content-Type: application/json; charset=utf-8
Content-Length: 351
{
  "errors": {
    "login": [
      "Error converting value \"  \" to type 'Synergy.Sample.Web.API.Services.Users.Domain.Login'. Path 'login', line 1, position 13."
    ]
  },
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "0HM2G07BRCKON"
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is POST | OK |
| Convention: Returned HTTP status code is 400 (BadRequest) | OK |
| Convention: error JSON contains "title" node | OK |
| Convention: error JSON contains "traceId" node | OK |
| Convention: There is NO "Location" header returned in response | OK |
| Manual: User is NOT created and error is returned | OK |



## 6. Delete user (2 steps)

| # | Step Actions | Status |
| - | - | - |
| 1 | Delete user by id | OK |
| 2 | Try to get the deleted user | OK |

### 6.1. Delete user by id (1 request)

### 6.1.1. Request to [Delete user with id "92244f5bb3fd470eb7551f2f1a7428ce"]

- Request
```
DELETE  /api/v1/users/6f5543256b314387853b9df16282d8d4
```

- Response
```
HTTP/1.1 200 OK
api-supported-versions: 1.0
Content-Type: application/json; charset=utf-8
Content-Length: 2
{}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is DELETE | OK |
| Convention: Returned HTTP status code is 200 (OK) | OK |
| Convention: Returned HTTP Content-Type is "application/json" | OK |
| Manual: User is deleted and no details are returned | OK |


### 6.2. Try to get the deleted user (1 request)

### 6.2.2. Request to [Get user with id "92244f5bb3fd470eb7551f2f1a7428ce"]

- Request
```
GET  /api/v1/users/794e21cf23fa443d8222544b82ce367a
```

- Response
```
HTTP/1.1 404 NotFound
api-supported-versions: 1.0
Content-Type: application/problem+json
{
  "type": "https://httpstatuses.com/404",
  "title": "User with id 794e21cf23fa443d8222544b82ce367a does not exist",
  "status": 404,
  "traceId": "0HLSPQGI8OPFR"
}
```

| Expected Results  | Status |
| - | - |
| Convention: HTTP request method is GET | OK |
| Convention: Returned HTTP status code is 404 (NotFound) | OK |
| Convention: Returned HTTP Content-Type is "application/problem+json" | OK |
| Manual: User is not found and error is returned | OK |


