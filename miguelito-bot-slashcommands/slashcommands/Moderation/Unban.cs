﻿using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Moderation
{
    internal class Unban : ApplicationCommandModule
    {
        [SlashCommandPermissions(Permissions.BanMembers)]
        [SlashCommand("unban", "Moderation ┇ Unban a user")]
        public async Task unban(InteractionContext ctx, [Option("user", "User to unban")] DiscordUser user, [Option("reason", "reason of unban")] string reason = "NULL")
        {
            try
            {
                await ctx.Guild.GetBanAsync(user);
            }
            catch
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"O Usuario não esta banido.").AsEphemeral(true));
                return;
            }
            await ctx.Guild.UnbanMemberAsync(user);
            return;
        }
    }
}