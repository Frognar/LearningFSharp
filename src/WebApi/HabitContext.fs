namespace WebApi.Data

open Microsoft.EntityFrameworkCore

[<CLIMutable>] 
type DbHabit = {
    Id : System.Guid
    Name : string
    Frequency : string
}

type HabitContext() =
    inherit DbContext()
    
    [<DefaultValue>]
    val mutable habits : DbSet<DbHabit>
    
    member public this.Habits
        with get() = this.habits
        and set h = this.habits <- h
    
    override this.OnConfiguring(optionsBuilder) =
        optionsBuilder.UseSqlite("Data Source=habits.db") |> ignore