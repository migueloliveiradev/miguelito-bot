using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miguelito_bot_slashcommands.Utils
{
    internal class Database
    {
        public static async Task AddBan(ulong guild_id, ulong user_id, ulong user_banned_by_id, long delete_days, string reason)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            using MySqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO " +
                $"HISTORIC_BANS(ID_GUILD, ID_USER, BANNED_BY_ID, COUNT_DAYS_DELETE, REASON_BAN, TYPE) " +
                $"VALUES('{guild_id}', '{user_id}', '{user_banned_by_id}', '{delete_days}', '{reason}', 'BAN')";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static async Task AddUnban(ulong guild_id, ulong user_id, ulong user_banned_by_id, long delete_days, string reason)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            using MySqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = $"INSERT INTO " +
                $"HISTORIC_BANS(ID_GUILD, ID_USER, BANNED_BY_ID, COUNT_DAYS_DELETE, REASON_BAN, TYPE) " +
                $"VALUES('{guild_id}', '{user_id}', '{user_banned_by_id}', '{delete_days}', '{reason}', 'UNBAN')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
