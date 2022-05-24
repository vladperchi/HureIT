<!-- PROJECT SHIELDS -->

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->

<br />
<p align="center">
  <a href="https://github.com/vladperchi/HureIT">
    <img src="https://github.com/vladperchi/HureIT/blob/master/resources/image/HureIT_Banner.png" alt="HureIT">
  </a>
    <br />
    <a href="https://github.com/vladperchi/HureIT/issues">Report Bug</a>
    ·
    <a href="https://github.com/vladperchi/HureIT/issues">Request Feature</a>
   <br />
 <br />
</p>

## About This Project

HureIT is HR departament application built Modular Monolith architecture, written in .NET Core 5.0. Each module is a separate vertical slice with its own custom architecture and the overall integration between the modules is mostly based on the event-driven approach to achieve greater autonomy between the modules.

There actually was no real need to implement microservices. For this, a well designed monolith application would also do the trick. I was clear to have the API and UI seperated, to give oppurtunities to multiple client apps in the future. For API, ASP.NET Core 5.0 and for the UI Angular 12 Material was my obvious choice.

## Solution structure

Modular Architecture is a software design in which a monolith is made better and modular with the importance of reusing components / modules. The same implemented in HureIT helps to be extended to support and operate with n-modules.

`Host`
Web application responsible for initializing and starting all the modules - loading configurations, running DB migrations, exposing public APIs etc.

`Modules`
Autonomous modules with the different set of responsibilities, highly decoupled from each other - there's no reference between the modules at all (such as shared projects for the common data contracts), and the synchronous communication & asynchronous integration (via events) is based on local contracts approach.

-   Identity: Management of users/identity (register, login, permissions etc.).
-   Workflow: Management of employees, permissions and types of permissions (create, complete, verify, search).

`Shared`
The set of shared components for the common abstractions & cross-cutting concerns. This Cross-cutting concerns would use interfaces/events. And yes, domain events are also included in the project using the Mediatr Handler. Each of the modules follows a clean / Onion / Hex architecture design.

### PRO

-   Clear Separation of Concerns
-   Easily Scalable
-   Lower complexity compared to Microservices
-   Low operational / deployment costs.
-   Reusability
-   Organized Dependencies

### CONTRA

-   Not Multi-technology compatible.
-   Horizontal Scaling can be a concern. But this can be managed via load balancers.
-   nce Interprocess Communication is used, messages may be lost during Application Termination. Microservices combat this ise by using external messaging brokers like Kafka, RabbitMQ.
-   We can make use of message agents but no, let's keep it simple.

Take a look in detail at the [structure][structure-url] of the solution

## Technology Stack

-   API - ASP.NET Core 5.0 WebAPI
-   Data Access - [Entity Framework Core 5.0][coredownload-url]
-   DB Providers - MSSQL SERVER
-   Client - Angular 12 Material

## Features & Plus

-   [ ] Modular Architecture
-   [ ] Built on .NET 5.0
-   [ ] Follows Clean Architecture Principles
-   [ ] Domain Driven Design
-   [ ] Flexible Repository Pattern
-   [ ] Entity Framework Core

<details>
  <summary>Click to See More!</summary>

-   [ ] Auto DB Migrations
-   [ ] Dapper Integration
-   [ ] AutoMapper
-   [ ] Fluent Validations
-   [ ] CQRS using MediatR
-   [ ] Middlewares
-   [ ] Hangfire
-   [ ] Custom Errors
-   [ ] Serilog Integration
-   [ ] In-Memory Database
-   [ ] Paginated API Responses
-   [ ] User & Role Based Permission
-   [ ] Identity Seeding
-   [ ] Database Seeding
-   [ ] JWT Authentication
-   [ ] HTTP Interceptor
-   [ ] CRUD Operations
-   [ ] Custom EventLogs
-   [ ] API Versioning
-   [ ] Email Service
-   [ ] File Upload
-   [ ] Export Excel
-   [ ] Docker Support

</details>

## Project Status

-   API - `In Progress`
-   Angular/UI - `Coming Soon!`
-   Docker - `Coming Soon!`

## Prerequisites

1. Install [.NET 5 SDK][dotnetdownload-url]
2. Install the latest DOTNET & EF CLI Tools by using this command:

```
 dotnet tool install --global dotnet-ef
```

3. Install the [Visual Studio][vsdownload-url] IDE 2022(v17.0.0 and above) OR [Visual Studio Code][vscodedownload-url].
4. Install the latest [Docker on Windows][dockerwininstall-url].
5. It's recommended to use MsSql Server Database as it comes by default with HureIT.

## Getting Started

To get started, let's follow this short instruction routine:

-   Download ZIP
-   Extract solution to selected directory
-   Open the solution `HureIT.sln` with Visual Studio or the directory with VSCode

## Running the API

-   Open PowerShell `HureIT/compose` directory and execute:

```
docker-compose -f infrastructure.yml up -d
```

Note: It will start the required infrastructure in the background.

-   Browse to http://localhost:5341/#/events?autorefresh to Seq Logs!.

Then you can continue the list listed below:

1. Open up `HureIT.sln` in VS2022.
2. Navigate to appSettings.json under `src/Host/Api/appsettings.json`
3. Add you MsSql connection string under `PersistenceSettings`. The default connection string:
   `"mssql": "Data Source=.;Initial Catalog=HureDB;Integrated Security=True;MultipleActiveResultSets=True"`
4. That is all you need to configure the API. Just create and run the API project.
5. By default, the database is migrated. Take a look at the [migrations][migrations-url] of the solution
6. Some default data is also included in this database, such as roles, users, employees, etc.
7. Browse to https://localhost:5001/ to Api HureIT!

## Running Angular

-   Navigate to hureit\src\client via terminal.
-   Run `npm install` to install all the required packages
-   Run `ng serve`
-   Navigate to https://localhost:4200 or https://localhost:4201 on your browser

## Default Roles & Credentials

As soon you build and run your application, default users and roles get added to the database.

Default Roles are as follows.

-   `Administrator`
-   `Staff`

Here are the credentials for the default users.

-   Email - admin@hureit.com / Pass: @AdminP4$$w0rd#
-   Email - vlad@hureit.com / Pass: @BasicP4$$w0rd#

You can use these credentials to generate JWT tokens in the `api/identity/tokens` endpoint.

## HTTP requests can be sent to the API?

-   You can find the list of all HTTP requests in [HureIT.Postman][postmancollection-url], file placed in the directory `HureIT\postman`.
-   This file is compatible with [Postman][postmandownload-url] and easily edited with [Visual Studio Code][vscode-url].

## Contributing

Contributions are what make the open source community such an amazing place to learn, create, and inspire. Any contribution you make is **greatly appreciated**.

Join the elite list!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/IncredibleFeature`)
3. Commit your Changes (`git commit -m 'Add some IncredibleFeature'`)
4. Push to the Branch (`git push origin feature/IncredibleFeature`)
5. Open a Pull Request.

<a href="https://github.com/vladperchi/HureIT/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=vladperchi/HureIT" />
</a>

## Core Developer Contact

-   Twitter - [Vladimir][twitter-vlad-url]
-   Twitter - [Code With Vladperchi][twitter-code-url]
-   Linkedin - [Vladimir][linkedin-url]
-   GitHub - [Vladperchi][github-url]

## License

This project is licensed with the [MIT License][license-url].

## Support

Has this Project helped you learn something New? or Helped you at work?
Here are a few ways by which you can support.

-   Leave a star! :star:
-   Recommend this awesome project to your colleagues
-   Do consider endorsing me on LinkedIn for ASP.NET Core - [Connect via LinkedIn][linkedin-url]
-   Or, If you want to support this project in the long run, [consider buying me a coffee][buymeacoffee-url]!

<br />

<a href="https://www.buymeacoffee.com/codewithvlad"><img width="250" alt="black-button" src="https://user-images.githubusercontent.com/31455818/138557309-27587d91-7b82-4cab-96bb-90f4f4e600f1.png" ></a>

[vsdownload-url]: https://visualstudio.microsoft.com/es/vs/community/
[vscodedownload-url]: https://code.visualstudio.com
[postmandownload-url]: https://www.postman.com/downloads/
[postmancollection-url]: https://github.com/vladperchi/HureIT/blob/master/postman/HureIT.Postman.json
[dockerwininstall-url]: https://docs.docker.com/docker-for-windows/install/
[coredownload-url]: https://docs.microsoft.com/en-us/ef/core/
[dotnetdownload-url]: https://dotnet.microsoft.com/download/dotnet/5.0
[structure-url]: https://github.com/vladperchi/HureIT/blob/master/docs/md/api-project-structure.md
[migrations-url]: https://github.com/vladperchi/HureIT/blob/master/docs/md/api-migrations-guide.md
[build-shield]: https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2Fvladperchi%2FHureIT%2Fbadge&style=flat-square
[build-url]: https://github.com/vladperchi/HureIT/actions
[contributors-shield]: https://img.shields.io/github/contributors/vladperchi/HureIT.svg?style=flat-square
[contributors-url]: https://github.com/vladperchi/HureIT/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/vladperchi/HureIT?style=flat-square
[forks-url]: https://github.com/vladperchi/HureIT/network/members
[stars-shield]: https://img.shields.io/github/stars/vladperchi/HureIT.svg?style=flat-square
[stars-url]: https://img.shields.io/github/stars/vladperchi/HureIT?style=flat-square
[issues-shield]: https://img.shields.io/github/issues/vladperchi/HureIT?style=flat-square
[issues-url]: https://github.com/vladperchi/HureIT/issues
[license-shield]: https://img.shields.io/github/license/vladperchi/HureIT?style=flat-square
[license-url]: https://github.com/vladperchi/HureIT/blob/master/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/vladperchi/
[github-url]: https://github.com/vladperchi/
[blog-url]: https://www.codewithvladperchi.com
[twitter-code-url]: https://www.twitter.com/codewithvlad
[twitter-vlad-url]: https://www.twitter.com/vladperchi
[buymeacoffee-url]: https://www.buymeacoffee.com/codewithvlad
