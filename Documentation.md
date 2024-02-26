# AnimalShelter API
## an api animal names, age, and species
_by Gabriel Tucker_

Base URL: localhost:5001/api

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