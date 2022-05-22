using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miguelito_bot_commands.commands
{
    internal class roll_commands : BaseCommandModule
    {
        [Command("dado"), Aliases("roll", "sortear")]
        public async Task sortear(CommandContext ctx, int numero = 0)
        {
            await ctx.TriggerTypingAsync();
            if (numero == 0)
            {
                await ctx.RespondAsync("Meu chapa, você não colocou um número para o dado :pensive:");
            }
            else if (numero > int.MaxValue)
            {
                await ctx.RespondAsync("Meu chapa, eu não sou uma calculadora :pensive:");
            }
            else
            {
                Random rnd = new();
                int num = rnd.Next(1, numero + 1);
                await ctx.RespondAsync($"O número sorteado foi: **{num}**");
            }
            await Program.log("dado");
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("roll-member"), Aliases("rollmember", "roll_member")]
        public async Task roll_member(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            Random rnd = new();
            List<DiscordMember> member = ctx.Guild.GetAllMembersAsync().Result.ToList();
            while (true)
            {
                int num = rnd.Next(0, member.Count);
                if (!member[num].IsBot)
                {
                    await ctx.RespondAsync($"O membro sorteado foi: **{member[num].DisplayName}**");
                    break;
                }

            }
            await Program.log("roll-member");
        }

        [RequirePermissions(Permissions.ManageGuild)]
        [Command("roll-member-reacion"), Aliases("rollmemberreacion", "roll_member_reacion")]
        public async Task roll_member_reacion(CommandContext ctx, DiscordChannel channel, ulong id_message, DiscordEmoji emoji)
        {
            await ctx.TriggerTypingAsync();
            DiscordMessage message = await channel.GetMessageAsync(id_message);
            var reaction = message.GetReactionsAsync(emoji);
            if(reaction.Result.Count > 0)
            {
                List<DiscordUser> user = reaction.Result.ToList();
                Random rnd = new();
                int num = rnd.Next(0, user.Count);
                await ctx.RespondAsync($"O membro sorteado foi: {user[num].Username}");
            }
            else
            {
                await ctx.RespondAsync("Este emoji não existe nas reações da mensagem");
            }
        }
    }
}
