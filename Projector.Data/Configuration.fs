module Projector.Data.Configuration

open System
open System.Data
open System.Linq.Expressions
open AutoMapper
open Microsoft.FSharp.Quotations
open Projector.Data.Models

let compile (quot : Expr<'a -> 'b>) : Expression<Func<'a, 'b>> = failwith "Not implemented"
    
let ConfigureMapper() =
    Mapper.CreateMap<IDataRecord, User>(MemberList.Destination)
        .ForMember(compile <@ fun u -> box u.Id @>, fun c -> c.MapFrom(compile <@ fun r -> r.["Id"] @>))
