namespace WebApi.Data

open Microsoft.EntityFrameworkCore

type DbHabit = {
    Id : System.Guid
    Name : string
    Frequency : string
}

type HabitContext() =
    inherit DbContext()
    
    member val Habits : DbSet<DbHabit> = null with get, set
    
    override this.OnConfiguring(optionsBuilder) =
        optionsBuilder.UseSqlite("Data Source=habits.db") |> ignore