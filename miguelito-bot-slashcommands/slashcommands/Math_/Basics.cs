using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Data;

namespace miguelito_bot_slashcommands.slashcommands.Math_
{
    internal class Basics : ApplicationCommandModule
    {
        [SlashCommand("Calculate", "Math ┇ Calculate values ​​of basic operations")]
        public async Task Calculate(InteractionContext ctx,
            [Option("Operation", "Enter a valid arithmetic operation.")] string Operation)
        {
            try
            {
                DataTable tabela = new();
                object resultado = tabela.Compute(Operation.Replace(',', '.'), "");
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"O resultado foi: {resultado}"));
                return;
            }
            catch
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"Isso não é uma operação valida, infelizmente não sou Albert Einstein."));
                return;
            }
        }
    }
}