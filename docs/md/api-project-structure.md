# Structure

-   `src`
    -   `Client`
        -   `Web`
            -   UI
    -   `Server`
        -   `Host`
            -   Api
        -   `Modules`
            -   Workflow
                -   `Api`
                    -   Controllers
                    -   Extensions
                -   `Core`
                    -   Entities
                    -   Interfaces
                    -   Exceptions
                    -   Handlers
                    -   Commands
                    -   Queries
                -   `Infrastructure`
                    -   Context
                    -   Persistence
        -   `Shared`
            -   `Core`
                -   Domain
                -   Features
                -   Interfaces
                -   Exceptions
                -   Logging
                -   Services
                -   Serialization
                -   Wrapper
            -   `DTOs`
                -   Request
                -   Response
            -   `Infrastructure`
                -   Middlewares
                -   Persistence
                -   Services
                -   Mappings
                -   Utilities
                -   Swagger
-   `tests`
    -   `FunctionalTests`
        -   ControllerApis
    -   `IntegrationTests`
        -   Data
    -   `UnitTests`
        -   Core
