# Docs
There is an easy way to see the docs.
In the root of the project, there is a `swagger.yaml` file.
This file is the documentation of the project, in an openapi format.
To view the docs, go to [https://editor.swagger.io/] and copy the code in the left side. The browser will render the Html doc for you.
# How to use
Clone this repo into a folder and install the packages needed for NuPackage.
To see documentation, run the program or follow the previous step `Docs`.
The project url in Azure is `https://studyprojects.azurewebsites.net`
The admin cpf is cpf=`11111111111`, with password=`12345678`.
Send this data via post to the url `https://studyprojects.azurewebsites.net/api/Home/login`, in JSON format. You will receive an admin username, and the token.
You will need to send this token via all `HEADER` authorization="Bearer TOKEN", since the api uses JSON Web Tokens for validation.

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


