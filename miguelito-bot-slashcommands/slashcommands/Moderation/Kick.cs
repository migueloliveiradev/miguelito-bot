using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Moderation
{
    internal class Kick : ApplicationCommandModule
    {
        [SlashCommandPermissions(Permissions.KickMembers)]
        [SlashCommand("Kick", "Moderation ┇ Kick a user")]
        public async Task ban(InteractionContext ctx, [Option("Member", "User to ban")] DiscordUser user,
            [Option("Reason", "User Kick reason")] string reason = "NULL")
        {
            if (user == ctx.Guild.Owner)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Você não pode expulsar o dono.").AsEphemeral(true));
                return;
            }
            else if (user == ctx.User)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Você não pode se expulsar.").AsEphemeral(true));
                return;
            }
            else if (ctx.Guild.GetMemberAsync(user.Id).Result.Hierarchy >= ctx.Member.Hierarchy)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Você não expulsar esse membro pois ele tem um cargo maior ou equivalente que o seu.").AsEphemeral(true));
                return;
            }
            else if (ctx.Guild.GetMemberAsync(user.Id).Result.Hierarchy >= ctx.Guild.GetMemberAsync(ctx.Client.CurrentUser.Id).Result.Hierarchy)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Eu não posso banir esse membro pois o cargo dele é maior ou equivalente que o meu.").AsEphemeral(true));
                return;
            }
            else if (user == ctx.Client.CurrentUser)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent("?").AsEphemeral(true));
                return;
            }
            else if (reason.Length > 450)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent("O motivo da expulsão deve ser menor que 450 caracteres").AsEphemeral(true));
                return;
            }
            await ctx.Guild.GetMemberAsync(user.Id).Result.RemoveAsync();
            await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Kick {user.Username}"));
            return;
        }
    }
}