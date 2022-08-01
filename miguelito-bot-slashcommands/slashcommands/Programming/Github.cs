using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miguelito_bot_slashcommands.slashcommands.Programming
{
    internal class Github : ApplicationCommandModule
    {
        [SlashCommandGroup("github", "Github related commands")]
        public class GroupContainer : ApplicationCommandModule
        {
            [SlashCommand("profile", "Programming ┇ View information from a user's github profile.")]
            public async Task ServerAvatar(InteractionContext ctx,
                [Option("nick", "user nick")][Minimum(7)][Maximum(2147483647)] string nick)
            {

                
                //await ctx.CreateResponseAsync();
                return;
            }
        }
    }
}
