# Application Overview

This application is created for educational purposes. It demonstrates the **Clean Architecture** pattern in an e-commerce platform.

---

## Application Goal

The goal of this application is to implement a basic e-commerce platform that provides core online shop functionality.

---

# Authentication

Authentication is implemented using **JWT (JSON Web Tokens)** with two types of tokens:

## Access Token
- Lifetime: 15 minutes  
- Payload:
  - `userId`
  - `role`

## Refresh Token
- Lifetime: 1 day  
- Payload: empty  
- Stored in a relational database  

---

# Role System

The system supports two types of users:

## Content Manager
- Manage products using full CRUD operations (Create, Read, Update, Delete)
- Responsible for maintaining the product catalog

## Client
- View product listings
- Use pagination for browsing products

---

# Cart Feature

The cart feature works for both authenticated and unauthenticated users.

## Key Behavior
- Users can add products to the cart without registration or login
- Each cart is identified by a unique ID stored in browser cookies
- A cookie is created when the user adds the first product to the cart

## Maintenance
- A CRON job removes inactive carts
- Carts not modified for 7 days are automatically deleted

## Checkout Interaction
- When checkout is completed, the cart is deleted
- Cart data is archived as user purchase history

---

# Checkout Feature

Checkout is available only when:
- The cart is not empty
- The user is logged in (client role)

## Behavior
- Cart data is saved as purchase history during checkout

## Access Rules

### Content Manager
- Can view all user checkouts

### Client
- Can view only their own checkout history