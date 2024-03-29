﻿using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Moderation
{
    public class Ban : ApplicationCommandModule
    {
        [SlashCommandPermissions(Permissions.BanMembers)]
        [SlashCommand("ban", "Moderation ┇ Bans a user")]
        public async Task ban(InteractionContext ctx, [Option("user", "User to ban")] DiscordUser user,
            [Choice("None", 0)]
            [Choice("1 Day", 1)]
            [Choice("1 Week", 7)]
            [Option("Delete_messages", "Number of days of message history to delete")] long deleteDays = 0,
            [Option("Reason", "User ban reason")] string reason = "")
        {
            if (user == ctx.Guild.Owner)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Você não pode banir o dono.").AsEphemeral(true));
                return;
            }
            else if (user.Id == ctx.User.Id)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Você não pode se banir.").AsEphemeral(true));
                return;
            }
            else if (user == ctx.Client.CurrentUser)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent("?").AsEphemeral(true));
                return;
            }
            else if (ctx.Guild.GetMemberAsync(user.Id).Result.Hierarchy >= ctx.Member.Hierarchy)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Você não pode banir esse membro pois ele tem um cargo maior ou equivalente que o seu.").AsEphemeral(true));
                return;
            }
            else if (ctx.Guild.GetMemberAsync(user.Id).Result.Hierarchy >= ctx.Guild.GetMemberAsync(ctx.Client.CurrentUser.Id).Result.Hierarchy)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Eu não posso banir esse membro pois o cargo dele é maior ou equivalente que o meu.").AsEphemeral(true));
                return;
            }
            else if (reason.Length > 450)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent("O motivo do banimento deve ser menor que 450 caracteres").AsEphemeral(true));
                return;
            }
            try
            {
                await ctx.Guild.GetBanAsync(user);
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Esse Usuario já esta banido.").AsEphemeral(true));
                return;
            }
            catch { }
            await ctx.Guild.BanMemberAsync(user.Id, Convert.ToInt32(deleteDays), $"Banido por {ctx.User.Username}: {reason}");
            await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Usuario {user.Username} banido."));
            return;
        }
    }
}