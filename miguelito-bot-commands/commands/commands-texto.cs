using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using miguelito_bot_commands.Utils;

namespace miguelito_bot_commands.commands
{
    internal class Commands_texto : BaseCommandModule
    {
        [Command("conselho"), Aliases("conselhos")]
        public async Task Conselhos(CommandContext ctx, int numero = -1)
        {
            await ctx.TriggerTypingAsync();
            if (numero == -1)
            {
                Random random = new();
                int i = random.Next(0, Variables.Conselhos.Count);
                await ctx.RespondAsync($"{i + 1} - {Variables.Conselhos[i]}");
            }
            else if (numero != -1)
            {
                try
                {
                    numero--;
                    await ctx.RespondAsync(Variables.Conselhos[numero]);
                }
                catch
                {
                    await ctx.RespondAsync("Não consegui achar o conselho do numero pedido, as vezes nem tudo pode ser uma resposta para suas buscas.");
                }
            }
            await Program.Log("conselho");
            return;
        }

        [Command("piada"), Aliases("piadas")]
        public async Task Piada(CommandContext ctx, int numero = -1)
        {
            await ctx.TriggerTypingAsync();
            if (numero == -1)
            {
                Random random = new();
                int i = random.Next(0, Variables.Piadas.Count);
                await ctx.RespondAsync($"{i} - {Variables.Piadas[i]} :joy:");
            }
            else if (numero <= Variables.Piadas.Count && numero > 0)
            {
                numero--;
                await ctx.RespondAsync(Variables.Piadas[numero]);
            }
            else
            {
                await ctx.RespondAsync("Não consegui achar a piada do numero pedido, as vezes a graça da vida é cantar sobre bananas");
            }
            await Program.Log("piada");
            return;
        }

        [Command("curiosidade"), Aliases("curiosidades")]
        public async Task Curiosidade(CommandContext ctx, int numero = -1)
        {
            await ctx.TriggerTypingAsync();
            if (numero == -1)
            {
                Random random = new();
                int i = random.Next(0, Variables.Curiosidades.Count);
                await ctx.RespondAsync($"{i} - {Variables.Curiosidades[i]} :joy:");
            }
            else if (numero <= Variables.Curiosidades.Count && numero > 0)
            {
                numero--;
                await ctx.RespondAsync(Variables.Curiosidades[numero]);
            }
            else
            {
                await ctx.RespondAsync("Não consegui achar o conselho do numero pedido, as vezes nem tudo pode ser uma resposta para suas buscas.");
            }
            await Program.Log("curiosidade");
        }

        [Command("cantada")]
        public async Task Cantada(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            Random random = new();
            int i = random.Next(0, Variables.Cantadas.Count);
            await ctx.RespondAsync(Variables.Cantadas[i] + "\n\nUooh Uooh Uooh meu lençol drobrado já tá todo bagunçado");
            await Program.Log("cantada");
            return;
        }
    }
}