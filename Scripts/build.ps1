cd (Join-Path $PSScriptRoot ..)
dotnet build --configuration Release Twia.AzureFunction.SimpleInjector.sln
dotnet test --no-build --configuration Release --collect "Code Coverage" .\Twia.AzureFunction.SimpleInjector.UnitTests\Twia.AzureFunction.SimpleInjector.UnitTests.csproj
