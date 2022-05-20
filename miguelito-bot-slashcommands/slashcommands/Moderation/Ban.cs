using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Moderation
{
    public class Ban : ApplicationCommandModule
    {
        [SlashCommandPermissions(Permissions.BanMembers)]
        [SlashCommand("ban", "Bans a user")]
        public async Task ban(InteractionContext ctx, [Option("user", "User to ban")] DiscordUser user,
            [Choice("None", 0)]
            [Choice("1 Day", 1)]
            [Choice("1 Week", 7)]
            [Option("deletedays", "Number of days of message history to delete")] long deleteDays = 0,
            [Option("reason", "user ban reason")] string reason = null)
        {
            await ctx.Guild.BanMemberAsync(user.Id, (int)deleteDays, reason);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Banned {user.Username}"));
        }
    }
}
