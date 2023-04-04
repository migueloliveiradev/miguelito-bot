using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Programming
{
    internal class Github : ApplicationCommandModule
    {
        [SlashCommandGroup("github", "Github related commands")]
        public class GroupContainer : ApplicationCommandModule
        {
            [SlashCommand("profile", "Programming ┇ View information from a user's github profile.")]
            public async Task ServerAvatar(InteractionContext ctx,
                [Option("nick", "user nick")] string nick)
            {


                //await ctx.CreateResponseAsync();
                return;
            }
        }
    }
}
