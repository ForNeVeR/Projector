namespace Projector.Controllers

open System
open System.Collections.Generic
open System.Data
open System.Data.Common
open System.Linq
open System.Net.Http
open System.Web.Http

open Npgsql

type ProjectController() =
    inherit ApiController()

    member x.Get() =
        async {
            use conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=user;Password=password;Database=database;")

            conn.Open()

            use command = new NpgsqlCommand("select id, content from table", conn)
            use! result = Async.AwaitTask <| command.ExecuteReaderAsync()

            return result
                |> Seq.cast<IDataRecord>
                |> Seq.map (fun r -> 1)
                |> Seq.toList
        } |> Async.StartAsTask
