namespace WebApi.Habits

type Frequency =
    | Daily
    | Weekly
    | Monthly

module Parser =
    let parseFrequency (candidate: string) =
        match candidate.ToUpper() with
        | "DAILY" -> Some Daily
        | "WEEKLY" -> Some Weekly
        | "MONTHLY" -> Some Monthly
        | _ -> None

type Name = string

type HabitId = System.Guid

type Habit = {
    Id : HabitId
    Name : Name
    Frequency : Frequency
}