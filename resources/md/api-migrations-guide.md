# Migrations - Guide

HureIT currently operates with a MSSQL database provider. In future versions Postgres and MySql could be included.

The solution has the repeated default migrations created from the base. This guide is only and only in case you need to add a field to a table or modify some type of data in it. Next you will see how to proceed.

Firstly, you need to make sure that valid connection strings are mentioned in the appSetting.json
Next, set either to true in `PersistenceSettings`.

`"UseMsSql": true,`

### Note Important

-   Make sure to delete all the migrations, and re-add migrations via the below CLI Command.
-   Make sure that you drop the existing database if any.

## Steps

-   Navigate to the Host folder and set the API as the startup project.
-   Open package manager console window and select the default project to perform the migration, which, in general, will always be in Modules.`Project-Name`.Infrastructure.
-   Navigate to each of the Infrastructure project per module and shared (Shared.Infrastructure)
-   Run the EF commands. You can find the EF Commands below in the next section with additional steps
-   That's it!

### Application

Navigate terminal to Shared.Infrastructure and run the following.

-   `Add-Migration Initial -context ApplicationDbContext -o Persistence/Migrations/`

### Identity

Navigate terminal to Modules.Identity.Infrastructure and run the following.

-   `Add-Migration Initial -context IdentityDbContext -o Persistence/Migrations/`

### Hure

Navigate terminal to Modules.Inmo.Infrastructure and run the following.

-   `Add-Migration Initial -context HureDbContext -o Persistence/Migrations/`
