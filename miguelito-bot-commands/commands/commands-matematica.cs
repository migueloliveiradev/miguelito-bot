using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace miguelito_bot_commands.commands
{
    internal class commands_matematica : BaseCommandModule
    {
        [Command("soma")]
        [Aliases("somar")]
        public async Task soma(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-somar` agora é em slash comandos? " +
                $"use `/calculate` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("subtrair")]
        [Aliases("diminuir")]
        public async Task subtrair(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-subtrair` agora é em slash comandos? " +
                $"use `/calculate` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("Bhaskara")]
        public async Task Bhaskara(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-Bhaskara` agora é em slash comandos? " +
                $"use `/bhaskara` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("raiz")]
        public async Task raiz(CommandContext ctx, double a)
        {
            await ctx.TriggerTypingAsync();
            if (a < 0)
            {
                await ctx.RespondAsync("A raiz de um número negativo não existe meu nobre.");
            }
            else
            {
                double raiz = Math.Sqrt(a);
                await ctx.RespondAsync("A raiz de " + a + " é " + raiz);
            }
            await Program.Log("raiz");
        }

        [Command("multiplicar")]
        public async Task multiplicar(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-multiplicar` agora é em slash comandos? " +
                $"use `/calculate` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }

        [Command("dividir")]
        public async Task dividir(CommandContext ctx, [RemainingText] string value = "")
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-dividir` agora é em slash comandos? " +
                $"use `/calculate` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }
        [Command("pi")]
        public async Task pi(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"O valor de pi é: {Math.PI}");
            await Program.Log("pi");
        }

        [Command("Potência")]
        [Aliases("Potencia", "potenciação", "potenciaçao", "potenciacao")]
        public async Task Potencia(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new()
            {
                Title = ":arrow_up: Updates",
                Description = $"Opa {ctx.User.Username} sabia que agora o comando `-potenciacao` agora é em slash comandos? " +
                $"use `/potentiation` para testa-lo\n\n" +
                $"Todos os comandos que não são em slash irão desaparecer **31 de Agosto de 2022**, estão comece a se acostumar com eles\n\n" +
                $"Caso os comandos não estejam aparecendo {Formatter.MaskedUrl("autorize minhas permisões", new Uri("https://discord.com/api/oauth2/authorize?client_id=949488330620432386&permissions=8&scope=bot%20applications.commands"), "beba agua")}" +
                $"Em caso de algum problema entre no meu {Formatter.MaskedUrl("servidor de suporte", new Uri("https://discord.gg/FZpH3SZahH"), "se o problema for culpa sua vai catar coquinho")}"
            };
            await ctx.RespondAsync(embed);
            return;
        }
    }
}