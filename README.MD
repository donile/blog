# markdonile.com blog

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