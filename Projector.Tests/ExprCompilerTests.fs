namespace Projector.Tests

open System
open System.Linq.Expressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open Projector.Common

[<TestClass>]
type ExprCompilerTests() =
    let assertEqual (expected: Expression) (actual: Expression) =
        Assert.AreEqual(expected.ToString(), actual.ToString())

    [<TestMethod>]
    member __.TestLambda() =
        let body = <@ fun (x: DateTime) -> x.Year @>
        let expected = Expression.Lambda(Expression.Convert(Expression.Property(Expression.Variable(typeof<DateTime>,
                                                                                                    "x"),
                                                                                "Year"),
                                                            typeof<obj>),
                                         Expression.Parameter(typeof<DateTime>, "x"))

        assertEqual expected <| ExprCompiler.compileLambda body
