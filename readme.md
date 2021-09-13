# How to use
Clone this repo into a folder and install the packages needed for NuPackage.
To see documentation, run the program.
The project url in Azure is `https://studyprojects.azurewebsites.net`
Run setup steps and see the docs for more info.
The admin cpf is cpf=`11111111111`, with password=`12345678`.
Send this data via post to the url, in JSON format. You will receive an admin username, and the token.
You will need to send this token via all `HEADER` authorization="Bearer <token>", in order to validate your entry.
Check docs for the available methods.
# Setup
Change the key in `appsettings.json` for a valid SQL Server connection.
  "ConnectionStrings": {
    "DefaultConnection": "Some AVAILABLE CONNECTION"
  },
In dotnet cli run:
```
update-database
```
After that, you may run the project and see docs.


