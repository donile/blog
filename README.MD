# markdonile.com blog

## Setup
Add environment variable `POSTGRES_PASSWORD`
Add environment variable `ASPNETCORE_CONNECTIONSTRING`

## Start PostgreSQL Database Container
`> docker run --name postgres-13.1 -e POSTGRES_PASSWORD="${POSTGRES_PASSWORD}" -d postgres:13.1-alpine`

## Dependencies
[Coverlet](https://github.com/coverlet-coverage/coverlet)

[ReportGenerator](https://github.com/danielpalme/ReportGenerator)

### Install Dependencies

`project-root> dotnet tool restore`

## How to Execute Tests

### Execute Tests

`project-root> dotnet test`

### Execute Tests And Generate Test Coverage Report

`project-root> dotnet test ./tests/Blog.Tests/Blog.Tests.csproj --collect:"XPlat Code Coverage" --settings:"TestCoverage.runsettings"`

### Generate Test Coverage Report

```powershell
project-root> dotnet reportgenerator 
    "-reports:Path\To\TestProject\TestResults\{guid}\coverage.cobertura.xml"
    "-targetdir:coveragereport" 
    -reporttypes:Html
```