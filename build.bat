cls
set initialPath=%cd%
set srcPath=%cd%\CatFactory.PostgreSql
set testPath=%cd%\CatFactory.PostgreSql.Tests
cd %srcPath%
dotnet build
cd %testPath%
dotnet test
cd %srcPath%
dotnet pack
cd %initialPath%
pause
