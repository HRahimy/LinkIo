# LinkIo

## Prerequisites

The following is a set of prerequisites to run the application locally

- Windows OS
- Auth0 API and Client configured to work with
- Docker Desktop
- [MS SQL Database running in Docker container](#SQL_Database)

### Auth0 Configuration

For testing, the API and Angular application are pre-configured. No further steps are required.

### SQL Database

## Testing

Simply run `dotnet test` from the root directory of the project to run all tests.

The test suite contains a combination of Unit and Integration tests.

### Integration Tests Database Configuration

A set of integration tests have been implemented in the `Application.FunctionalTests` project.

By default, the integration tests will use a in-memory database constructed within code.

To use an actual SQL database for the Integration tests, a connection string must be provided. This can be provided either in the `appsettings.json` file. The connection string MUST be named `TestDefaultConnection`. 

Alternatively, you can provide the connection string via environment variables. The environment variable must be named `ConnectionStrings__TestDefaultConnection`.

Assuming you're using the database setup described in the [Prerequisites](#Prerequisites), you can provide the following connection string value

```
Server=127.0.0.1,1433;Database=LinkIoTestDb;User Id=SA;Password=Administrator1@;TrustServerCertificate=True;MultipleActiveResultSets=true
```