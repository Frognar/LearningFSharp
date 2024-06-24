namespace WebApi.Habits

type Frequency =
    | Daily
    | Weekly
    | Monthly

type Name = string

type HabitId = System.Guid

type Habit = {
    Id : HabitId
    Name : Name
    Frequency : Frequency
}