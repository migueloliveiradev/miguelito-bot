using DSharpPlus.Entities;
using MySql.Data.MySqlClient;

namespace miguelito_bot_24h.events
{
    public class Events
    {
        public static async Task remind()
        {
            int i = 1;
            while (true)
            {
                try
                {
                    Console.WriteLine($"iniciou {i}");
                    string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    Console.WriteLine($"{time}");
                    string cs = Program.config[2];
                    using MySqlConnection con = new(cs);
                    await con.OpenAsync();

                    string sql = $"SELECT ID_GUILD, ID_USER, MENSAGEM, DATA_HORA FROM REMINDER WHERE DATA_HORA LIKE '29/05/2022 15:18%'";
                    MySqlCommand cmd = new(sql, con);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    while (rdr.Read())
                    {
                        Console.WriteLine($"leu");
                        ulong guild = rdr.GetUInt64(0);
                        ulong user = rdr.GetUInt64(1);
                        string msg = rdr.GetString(2);
                        DiscordGuild g = Program.cliente.GetGuildAsync(guild).Result;
                        DiscordMember u = g.GetMemberAsync(user).Result;
                        await u.SendMessageAsync(msg);
                        Console.WriteLine($"lembrete enviado {time} - {guild} - {user} - {msg}");
                    }
                    Console.WriteLine($"fim {i}");
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                await Task.Delay(20000);
                i++;
            }
        }
    }
}