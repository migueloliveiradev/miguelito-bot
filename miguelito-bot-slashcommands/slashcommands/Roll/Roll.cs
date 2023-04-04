using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Roll
{
    internal class Roll : ApplicationCommandModule
    {
        [SlashCommandGroup("Roll", "Server related commands")]
        public class GroupContainer : ApplicationCommandModule
        {
            [SlashCommand("number", "Roll ┇ Get the server avatar or server avatar you want")]
            public async Task ServerAvatar(InteractionContext ctx,
                [Option("number", "the maximum number you want to choose")][Minimum(7)][Maximum(2147483647)] long number = 6)
            {
                int num = new Random().Next(1, Convert.ToInt32(number));
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent(num.ToString()));
                return;
            }

            [SlashCommand("info", "Server ┇ Get the server information or server information you want")]
            public async Task ServerInfo(InteractionContext ctx, [Option("server", "server id")] string serverID = null)
            {

                return;
            }
        }
    }
}
