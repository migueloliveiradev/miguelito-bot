using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace miguelito_bot_commands.Utils
{
    internal class Events
    {
        public static async Task OnReady(DiscordClient sender, ReadyEventArgs e)
        {
            await sender.UpdateStatusAsync(new DiscordActivity("Visual Studio", ActivityType.Playing), UserStatus.Online);
            //await Variables.AddBomdia();
        }
        public static async Task ClientErrored(DiscordClient sender, ClientErrorEventArgs e)
        {
            DiscordGuild guild = await sender.GetGuildAsync(880904935787601960);
            DiscordChannel channel = guild.GetChannel(Convert.ToUInt64(Program.config[7]));
            await channel.SendMessageAsync($"erro {e.Exception.Message}");
            Console.WriteLine($"Erro: {e.Exception.Message}");
        }
    }

}
