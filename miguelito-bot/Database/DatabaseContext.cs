using Microsoft.EntityFrameworkCore;

namespace miguelito_bot.Database
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("miguelito_bot");
        }
    }
}
