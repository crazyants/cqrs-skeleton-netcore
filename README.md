## Uranus Commercial (ASP.NET Core 1.0) ##

A backend CQRS Solution (Command Query Separation).

## Overview ##

- **Uranus.Commercial.Api**: ASP.NET Core WebApi
- **Uranus.Commercial.CommandStack**: This is the layer where we have commands, async command handlers, events.
- **Uranus.Commercial.CommandStack**.Domain: Business Model and Domain Entities. In this layer we will use EntityFrameworkCore. We avoid anemic domain models.
- **Uranus.Commercial.Infrastructure**: Cross Support Layer.
- **Uranus.Commercial.QueryStack**: The read model. This layer has its own way to get information from database. Normally, this will consume materialized views, or denormalized database.
- **Uranus.Commercial.Services**: Application services. Orchestrate use cases and provide an entry point to the domain layer. Manage transactions.

## Technology ##

- **Mediatr**: .NET Core library for event async handling.
- **Entity Framwork Core**: Database ORM.