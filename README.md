# Backend API Documentation

## Introduction

This API helps manage commerce information for businesses. It makes it easier to add, update, and get data about categories, customers, orders, order-products, products and product-categories.

In this document, we will talk about:

- **Design Patterns Used**
- **Basic Requirements**
- **How to Install**

## Design Patterns Used

- Repository Pattern: Abstraction of the logic Access Data
- Cqrs Pattern: Separates read and write operations in an application
- Mediator Pattern: Reduce direct dependencies between objects

## Basic Requirements

Before you begin, ensure that your system meets the following prerequisites:

**Running locally with Visual Studio 2022**
- Windows 10 installed
- At least 4 GB of RAM (8 GB or more recommended)
- Sufficient disk space to install the .NET SDK, development tools, and project dependencies (at least 10 GB of free space recommended)
- NET 8 SDK installed. Download and install the .NET 8 SDK from the official .NET website: https://dotnet.microsoft.com/download/dotnet/8.0
- Docker or Docker Desktop installed https://www.docker.com/
- docker-compose
- Git (version control): [Download from Git](https://git-scm.com/)
- Visual Studio 2022 Community Edition installed

**Running locally with Docker Desktop**
- Docker or Docker Desktop installed https://www.docker.com/
- docker-compose
- Git (version control): [Download from Git](https://git-scm.com/)

## How to Install

**Clone the repository**
```sh
git clone https://github.com/elmauro/commerce-back.git
```

Create a network for backend and front-end

```sh
docker network create commerce-network
``` 

Use Docker Compose to launch the database

```sh
docker-compose -f compose.yml up --build -d commerce-mssql
```

```sh
dotnet tool install --global dotnet-ef
```

```sh
dotnet-ef database update --project src/MC.CommerceService.API
```

![image](https://github.com/user-attachments/assets/44a71cb9-2bdd-44a5-8bc2-ff0e6c5b01f2)


## Database Diagram

![image](https://github.com/user-attachments/assets/9c5619a1-350c-4400-8bef-fe66f0fcdf7d)



## Starting the Service

You can start the service using Visual Studio 2022 or Docker Compose

**With Docker Compose**

```sh
docker-compose -f compose.yml up --build -d commerce-back
```

![image](https://github.com/user-attachments/assets/5540c4f4-4c8f-431f-8702-3d85e7ba8c68)

## Using the Commerce Service API

**API Access**

The API can be accessed locally at:

```sh
http://localhost:56508/commerce/index.html
```

The API is fully documented using Swagger, which provides a detailed overview of available endpoints, request models, and response statuses. Please review the Swagger documentation to validate response HTTP statuses and understand the expected behavior of the API

![image](https://github.com/user-attachments/assets/f4949758-88bf-4e72-8a36-7d49711f904c)


**Important!**
The response for POST methods includes the automatic ID created in the location section of the response headers. You need the ID to update and access information.

Sample with the Product method POST

![image](https://github.com/user-attachments/assets/22f7a1ca-a0cd-4555-bc9b-428491b859b0)


## Logs

You can view the logs from within the Docker container using the following command

```sh
docker logs [container_id]
```

![image](https://github.com/user-attachments/assets/e5266262-02cb-4692-8255-94314e0333a4)

![image](https://github.com/user-attachments/assets/03ae4cf4-38d7-40ed-a6fe-985518a4b672)
