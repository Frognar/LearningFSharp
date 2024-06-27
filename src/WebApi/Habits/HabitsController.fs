namespace WebApi.Habits

open Microsoft.AspNetCore.Mvc
open WebApi.Data

[<ApiController>]
[<Route("habits")>]
type HabitsController(context: HabitContext) =
    inherit ControllerBase()
    
    
    [<HttpGet>]
    member this.Get (id: System.Guid) =
        let dbHabit: DbHabit = context.Habits.Find id
        let optionDbHabit = if obj.ReferenceEquals(dbHabit, null) then None else Some dbHabit
        let habit : Habit option = optionDbHabit |> Option.map (fun h -> {
                Id = h.Id
                Name = h.Name
                Frequency =
                    match Parser.parseFrequency h.Frequency with
                    | Some f -> f
                    | None -> failwith "Bad Data"
            })
        
        let dto = habit |> Option.map (fun h -> {
            Id = h.Id
            Name = h.Name
            Frequency = h.Frequency.ToString() 
        })
        
        match dto with
        | Some v -> this.Ok v :> IActionResult
        | _ -> this.NotFound () :> IActionResult
    
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
