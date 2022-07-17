using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using MySql.Data.MySqlClient;

namespace miguelito_bot_log.Events
{
    internal static class Events
    {
        public static async Task ClientErrorEvent(DiscordClient sender, ClientErrorEventArgs e)
        {
            DiscordChannel channel = await sender.GetChannelAsync(982133738848784384);
            await channel.SendMessageAsync(e.Exception.Message);
        }

        public static async Task GuildMemberAddEvent(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            string sql = $"SELECT CHECK_ADD, CODE_MESSAGE, ID_CHANNEL FROM TABLE_ADD WHERE ID_SERVER = '{e.Guild.Id}'";
            MySqlCommand cmd = new(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (Convert.ToByte(rdr[0]) == 1)
            {
                try
                {
                    DiscordMessageBuilder message = await CSharpScript.EvaluateAsync<DiscordMessageBuilder>(rdr[1].ToString());
                    DiscordChannel channel = e.Guild.GetChannel((ulong)rdr[2]);
                    await channel.SendMessageAsync(message);
                }
                catch { }
            }
            await con.CloseAsync();
            return;
        }

        public static async Task GuildMemberRemoveEvent(DiscordClient sender, GuildMemberRemoveEventArgs e)
        {
            string cs = Program.config[2];
            using MySqlConnection con = new(cs);
            await con.OpenAsync();
            string sql = $"SELECT CHECK_EXIT, CODE_MESSAGE, ID_CHANNEL FROM TABLE_EXIT WHERE ID_SERVER = '{e.Guild.Id}'";
            MySqlCommand cmd = new(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (Convert.ToByte(rdr[0]) == 1)
            {
                try
                {
                    DiscordMessageBuilder message = await CSharpScript.EvaluateAsync<DiscordMessageBuilder>(rdr[1].ToString());
                    DiscordChannel channel = e.Guild.GetChannel((ulong)rdr[2]);
                    await channel.SendMessageAsync(message);
                }
                catch { }
            }
            await con.CloseAsync();
            return;
        }
    }
}