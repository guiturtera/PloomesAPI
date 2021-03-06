# Summary
This project has the purpose of storing data from user, and with that, create Spreadsheets for them.

Validation: based on JSON Web Tokens auth, the user will have access to specific features.

Login: used SHA256 as hash with some salt to improve the security.

There are 3 different access types:
unauthorized: when you don't have a token, you only have access to login page.
defaultuser: default users only have access for their info and Spreadsheets
admin: admins can add, edit, delete and get info about every user and Spreadsheets
# Docs
Since swagger has a bug that is hardly solved with dotnet deploy to azure, the project URL does not contain any swagger HTML.

To overcome this, I created a file named `swagger.yaml` in the root of the project. If you go to https://editor.swagger.io/, and copy the content in the left side, the browser will render the content of the doc formatted in HTML.

Another way to see docs is to clone the project and run it, Swagger will be available for using.
# How to use
I have created a custom POSTMan api for using the API. Just import the url https://www.postman.com/collections/41375fd4f2d369b03293 into postman.
You will have to log in and enable the Bearer token in the authorization, as explained in the bellow text.

Another easier way of testing the application is importing the `swagger.yaml` file content into POSTMan. It will create a custom API for you, other than the manually created for my tests. 

Clone this repo into a folder and install the packages needed for NuPackage.
To see documentation, see the previous step `Docs`.
The project url in Azure is `https://studyprojects.azurewebsites.net`
The admin cpf is cpf=`11111111111`, with password=`12345678`.
Send this data via post to the url `https://studyprojects.azurewebsites.net/api/Home/login`, in JSON format to the BODY of HTTPPost request. You will receive the json of an admin user, and the token, which you will have to use. This Token will be available for 2 hours. After that, you need to login again.

You will need to send this token via all `HEADER` authorization="Bearer TOKEN" for every HTTP method, since the api uses JSON Web Tokens for validation.
For more info about, you can access https://jwt.io

Check docs for the available methods.

## Setup
Change the key in `appsettings.json` for a valid SQL Server connection.
  "ConnectionStrings": {
    "DefaultConnection": "Some AVAILABLE CONNECTION"
  },
In dotnet cli run:
```
update-database
```
After that, you can start using the application.

