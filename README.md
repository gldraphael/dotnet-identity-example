# ASP.NET Core Identity Example

This project complements my blog post: <>

## Setting up your dev environment and this project solution

* Install Visual Studio or any text editor of your choice like VS Code
* Install MySql and create a new database (you can also use postgres or SQL Server etc; refer to my [blog post](appsettings.Development.example.json) or [Microsoft Docs](https://docs.microsoft.com/en-us/ef/core/providers/))
* Clone this repo
* Open up the solution (directory) in  VS / your text editor
* Rename `Web/appsettings.Development.example.json` to `appsettings.Development.json`
* Update the `ConnectionStrings` with the correct parameters. (If you're not using MySql replace it with an appropriate connection string.)
* That's it.

## Run the project

#### Visual Studio / Visual Studio Code

<kbd>F5</kbd> to Debug

#### Visual Studio for Mac

<kbd>Cmd + Return</kbd> to debug

#### Commandline

```bash
cd Web
dotnet run
# Now browse to localhost:5000
```
