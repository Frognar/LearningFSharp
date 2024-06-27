namespace WebApi.Habits

open Microsoft.AspNetCore.Mvc
open WebApi.Data

[<ApiController>]
[<Route("habits")>]
type HabitsController(context: HabitContext) =
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
    
    [<HttpPost>]
    member this.Post([<FromBody>] habitDto: {|Name : string; Frequency: string|}) =
        let frequency = Parser.parseFrequency habitDto.Frequency
        let habit = frequency |> Option.map (fun f -> { Id = System.Guid.NewGuid(); Name = habitDto.Name; Frequency = f }: Habit)
        let dbHabit = habit |> Option.map (fun h -> { Id = h.Id; Name = h.Name; Frequency = h.Frequency.ToString() }: DbHabit)
        let saveHabit (c: HabitContext) (h: DbHabit) =
            c.Habits.Add(h)
            c.SaveChanges() |> ignore
            h
            
        let savedHabit = dbHabit |> Option.map (saveHabit context)
        
        match savedHabit with
        | Some x -> this.Ok x :> IActionResult
        | None -> this.BadRequest "Wrong frequency!" :> IActionResult
