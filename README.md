# LinkIo

You can reach the current hostings of the solution under:

- [Backend](https://linkioback.azurewebsites.net/api)
- [Frontend](https://happy-smoke-0ceb2ca10.5.azurestaticapps.net/)

To test user login, use:

- User: standard@linkio.com
- Password: Administrator1@

## Prerequisites

The following is a set of prerequisites to run the application locally

- Windows 10/11
- Visual Studio
- [NodeJs (v20.x)](https://nodejs.org/en)
- [.Net 8.0 SDK](https://dotnet.microsoft.com/en-us/download)
- [Auth0 API and Client configured to work with](#auth0-configuration)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [MS SQL Database running in Docker container](#sql-database)

### Auth0 Configuration

For testing, the API and Angular application are pre-configured. No further steps are required.

If you would like to use your own Auth0 tenant and services, a couple of changes need to be made

#### Backend

Configuration can either be provided via the `appsettings.json` in the `LinkIo.Web` project. The following are the default values:

```json
  "Auth0": {
    "Domain": "ngbridge.eu.auth0.com",
    "Audience": "https://linkio.com"
  },
```

Or via system environment variables:

- `Auth0__Domain`
- `Auth0__Audience`

> **Note:** If configured, system environment variables will take precedence over the `appsettings.json` file.

##### Auth0 API configuration

The Backend depends on the following configurations in your Auth0 API:

- A set of permissions:
  - `create:links`
  - `read:links`
  - `edit:links`
  - `delete:links`
- **RBAC** Settings -> `Enable RBAC` and `Add Permissions in the Access Token` enabled.

##### Auth0 User Roles

Add a Role (named e.g, LinkIo Standard Users) and give it all the permissions you created in your Auth0 API configuration

#### Frontend

Provide the following configurations in the `environment.ts` file:

```typescript
  ... other parameters
  auth0Domain: 'The Domain value from your Auth0 client',
  auth0ClientId: 'The Client ID value from your Auth0 client',
  auth0Audience: 'The Identifier value from your Auth0 API',
```

##### Auth0 Client configuration

The client must be a SPA client.

- Add these urls to the Allowed Callback URLs`https://localhost:44447, https://localhost:44447/links,`
- Add `https://localhost:44447` to the **Allowed Logout URLs**, and **Allowed Web Origins**

### SQL Database

This application was developed and tested using MS SQL server hosted on Docker Desktop locally. Use the following command to set up MS SQL server in a docker container

```shell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Administrator1@" -e "MSSQL_PID=Evaluation" -p 1433:1433  --name sqlpreview --hostname sqlpreview -d mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
```

> Alternative SQL Server installation methods will work with the application but the connection strings may need to change.

## Run the app

In Visual Studio, simply run the Web project, it will launch the backend and frontend.

## Testing

Simply run `dotnet test` from the root directory of the project to run all tests.

The test suite contains a combination of Unit and Integration tests.

### Integration Tests Database Configuration

A set of integration tests have been implemented in the `Application.FunctionalTests` project.

By default, the integration tests will use a in-memory database constructed within code.

To use an actual SQL database for the Integration tests, a connection string must be provided. This can be provided either in the `appsettings.json` file. The connection string MUST be named `TestDefaultConnection`. 

Alternatively, you can provide the connection string via environment variables. The environment variable must be named `ConnectionStrings__TestDefaultConnection`.

Assuming you're using the database setup described in the [Prerequisites](#sql-database), you can provide the following connection string value

```
Server=127.0.0.1,1433;Database=LinkIoTestDb;User Id=SA;Password=Administrator1@;TrustServerCertificate=True;MultipleActiveResultSets=true
```