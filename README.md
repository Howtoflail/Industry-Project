# kind-regards-api
Backend API for the prototype of "Kind Regards"

## Table of contents

<!-- TOC -->

- [kind-regards-api](#kind-regards-api)
    - [Table of contents](#table-of-contents)
    - [Documentation](#documentation)
    - [Before you start](#before-you-start)
        - [Application structure](#application-structure)
        - [Main layers](#main-layers)
        - [Abstraction layers](#abstraction-layers)
        - [IoC layers](#ioc-layers)
        - [Controller discovery](#controller-discovery)
        - [Choices](#choices)
        - [Class diagram](#class-diagram)
    - [Running the project](#running-the-project)
        - [MySQL database](#mysql-database)
        - [Entity framework](#entity-framework)
        - [Visual studio](#visual-studio)
    - [Future improvements](#future-improvements)

<!-- /TOC -->

## Documentation
This project includes documentation that explain the structure and architecture of this API. This document shows how it is structured using pictures and how to install the database on your laptop:
- picturesReadMe

## Before you start
This section is an introduction to this project and contains valuable information that hopefully speeds up the development process for future groups.

### Application structure
This project has been built with a layered architecture in mind. The layers are being used to separate the responsibilities of each class in a more structured way.

The default project (and application starting point) is `KindRegardsApi.Host`. This project houses the asp.net application which handles the setup of the asp.net application, it should only be used for that.

**Note:** Make sure you are running the `KindRegardsApi.Host` application, and not a any of the other project applications. Each application other than `KindRegardsApi.Host` is a class library project and will not have a starting point.


### Main layers
A main layer is a layer that has a (partial) implementation of a functionality. The main layers for this project are: `Presentation`, `Logic`, `Domain`, `Entity`, and `Data`.

**Presentation**<br/>
The presentation layer is responsible for taking in all requests and generating a JSON response for it. It uses DTOs to prevent requests from changing more data than it should.

**Logic**<br/>
The logic layer is responsible for the business logic. Everything that needs to be calculated or transformed should exist in here.

**Domain**<br/>
The domain layer contains all business objects.

**Entity**<br/>
The entity layer contains all database entities. Each file name uses the `Entity` prefix in its class and file name to differentiate from business objects.

**Data**<br/>
The data layer is responsible for retrieving and storing data inside a database. This application uses a MySQL (MariaDB) database to store all of its information.

### Abstraction layers
An abstraction layer contains the interfaces that a layer may use. The layout of these layers are the same as their main layer counterpart, but instead of implementations they only include abstractions (interfaces).

### IoC layers
An IoC (Inversion of Control) layer has a direct dependency on a almost all layers within the architecture. These dependencies are used to provide ASP.net the correct information to make the dependency injection process work.

Each IoC layer only contains a `[LAYERNAME]ServiceCollectionExtensions.cs` class. This is being used by ASP.net to configure the dependency injection tree.

### Controller discovery
The `Host` layer will auto-discover controllers inside the `Presentation` layer. You only have to create a new file, mark it with the `[ApiController]` annotation, give it a route and you're done.

### Choices
- The usage of a `Host` project brings the advantage that each main layer that is being used (`Presentation`, `Logic`, `Domain`, `Entity`, and `Data`) only has to think about their own implementations. The `Host` will make sure that all IoC layers are being included in the dependency injection process.
- Each service has an interface to make unit-testing easier, and to respect the asp.net dependency injection pattern.
- Abstraction layers are used to keep layers from leaking into each other. A main layer should never need to know about the implementations of other main layers, only their abstractions.
- A relational database system was chosen because our data entities have close ties with each other.
- The API auto-migrates it's database on every request. This is done to ensure that it can run inside a docker container. At the time of writing this there wasn't any way to migrate the database during the application startup.

### Class diagram
The diagram below shows the implementation flow of each main layer and their abstraction counterpart.
you can find the diagram in the document.

## Running the project
This section will talk about running the entire project locally on your computer. The docker option does not require you to have a MySQL database installed locally, this is done within docker-compose.

Once you have the application running, you can access it through postman (recommended) or your webbrowser on the following URL: [http://127.0.0.1:8080](http://127.0.0.1:8080)

**Note:** Each command that is listed below must be executed from the root folder of the API. The root folder is the same folder that this README file has been placed in.

### MySQL database
A MySQL database is needed for entity framework to run this application. You can either install MySQL directly on your machine, or use a virtual host manager that comes with a MySQL installation. we advice laragon because we used it and can help if needed. you can contact me on teams Maike Meek.

Availlable virtual host managers:
- [Laragon](https://laragon.org/)
- [WampServer](https://www.wampserver.com/en/)
- [Xampp](https://www.apachefriends.org/index.html)

For laragon:
1. download laragon.
2. open laragon
3. click on start all
4. click on database
5. download the krc file
6. unzip the krc file
7. create a new database called kindregards
8. open the database
9. double click krc.sql or import sql file

### Entity framework
Make sure that you have the entity framework tools installed globally:
```bash
$ dotnet tool install --global dotnet-ef --version 6.0.5
```

### Visual studio
If you want to run this project in Visual studio, you need to do the following:

**Open the KindRegardsApi project**<br/>
this can be done when you download the gitlab files

**Select the KindRegardsApi.Host project**<br/>
This can be done in the solution explorer:

**Set it as the startup project**<br/>
This can be done by right-clicking on the `KindRegardsApi.Host` project and selecting "Set as startup project" in the context menu:



### Dotnet CLI
To run this project from the dotnet CLI, you can use the following command:
```bash
$ dotnet run --project KindRegardsApi.Host
```

## Future improvements
Since we've had around 5 weeks to create a good base for this project, not everything is as polished as it should be. This section details possible future improvements that can be done by a future team.

1. Integrate image to base64 conversion functionality.
2. extend the database api code
3. connect it to docker
