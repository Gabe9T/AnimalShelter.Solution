# AnimalShelter API
## Api for animal names, age, and species
_by Gabriel Tucker_

## Setup/Installation Requirements

* .NET must be installed. Latest version can be found [here](https://dotnet.microsoft.com/en-us/).
* To run locally on your computer, clone the main branch of this [repository](https://github.com/Gabe9T/AnimalShelter.Solution).
* In your terminal, navigate to the root folder of this project and run `dotnet restore`.
* Open MySQL Workbench. Latest version can be downloaded [here](https://dev.mysql.com/downloads/workbench/).
* Create a new file in the "AnimalShelter" directory called appsettings.json. NOTE: If you plan to use this project as a jumping off point for further development, you must ensure that appsettings.json is added to your .gitignore file and committed prior to creating this file.
* In `appsettings.json`, enter the following, replacing `USERNAME` and `PASSWORD` to match the settings of your local MySQL server. Replace `DATABASE-NAME` with whatever you would like to name your database.
  
```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=DATABASE-NAME;uid=USERNAME;pwd=PASSWORD;"
    },
    "JWT": {
        "ValidAudience": "example-audience",
        "ValidIssuer": "example-issuer",
        "Secret": "SecretPassword12"
    }
}
```
* In your terminal, navigate to the "AnimalShelter" directory and run `dotnet ef database update` to create a local database schema seeded 6 animals
* To view the project in a web browser, navigate to the "AnimalShelter" directory and run `dotnet watch run`.

### Endpoints

```
GET https://localhost:5001/api/animals/
GET https://localhost:5001/api/animals/{id}
POST https://localhost:5001/api/animals/
PUT https://localhost:5001/api/animals/{id}
DELETE https://localhost:5001/api/animals/{id}
```
* Note: the `{id}` in the URL is a variable and should be replaced with the id of the animal a user wants to access.

 -   A GET endpoint at https://localhost:5001/api/animals will return all animal objects.
 -   A GET endpoint at https://localhost:5001/api/animals/{id} will return one animal object based on its AnimalId property.
 -   A POST endpoint at https://localhost:5001/api/animals will add a new animals to our database and requires a request body with an object literal of a animal.
 -   A PUT endpoint at https://localhost:5001/api/animals/{id} will edit the object in entired based on it's AnimalId property
 -   A DELETE endpoint at  https://localhost:5001/api/animals/{id} will delete the object based on its AnimalId property


### Additional requirements for PUT request
When making a PUT request to `https://localhost:5001/api/Animals/{id}`, you need to include a **body** that includes the animal's `animalId` property. Here's an example body in JSON:

```json
{
  "AnimalId": 1,
  "name": "string",
  "species": "string",
  "age": "int",
  "description": "string"
}
```

And here's the PUT request we would send the previous body to:

```
https://localhost:5001/api/Animals/1
```

Notice that the value of `AnimalId` needs to match the id number in the URL. In this example, they are both 1. For put and delete requests, the value of `user` must match the value of user in the database for that instance of a animal.
