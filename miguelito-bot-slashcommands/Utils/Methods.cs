using MySql.Data.MySqlClient;

namespace miguelito_bot_slashcommands.Utils
{
    internal class Methods
    {
        public static async Task CommandsUsed(string command, ulong guild_id, ulong user_id)
        {
            string cs = Program.config[2];
            using var con = new MySqlConnection(cs);
            await con.OpenAsync();
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText =
                $"INSERT INTO USED_COMMANDS(COMMAND, DATA, HORA, GUILD_ID, USER_ID) " +
                $"VALUES('{command}', '{DateTime.Now:dd/MM/yyyy}', '{DateTime.Now:HH:mm}', '{guild_id}', '{user_id}')";
            cmd.ExecuteNonQuery();
            con.Close();
            return;
        }
    }
}