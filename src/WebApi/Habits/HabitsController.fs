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
    
    [<HttpPost>]
    member this.Post([<FromBody>] habitDto: {|Name : string; Frequency: string|}) =
        let frequency = Parser.parseFrequency habitDto.Frequency
        let habit = frequency |> Option.map (fun f -> { Id = System.Guid.NewGuid(); Name = habitDto.Name; Frequency = f }: Habit)
        match habit with
        | Some x -> printf $"{x}"
        | None -> printf "Wrong frequency!"
