using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace miguelito_bot_commands.Utils
{
    internal class Events
    {
        public static async Task OnReady(DiscordClient sender, ReadyEventArgs e)
        {
            await sender.UpdateStatusAsync(new DiscordActivity("Pou", ActivityType.Playing), UserStatus.Online);
            await Variables.AddVariables();
            Console.WriteLine("Added variable values.");
        }


        public static async Task ClientErrored(DiscordClient sender, ClientErrorEventArgs e)
        {
            DiscordChannel channel = await sender.GetChannelAsync(Convert.ToUInt64(Program.config[7]));
            await channel.SendMessageAsync($"erro {e.Exception.Message}");
            Console.WriteLine($"Erro: {e.Exception.Message}");
        }
    }

}
