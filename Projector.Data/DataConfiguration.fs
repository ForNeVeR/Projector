module public Projector.Data.DataConfiguration

open System
open System.Data
open System.Linq.Expressions
open AutoMapper
//open FSharp.Quotations.Evaluator
open Microsoft.FSharp.Quotations
open Projector.Data.Models

let compile (quot : Expr<'a -> 'b>) : Expression<Func<'a, 'b>> = failwith "Not implemented"
    //QuotationEvaluator.ToLinqExpression quot :?> Expression<Func<'a, 'b>>
    
let ConfigureMappings() =
    ignore <| Mapper.CreateMap<IDataRecord, User>(MemberList.Destination)
        .ForMember(compile <@ fun u -> box u.Id @>, fun c -> c.MapFrom(compile <@ fun r -> r.["Id"] @>))
        .ForMember(compile <@ fun u -> box u.Login @>, fun c -> c.MapFrom(compile <@ fun r -> r.["Login"] @>))
        .ForMember(compile <@ fun u -> box u.PasswordHash @>, fun c -> c.MapFrom(compile <@ fun r -> r.["PasswordHash"] @>))

    Mapper.AssertConfigurationIsValid()
