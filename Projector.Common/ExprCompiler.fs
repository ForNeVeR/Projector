module Projector.Common.ExprCompiler

open System
open System.Linq.Expressions
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

let rec compile context expr : Expression =
    match expr with
    | PropertyGet(Some obj, property, []) ->
        upcast Expression.Property(compile context obj, property.GetMethod)
    | PropertyGet(Some obj, property, args) ->
        upcast Expression.Property(compile context obj, property, List.map (compile context) args)
    | Value(``obj``, ``type``) ->
        upcast Expression.Constant(``obj``, ``type``)
    | Var(var) ->
        upcast Map.find var.Name context
    | _ -> failwithf "Unsupported expr for compile: %A" expr

let compileLambda (expr : Expr<'a -> _>) : Expression<Func<'a, obj>> =
    match expr with
    | Lambda(param, body) ->
        let parameter = Expression.Parameter(param.Type, param.Name)
        let context = Map.ofList [(parameter.Name, parameter)]
        Expression.Lambda(
            Expression.Convert(compile context body, typeof<obj>),
            parameter) :?> Expression<Func<'a, obj>>
    | _ -> failwithf "Unsupported expr for compileLambda: %A" expr
