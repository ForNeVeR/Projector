namespace Projector.Data.Repositories

open System
open Microsoft.Practices.Unity
open Npgsql
open Projector.Configuration

type BaseRepository(container: IUnityContainer) =
    let config = container.Resolve<IConfig>()
    let connection = new NpgsqlConnection(config.ConnectionString)

    member internal __.CreateCommand sql = new NpgsqlCommand(sql, connection)

    interface IDisposable with
        member __.Dispose() = connection.Dispose()
