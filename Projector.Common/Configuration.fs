namespace Projector.Configuration

open System.Configuration

type IConfig =
    abstract member ConnectionString : string

type internal Config =
    interface IConfig with
        member __.ConnectionString = ConfigurationManager.ConnectionStrings.["Projector"].ConnectionString
