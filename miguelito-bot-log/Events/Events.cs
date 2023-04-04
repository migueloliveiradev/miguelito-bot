using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
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
            
            return;
        }

        public static async Task GuildMemberRemoveEvent(DiscordClient sender, GuildMemberRemoveEventArgs e)
        {
            
            return;
        }
    }
}