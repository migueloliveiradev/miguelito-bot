using MySql.Data.MySqlClient;

namespace miguelito_bot_site.Utils
{
    public class Database
    {
        public static ulong CommandsCount()
        {
            string cs = Program.Tokens[3];
            using MySqlConnection con = new(cs);
            con.Open();
            string sql = "SELECT COUNT('COUNT')FROM USED_COMMANDS";
            MySqlCommand cmd = new(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            ulong CommandsCount = Convert.ToUInt64(rdr[0]);
            con.Close();
            return CommandsCount;
        }
    }
}