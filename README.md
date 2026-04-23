# HotelMS

HotelMS is a backend hotel management system built with ASP.NET Core Web API using Clean Architecture.

The project is designed to manage core hotel operations through an API in a structured and maintainable way. It includes authentication and authorization with JWT, role-based access control, hotel and room management, booking operations, reviews, payments, invoice generation, request validation, and object mapping between entities and DTOs.

The architecture is separated into clear layers so that business logic, domain models, infrastructure concerns, and API endpoints remain organized and easier to maintain.

## What the Project Does

HotelMS allows the system to handle:

- user registration and login
- jwt authentication
- role-based authorization with jwt
- hotel management
- room management
- booking management
- review handling
- payment operations
- invoice generation after booking confirmation
- pdf invoice generation
- s3 service integration for invoice-related storage
- request validation with fluentvalidation
- object mapping with automapper

## Architecture

The project follows Clean Architecture and is divided into the following layers:

- HotelMS.Api – controllers, program configuration, swagger, authentication, dependency injection, and RabbitMQ consumer registration
- HotelMS.Application – services, DTOs, interfaces, mappings, validation, and business logic
- HotelMS.Domain – entities, enums, events, helpers, and core domain models
- HotelMS.Infrastructure – repositories, database persistence, security services, email service, and s3 service integration

## Main Technical Components

### Authentication and Authorization
The project uses JWT for authentication and supports role-based authorization to control access to protected endpoints.

### Validation
FluentValidation is used to validate incoming requests before business logic is executed.

### Mapping
AutoMapper is used to map entities and DTOs between layers.

### Invoice Generation
RabbitMQ is used to trigger invoice generation after booking confirmation, QuestPDF is used to generate invoice PDFs, and S3 service integration is used in the invoice-related workflow.

## Technologies

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication and Role-Based Authorization
- FluentValidation
- AutoMapper
- RabbitMQ / MassTransit
- QuestPDF
- Amazon S3
