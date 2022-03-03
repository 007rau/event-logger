# Event Logger

Event Logger is an analytics and reporting project which is built in C#/.NET 6. It has REST APIs to GET and POST that will log and report on user events.

## Installation

Use the docker hub to get the EventLogger image.

```bash
docker pull pandey72/eventlogger:v1
```

## Run

```bash
docker run -it --rm -p 8080:80 pandey72/eventlogger:v1
```

This will run [swagger](http://localhost:8080/swagger.index.html) (as the docker file is configured as development env), which can be used to interact with the REST APIs and can be used as a UI for the backend (temporarily).

## Design

GET APIs:
```
1. /api/v1/events/all
Description: Used to get all the events in the DB.

2. /api/v1/events/{id}
Description: Used to get an event with id from the DB.

3. /api/v1/events/analytics/InRange
Parameters: dateStart && dateEnd
Description: Used to get analytics on the top actions performed by the users on the date range is given by the API.

4. /api/v1/events/analytics/all
Description: Used to get analytics on the top actions performed by the users.
```

POST API:

```
1. /api/v1/events/createEvent
Request body: { "userName": "string", "eventType": 0, "eventContent": "string" }
Description: Saves the new event performed by the user. "eventType" accepts only values from [0, 1, 2] where refers to the actions.

eventType dictionary:
0: A user clicked on a link in a web page,
1: A user copies some text on a web page,
2: A user prints the contents of a web page
```

## Technical Decisions

1. Use of .NET 6 rather than .NET 5 as mentioned in assessment. This is due to the fact that .NET 5 doesn't have arm64 for macOS (M1 chip set).
2. Use of List (Collections) as In Memory DB. This is for ease of coding and data manipulation required for implementation of the APIs.
3. EventTpye to be stored as Enum. This is to validate backend only gets validate actions with every createEvent request.
4. Use of Swagger UI to interact with REST APIs as user actions.

## References
1. .NET 5 REST API Tutorial - Build From Scratch With C#.
2. Microsoft .NET documentations.
3. StackOverflow Community.