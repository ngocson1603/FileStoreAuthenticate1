# Test Plan for FileStore Application

## Overview
This document outlines the testing strategy for the FileStore ASP.NET Core application with JWT authentication.

## Test Scope

### 1. Authentication Tests
- **JWT Token Generation**
  - Verify token is generated with correct claims
  - Verify token expiration is set to 24 hours
  - Verify token signature is valid
  
- **Login Endpoint** (`POST /api/auth/login`)
  - Valid credentials return token
  - Invalid credentials return error message
  - Response format matches ApiResponse structure

### 2. Authorization Tests
- **JwtAuthorizeFilter**
  - Verify protected endpoints require valid JWT token
  - Verify expired tokens are rejected
  - Verify invalid token signatures are rejected

### 3. WebSocket Tests
- **WebSocket Connection** (`/api/ws`)
  - Verify connection establishes successfully
  - Verify server can send real-time data
  - Verify connection closes properly
  - Test with testWebsocket.html client

### 4. Integration Tests
- User authentication flow
- File manager operations
- Quote service operations
- Real-time data updates via WebSocket

## Testing Tools & Framework
- **Unit Tests**: xUnit or NUnit
- **Integration Tests**: TestHost/WebApplicationFactory
- **API Tests**: HttpClient

## Critical Test Cases

### Authentication Flow
1. User logs in with valid username/password
2. Server returns JWT token
3. User includes token in Authorization header
4. Protected endpoints verify token validity

### Error Handling
1. SQL Server connection failures
2. Invalid/expired tokens
3. Missing authorization headers
4. Malformed JWT tokens

## Current Build Status
- Target Framework: .NET 7.0
- Dependencies: EF Core, JWT Bearer, Swagger, WebSockets

## Notes
- Password storage should use hashing (consider bcrypt)
- Connection string is hardcoded in Program.cs (should use configuration)
- Missing unit test project in solution
