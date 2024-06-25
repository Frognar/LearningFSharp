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
        
        let dto: HabitDto = {
            Id = habit.Id
            Name = habit.Name
            Frequency = habit.Frequency.ToString() 
        }
        
        dto