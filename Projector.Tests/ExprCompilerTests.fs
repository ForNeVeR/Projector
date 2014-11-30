namespace Projector.Tests

open System
open System.Collections.Generic
open System.Linq.Expressions
open Microsoft.VisualStudio.TestTools.UnitTesting
open Projector.Common

[<TestClass>]
type ExprCompilerTests() =
    let assertEqual (expected: Expression) (actual: Expression) =
        Assert.AreEqual(expected.ToString(), actual.ToString())

    [<TestMethod>]
    member __.TestValue() =
        let body = <@ "aaa" @>
        let expected = Expression.Constant("aaa", typeof<string>)
        assertEqual expected <| ExprCompiler.compile body

    [<TestMethod>]
    member __.TestPropertyGetter() =
        let body = <@ fun (x: DateTime) -> x.Year @>
        let expected = Expression.Lambda(Expression.Convert(Expression.Property(Expression.Variable(typeof<DateTime>,
                                                                                                    "x"),
                                                                                "Year"),
                                                            typeof<obj>),
                                         Expression.Parameter(typeof<DateTime>, "x"))

        assertEqual expected <| ExprCompiler.compileLambda body

    [<TestMethod>]
    member __.TestIndexer() =
        let body = <@ fun (x: Dictionary<int, int>) -> x.[0] @>
        let expected = Expression.Lambda(Expression.Convert(Expression.Property(Expression.Variable(typeof<Dictionary<int, int>>,
                                                                                                    "x"),
                                                                                "Item",
                                                                                Expression.Constant(0)),
                                                            typeof<obj>),
                                         Expression.Parameter(typeof<Dictionary<int, int>>, "x"))

        assertEqual expected <| ExprCompiler.compileLambda body
