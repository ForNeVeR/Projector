module Projector.Common.ExprCompiler

open System
open System.Linq.Expressions
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

let rec compile expr : Expression =
    match expr with
    | PropertyGet(Some obj, property, _) ->
        upcast Expression.Property(compile obj, property.GetMethod)
    | Var(var) ->
        upcast Expression.Variable(var.Type, var.Name)
    | _ -> failwithf "Unsupported expr for compile: %A" expr

let compileLambda (expr : Expr<'a -> _>) : Expression<Func<'a, obj>> =
    match expr with
    | Lambda(param, body) ->
        let parameter = Expression.Parameter(param.Type, param.Name)
        Expression.Lambda(
            Expression.Convert(compile body, typeof<obj>),
            parameter) :?> Expression<Func<'a, obj>>
    | _ -> failwithf "Unsupported expr for compileLambda: %A" expr
