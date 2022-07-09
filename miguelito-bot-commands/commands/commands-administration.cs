using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace miguelito_bot_commands.commands
{
    internal class commands_administration : BaseCommandModule
    {
        [Command("ban")]
        public async Task ban(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-ban` agora é em slash comandos? " +
                $"use `/ban` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("unban")]
        public async Task unban(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-unban` agora é em slash comandos? " +
                $"use `/unban` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("kick")]
        public async Task kick(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-kick` agora é em slash comandos? " +
                $"use `/kick` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [RequirePermissions(Permissions.ManageMessages)]
        [Command("clear"), Aliases("limpar")]
        public async Task clear(CommandContext ctx, int quantity = 0)
        {
            if (quantity <= 500)
            {
                var mensagens = await ctx.Channel.GetMessagesAsync(quantity);
                await ctx.Channel.DeleteMessagesAsync(mensagens);
                await ctx.Client.SendMessageAsync(ctx.Channel, $"O chat teve {quantity} mensagens apagadas por {ctx.User.Mention} :broom:");
            }
            else if (quantity == 0)
            {
                var mensagens = await ctx.Channel.GetMessagesAsync(100);
                await ctx.Channel.DeleteMessagesAsync(mensagens);
                await ctx.Client.SendMessageAsync(ctx.Channel, $"O chat teve 100 mensagens apagadas por {ctx.User.Mention} :broom:");
            }
            else if (quantity > 500)
            {
                await ctx.RespondAsync("Infelizmente ainda sou um pequeno bot que suporta apagar o maximo de 500 mensagens");
            }
        }

    }
}
