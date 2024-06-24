namespace WebApi.Habits

open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("habits")>]
type HabitsController() =
    inherit ControllerBase()
    
    
    [<HttpGet>]
    member this.Get() =
        let habit: Habit = {
            Id = System.Guid.NewGuid()
            Name = "Test habit"
            Frequency = Daily 
        }
        habit.ToString()