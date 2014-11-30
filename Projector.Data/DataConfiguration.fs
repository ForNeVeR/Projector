module public Projector.Data.DataConfiguration

open System.Data
open AutoMapper
open Microsoft.FSharp.Quotations
open Projector.Common
open Projector.Data.Models

let private mapMember (destGetter: Expr<'dest -> _>) (sourceGetter: Expr<'source -> _>) (map: IMappingExpression<'source, 'dest>) =
    let compile = ExprCompiler.compileLambda
    map.ForMember(compile destGetter, fun c -> c.MapFrom <| compile sourceGetter)

let ConfigureMappings() =
    Mapper.CreateMap<IDataRecord, User>(MemberList.Destination)
    |> mapMember <@ fun d -> d.Id @> <@ fun s -> s.["Id"] @>
    |> mapMember <@ fun d -> d.Login @> <@ fun s -> s.["Login"] @>
    |> mapMember <@ fun d -> d.PasswordHash @> <@ fun s -> s.["PasswordHash"] @>
    |> ignore

    Mapper.AssertConfigurationIsValid()
