using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Reflection;
using System.Text;

namespace miguelito_bot_commands.commands
{
    internal class commands_texto : BaseCommandModule
    {

        [Command("conselho"), Aliases("conselhos")]
        public async Task conselhos(CommandContext ctx, int numero = -1)
        {
            await ctx.TriggerTypingAsync();
            List<string> conselho = ReadLines(() => Assembly.GetExecutingAssembly()
           .GetManifestResourceStream("miguelito_bot_commands.text.conselhos.miguelito"), Encoding.UTF8)
               .ToList();
            if (numero == -1)
            {
                Random random = new Random();
                int i = random.Next(0, conselho.Count);
                await ctx.RespondAsync($"{i + 1} - {conselho[i]}");
            }
            else if (numero != -1)
            {
                try
                {
                    numero--;
                    await ctx.RespondAsync(conselho[numero]);
                }
                catch
                {
                    await ctx.RespondAsync("Não consegui achar o conselho do numero pedido, as vezes nem tudo pode ser uma resposta para suas buscas.");
                }
            }
            await Program.log("conselho");
        }

        [Command("piada"), Aliases("piadas")]
        public async Task piada(CommandContext ctx, int numero = -1)
        {
            await ctx.TriggerTypingAsync();
            var piada = ReadLines(() => Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("miguelito_bot_commands.text.piadas.miguelito"), Encoding.UTF8)
                .ToList();
            if (numero == -1)
            {
                Random random = new Random();
                int i = random.Next(0, piada.Count);
                await ctx.RespondAsync($"{i} - {piada[i]} :joy:");
            }
            else if (numero <= piada.Count && numero > 0)
            {
                numero--;
                await ctx.RespondAsync(piada[numero]);
            }
            else
            {
                await ctx.RespondAsync("Não consegui achar a piada do numero pedido, as vezes a graça da vida é cantar sobre bananas");
            }
            await Program.log("piada");
        }

        [Command("curiosidade"), Aliases("curiosidades")]
        public async Task curiosidade(CommandContext ctx, int numero = -1)
        {
            await ctx.TriggerTypingAsync();
            var curiosidade = ReadLines(() => Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("miguelito_bot_commands.text.curiosidades.miguelito"), Encoding.UTF8)
                .ToList();
            if (numero == -1)
            {
                Random random = new Random();
                int i = random.Next(0, curiosidade.Count);
                await ctx.RespondAsync($"{i} {curiosidade[i]} :joy:");
            }
            else if (numero <= curiosidade.Count && numero > 0)
            {
                numero--;
                await ctx.RespondAsync(curiosidade[numero]);
            }
            else
            {
                await ctx.RespondAsync("Não consegui achar o conselho do numero pedido, as vezes nem tudo pode ser uma resposta para suas buscas.");
            }
            await Program.log("curiosidade");
        }

        [Command("cantada")]
        public async Task cantada(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var cantada = ReadLines(() => Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("miguelito_bot_commands.text.cantada.miguelito"), Encoding.UTF8)
                .ToList();
            Random random = new Random();
            int i = random.Next(0, cantada.Count);
            await ctx.RespondAsync(cantada[i] + "\n\nUooh Uooh Uooh meu lençol drobrado já tá todo bagunçado");
            await Program.log("cantada");
        }

        public static IEnumerable<string> ReadLines(Func<Stream> streamProvider,
                                   Encoding encoding)
        {
            using (var stream = streamProvider())
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
