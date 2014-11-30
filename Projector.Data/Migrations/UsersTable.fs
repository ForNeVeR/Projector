namespace Projector.Data.Migrations

open FluentMigrator

[<Migration(1L)>]
type UsersTable() =
    inherit Migration()

    override x.Up() =
        x.Create.Table("Users")
            .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("Email").AsString(256).NotNullable().Indexed()
            .WithColumn("PasswordHash").AsString(32).NotNullable()
        |> ignore

    override x.Down() =
        x.Delete.Table("Users")
        |> ignore
