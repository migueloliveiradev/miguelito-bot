using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace miguelito_bot_commands.commands
{
    internal class Commands_Animals : BaseCommandModule
    {
        [Command("gato"), Aliases("cats", "fofo")]
        public async Task gato(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-cats` agora é em slash comandos? " +
                $"use `/cats` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("cachorros"), Aliases("dog", "Cão")]
        public async Task dogs(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-dog` agora é em slash comandos? " +
                $"use `/dogs` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("raposa"), Aliases("fox", "firefox")]
        public async Task fox(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-fox` agora é em slash comandos? " +
                $"use `/fox` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("pato"), Aliases("duck")]
        public async Task duck(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-duck` agora é em slash comandos? " +
                $"use `/duck` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }
    }
}