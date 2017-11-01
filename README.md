Projector
=========
This will be a project management tool you need. No more, no less.

## Project Documentation

- [Technical Requirements][technical-requirements]

## Database deployment

This project uses a PostgreSQL database. First create the database:

    psql
    # create database projector encoding 'utf8';

 then run FluentMigrator `Migrate` tool:

    Migrate --assembly="Projector.Data/bin/Debug/Projector.Data.dll" --provider=Postgres --connection="Server=localhost;Port=5432;User Id=user;Password=password;Database=projector;"

It's always better to first check the migration with `--preview` key before applying it.

The `Migrate` tool is accessible through Package Manager Console or directly through command line if you have `FluentMigrator.Tools` installed globally.

[technical-requirements]: docs/technical-requirements.md
