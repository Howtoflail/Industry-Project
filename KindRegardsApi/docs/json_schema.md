# Kind Regards API JSON schema
This document contains the JSON schema that the Kinds Regards API uses.

## Table of contents

<!-- TOC -->

- [Kind Regards API JSON schema](#kind-regards-api-json-schema)
    - [Table of contents](#table-of-contents)
    - [API Versioning](#api-versioning)
    - [Routes](#routes)
        - [Stickers (v1)](#stickers-v1)
        - [Pets (v1)](#pets-v1)
    - [Requests](#requests)
        - [Stickers (v1)](#stickers-v1-1)
            - [GET /api/v1/stickers](#get-apiv1stickers)
            - [GET /api/v1/stickers/:id](#get-apiv1stickersid)
            - [POST /api/v1/stickers](#post-apiv1stickers)
            - [PUT /api/v1/stickers/:id](#put-apiv1stickersid)
            - [PUT /api/v1/stickers/unlock](#put-apiv1stickersunlock)
            - [DELETE /api/v1/stickers/:id](#delete-apiv1stickersid)
        - [Pets (v1)](#pets-v1-1)
            - [GET /api/v1/pets](#get-apiv1pets)
            - [GET /api/v1/pets/:id](#get-apiv1petsid)
            - [POST /api/v1/pets](#post-apiv1pets)
            - [PUT /api/v1/pets/:id](#put-apiv1petsid)
            - [DELETE /api/v1/pets/:id](#delete-apiv1petsid)

<!-- /TOC -->

## API Versioning
Each version of the API can be accessed through the following base URL:
```
/api/:version
```

The following versions are availlable:
| Version |
|---------|
| v1      |

## Routes
This section shows an overview of all specific resources and the routes that they provide.

### Stickers (v1)
| Method | Path                    |
|--------|-------------------------|
| GET    | /api/v1/stickers        |
| GET    | /api/v1/stickers/:id    |
| POST   | /api/v1/stickers        |
| PUT    | /api/v1/stickers/:id    |
| PUT    | /api/v1/stickers/unlock |
| DELETE | /api/v1/stickers/:id    |

### Pets (v1)
| Method | Path                    |
|--------|-------------------------|
| GET    | /api/v1/stickers/:id    |
| POST   | /api/v1/stickers        |
| PUT    | /api/v1/stickers/:id    |
| DELETE | /api/v1/stickers/:id    |

## Requests
### Stickers (v1)
#### GET /api/v1/stickers
This route retrieves all stickers and uses the device ID to check if the sticker has been unlocked by that specific device.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 200 | When a device ID is given |
| 400 | When no device ID is given |

**Request headers**<br/>
| Key | Value |
|-----|-------|
| deviceId | YOUR_DEVICE_ID_HERE |

**Request body (JSON)**<br/>
```json
// No body needed...
```

**Response headers (example)**<br/>
```json
{
    "Content-Type": "application/json; charset=utf-8"
}
```

**Response body (example)**<br/>
```json
[
    {
        "id": 1,
        "image": "aW1hZ2UucG5n", // Base64 encoded image...
        "amount": 0,
        "unlocked": false
    },
    {
        "id": 2,
        "image": "aW1hZ2UucG5n", // Base64 encoded image...
        "amount": 0,
        "unlocked": true
    },
    {
        "id": 3,
        "image": "aW1hZ2UucG5n", // Base64 encoded image...
        "amount": 2,
        "unlocked": true
    }
]
```

#### GET /api/v1/stickers/:id
This route retrieves a specific sticker and uses the device ID to check if the sticker has been unlocked by that specific device.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 200 | When the sticker is found |
| 400 | When no device ID is given |
| 404 | When no resource could be found |

**Request headers**<br/>
| Key | Value |
|-----|-------|
| deviceId | YOUR_DEVICE_ID_HERE |

**Request body (JSON)**<br/>
```json
// No body needed...
```

**Response headers (example)**<br/>
```json
{
    "Content-Type": "application/json; charset=utf-8"
}
```

**Response body (example)**<br/>
```json
{
    "id": 1,
    "image": "aW1hZ2UucG5n", // Base64 encoded image...
    "amount": 0,
    "unlocked": false
}
```

#### POST /api/v1/stickers
This route creates a new sticker that can be unlocked by devices.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 201 | If the sticker is created successfully |
| 400 | If the image string is empty |
| 400 | If the sticker could not be created |

**Request headers**<br/>
| Key | Value |
|-----|-------|
| Content-Type | application/json |

**Request body (JSON)**<br/>
```json
{
    "image": "aW1hZ2UucG5n", // Base64 encoded image...
}
```

**Response headers (example)**<br/>
```json
{
    "Content-Type": "application/json; charset=utf-8"
}
```

**Response body (example)**<br/>
```json
{
    "id": 1,
    "image": "aW1hZ2UucG5n" // Base64 encoded image...
}
```

#### PUT /api/v1/stickers/:id
This route updates an existing sticker that can be unlocked by devices.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 200 | If the image is updated successfully |
| 400 | If the image string is empty |
| 400 | If the given id in the body, and the id in the URL do not match |
| 400 | If the sticker could not be created |
| 404 | If the sticker with a specific id is not found |

**Request headers**<br/>
| Key | Value |
|-----|-------|
| Content-Type | application/json |

**Request body (JSON)**<br/>
```json
{
    "id": 1, // Sticker ID...
    "image": "aW1hZ2UucG5n" // Base64 encoded image...
}
```

**Response headers (example)**<br/>
```json
{
    "Content-Type": "application/json; charset=utf-8"
}
```

**Response body (example)**<br/>
```json
{
    "id": 1,
    "image": "aW1hZ2UucG5n", // Base64 encoded image...
    "amount": 1,
    "unlocked": true
}
```

#### PUT /api/v1/stickers/unlock
This route links/unlocks a sticker to a specific device ID, and can be used to update the amount of stickers a specific device has afterwards.

To update the amount of stickers through this route, you first have to unlock it. Once a sticker is unlocked the same request can be made with a different amount.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 200 | If the sticker is unlocked/updated successfully |
| 404 | If the sticker with a specific id is not found |

**Request headers**<br/>
| Key | Value |
|-----|-------|
| deviceId | YOUR_DEVICE_ID_HERE |
| Content-Type | application/json |

**Request body (JSON)**<br/>
```json
{
    "id": 1, // Sticker ID...
    "amount": 1
}
```

**Response headers (example)**<br/>
```json
{
    "Content-Type": "application/json; charset=utf-8"
}
```

**Response body (example)**<br/>
```json
{
    "id": 1,
    "image": "aW1hZ2UucG5n", // Base64 encoded image...
    "amount": 1,
    "unlocked": true
}
```

#### DELETE /api/v1/stickers/:id
This route deletes any stickers + their unlocked device counterparts.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 204 | If the sticker is successfully deleted |
| 404 | If the sticker with a specific id is not found |

**Request headers**<br/>
| Key | Value |
|-----|-------|

**Request body (JSON)**<br/>
```json
// No body needed...
```

**Response headers (example)**<br/>
```json
// No special headers are sent...
```

**Response body (example)**<br/>
```json
// No body is sent...
```

### Pets (v1)
You will see the term `owner` being used a lot in this section. An `owner` refers to a device ID.

#### GET /api/v1/pets
This route retrieves all pets + their device ID's.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 200 | N\A |

**Request headers**<br/>
| Key | Value |
|-----|-------|

**Request body (JSON)**<br/>
```json
// No body needed...
```

**Response headers (example)**<br/>
```json
{
    "Content-Type": "application/json; charset=utf-8"
}
```

**Response body (example)**<br/>
```json
[
    {
        "id": 1,
        "deviceId": "123",
        "typeId": 1,
        "name": "test",
        "color": "#000",
        "growthStage": 0
    }
]
```

#### GET /api/v1/pets/:id
This route lets an owner retrieve their pet.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 200 | When a device ID is given and a pet exists |
| 400 | When no device ID is given |
| 404 | When a wrong pet ID, or wrong device ID for the pet is given |

**Request headers**<br/>
| Key | Value |
|-----|-------|
| deviceId | YOUR_DEVICE_ID_HERE |

**Request body (JSON)**<br/>
```json
// No body needed...
```

**Response headers (example)**<br/>
```json
{
    "Content-Type": "application/json; charset=utf-8"
}
```

**Response body (example)**<br/>
```json
{
    "id": 1,
    "typeId": 1,
    "name": "test",
    "color": "#000",
    "growthStage": 0
}
```

#### POST /api/v1/pets
This route creates a new pet and links a device to it.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 200 | When a pet has been created |
| 400 | When no device ID is given |
| 403 | When text is used that is not whitelisted |
| 400 | When a sticker could not be created |

**Request headers**<br/>
| Key | Value |
|-----|-------|
| deviceId | YOUR_DEVICE_ID_HERE |

**Request body (JSON)**<br/>
```json
{
    "typeId": 1,
    "name": "test",
    "color": "#fff",
    "growthStage": 0
}
```

**Response headers (example)**<br/>
```json
{
    "Content-Type": "application/json; charset=utf-8"
}
```

**Response body (example)**<br/>
```json
{
    "id": 1,
    "typeId": 1,
    "name": "test",
    "color": "#fff",
    "growthStage": 0
}
```

#### PUT /api/v1/pets/:id
This route lets the owner of a pet update it.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 200 | When a device ID is given and a pet exists |
| 400 | When no device ID is given |
| 400 | If the given id in the body, and the id in the URL do not match |
| 403 | When text is used that is not whitelisted |
| 400 | When a sticker could not be updated |

**Request headers**<br/>
| Key | Value |
|-----|-------|
| deviceId | YOUR_DEVICE_ID_HERE |

**Request body (JSON)**<br/>
```json
{
    "id": 1,
    "typeId": 1,
    "name": "test",
    "color": "#000",
    "growthStage": 0
}
```

**Response headers (example)**<br/>
```json
{
    "Content-Type": "application/json; charset=utf-8"
}
```

**Response body (example)**<br/>
```json
{
    "id": 1,
    "typeId": 1,
    "name": "test",
    "color": "#000",
    "growthStage": 0
}
```

#### DELETE /api/v1/pets/:id
This route deletes any pets.

**Responds with**<br/>
| HTTP code | Notes |
| ----------|-------|
| 204 | If the pet is successfully deleted |
| 404 | If the pet with a specific ID + device ID is not found |

**Request headers**<br/>
| Key | Value |
|-----|-------|

**Request body (JSON)**<br/>
```json
// No body needed...
```

**Response headers (example)**<br/>
```json
// No special headers are sent...
```

**Response body (example)**<br/>
```json
// No body is sent...
```
