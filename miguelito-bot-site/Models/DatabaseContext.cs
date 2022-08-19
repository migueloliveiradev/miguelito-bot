using Microsoft.EntityFrameworkCore;

namespace miguelito_bot_site.Models
{
    public class DatabaseContext : DbContext
    {
        /*/protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Server=sql715.main-hosting.eu;DataBase=u721092915_miguelito;Uid=u721092915_miguelito";

            MySqlServerVersion serverVersion = new(new Version(8, 0, 29));

            
            optionsBuilder.UseMySql(connectionString, serverVersion);
            

        }
        public DbSet<Used_Commands> Users { get; set; }*/
    }
}
