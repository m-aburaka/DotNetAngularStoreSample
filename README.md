# DotNetAngularStoreSample

## Overview

- DotNetAngularStoreSample.Models - models, data transfer objects, exceptions and definition of request classes
- DotNetAngularStoreSample.Application - business logic, including domain services, logic of request handlers, and definition of database repository interfaces
- DotNetAngularStoreSample.Repository.Ef - implementation of repository interfaces for EntityFrameworkCore, EF DB Context, and migrations
- DotNetAngularStoreSample.Server - ASP .NET Core Web API server, it's controllers, and Angular SPA
- DotNetAngularStoreSample.Server.Tests - Tests for the controllers and basic integration tests

## Main tools and libraries

- Autofac - Dependency Injection container
- MediatR - implementation of mediator pattern
- AutoMapper - maps models to data transfer objects
- Serilog - logger
- Xunit - testing framework

## Back-end Architecture

### Dependency Injection

My DI container of choice is Autofac, which is more flexible than ASP .NET Core default DI container, with additional benefit of being separated from the ASP .NET Core framework.
All services, repositories and Request Handlers should be registered in a DI container, usually by scanning an assembly for every "...Service" and "...Repository" class, and registering it in the container during the initialization.
To separate registration of business logic and registration of infrastructure, I use Autofac modules. Each module is responsible for registering a particular slice of the app.
Modules and their helper classes can be found in IoC folders in the following projects

- DotNetAngularStoreSample.Application
- DotNetAngularStoreSample.Server
- DotNetAngularStoreSample.Server.Tests

### Mediator pattern

To make controllers lean and separate all the business logic, I use CQRS-like approach.
On business action, like 'Add Purchase', the Web API should receive a POST request with a request class, containing information about action to be executed.
The request class should implement IRequest interface, or IRequest<TResponse,> in case the request should issue a response.

The Web API controller then sends the request to MediatR library, which in turn executes Handle method in appropriate Request Handler class.
The request handler class should implement IRequestHandler<IRequest,>, or IRequestHandler<IRequest, TResponse> in case the request should issue a response.

MediatR determines which Request Handler is responsible for current request by IRequest and IRequestHandler interfaces.

All the services, infrastructure and repositories should be injected in Request Handler's constructor by Dependency Injection container. In our case it's Autofac.
RequestHandlers themselves should also be registered in the DI container, usually by automatically scanning the assembly and registering every class which implements IRequestHander.

### Data Transfer Objects

DTO's are defined in the DotNetAngularStoreSample.Models project, and represent all information needed to execute a request on particular model object.
To encapsulate a model, DTO's include fields only relevant to a request. All other fields and properties are excluded from DTO.

AutoMapper library allows to convert model to DTO with minimum of additional code.
Mapper can be injected in a request handler from DI container.
Definition of mapping profiles are in MapperFactory class.

### Repository pattern

To separate the data layer, and invert dependency from "Business logic depends on Data Layer" to "Data Layer depends on business logic", I use the Repository pattern.
Dependency Inversion is needed to separate the application from a particular database, and make databases and ORMs easily replaceable.

IRepository interfaces defined in the same project as the domain logic (DotNetAngularStoreSample.Application). The interfaces describe how the data layer for particular model should be implemented: how to get, insert and delete a model from DB.
The concrete implementation of data layer will be in separate project (e.g. DotNetAngularStoreSample.Repository.Ef).

## Exceptions

Exception handling is centralized: all exceptions thrown by request handlers are processed ExceptionMiddlewareExtensions, which employes ExceptionHandlerService to log the exception, and then serialize it to make it easy transfer them back to the client.

Exceptions thrown by request handlers should be inherited from ServerException, which will contain message, stack trace and HTTP error code.

Unhandled exceptions, and other exceptions thrown by request handlers, will be treated as InternalServerException with HTTP code 500, and returned to a client with message, stack trace, and response code 500

## Workflow and testng

I employ TDD: first I create a test for controller's action in DotNetAngularStoreSample.Server.Tests. Tests should be isolated, and include basic scenarios on what that particular action should do.
After I define the tests and scenarios, I create request handler for this action.