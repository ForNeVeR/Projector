namespace Projector.Data.Repositories

open AutoMapper
open Microsoft.Practices.Unity
open Projector.Data.Models

type IUserRepository =
    abstract member List: unit -> Async<User seq>

type internal UserRepository(container: IUnityContainer) =
    inherit BaseRepository(container)
    
    interface IUserRepository with
        member x.List() = async {
            use result = x.CreateCommand("select Id, Login from Users")
            use! reader = Async.AwaitTask <| result.ExecuteReaderAsync()
            return Mapper.Map<User seq>(reader)
        }
