using DSharpPlus;
using DSharpPlus.EventArgs;

namespace miguelito_bot_slashcommands.Utils
{
    internal class Events
    {
        public static async Task OnReady(DiscordClient sender, ReadyEventArgs e)
        {
            await Variables.VersionAdd();
        }
    }
}