
# Fruit Information Application

## Overview

The **Fruit Information Application** is a .NET 8-based web API that interacts with the Fruityvice API to provide detailed information about various fruits. This application allows users to fetch fruit details, add, update, and remove nutritional metadata, and optimize performance using caching mechanisms.

## Features

- **Fetch All Fruits:** Retrieve a list of all fruits from the Fruityvice API.
- **Get Fruit by Name:** Retrieve specific fruit details by name.
- **Add Metadata:** Add new nutritional information to a specific fruit.
- **Update Metadata:** Update existing nutritional information for a specific fruit.
- **Remove Metadata:** Remove specific nutritional information from a fruit.
- **Caching:** Caches the list of fruits to reduce the number of API calls for repeated queries.

## Prerequisites

- **.NET SDK** (version 8.0 or later)
- **Internet Connection**: Required to interact with the Fruityvice API.

## Setup Instructions

### 1. Clone the Repository

```bash
git clone [https://github.com/your-repo/FruitInfoApp.git](https://github.com/tamarastanikevska999/fruit-system.git)
```

### 2. Install Dependencies

Navigate to the project directory and restore the dependencies:

```bash
dotnet restore
```

### 3. Update Configuration

Ensure that the `appsettings.json` file contains the correct base URL for the Fruityvice API:

```json
{
    "FruityviceApi": {
        "BaseUrl": "https://www.fruityvice.com"
    }
}
```

### 4. Run the Application

Execute the following command to run the application:

```bash
dotnet run
```

The application will start, and you can interact with it via API endpoints.

## API Endpoints

### Fetch All Fruits

- **Description**: Retrieve a list of all fruits.
- **Endpoint**: `GET /api/fruits/all`
- **Response**: JSON array of fruit objects.

### Get Fruit by Name

- **Description**: Retrieve specific fruit details by name.
- **Endpoint**: `GET /api/fruits/{name}`
- **Parameters**: `name` - The name of the fruit to fetch.
- **Response**: JSON object of the fruit details.

### Add Metadata to Fruit

- **Description**: Add new nutritional information to a specific fruit.
- **Endpoint**: `POST /api/fruits/{name}/metadata`
- **Parameters**: `name` - The name of the fruit.
- **Request Body**: 
  ```json
  {
      "key": "exampleKey",
      "value": 1.23
  }
  ```
- **Response**: `204 No Content` on success.

### Update Metadata on Fruit

- **Description**: Update existing nutritional information for a specific fruit.
- **Endpoint**: `PUT /api/fruits/{name}/metadata`
- **Parameters**: `name` - The name of the fruit.
- **Request Body**: 
  ```json
  {
      "key": "exampleKey",
      "value": 1.23
  }
  ```
- **Response**: `204 No Content` on success.

### Remove Metadata from Fruit

- **Description**: Remove specific nutritional information from a fruit.
- **Endpoint**: `DELETE /api/fruits/{name}/metadata/{key}`
- **Parameters**: 
  - `name` - The name of the fruit.
  - `key` - The nutritional information key to remove.
- **Response**: `204 No Content` on success.

## Error Handling

- **Invalid Fruit Name**: Returns a `404 Not Found` response if the fruit name does not exist.
- **API Connectivity Issues**: Returns a `500 Internal Server Error` response if there are issues connecting to the Fruityvice API.
- **Invalid Metadata Key**: Returns a `400 Bad Request` response for invalid nutritional keys.

## Caching

The application uses in-memory caching to store the details of fruits that have been fetched from the Fruityvice API. This reduces the number of API calls for repeated queries and improves performance. Cached data is stored for one hour.
