using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Math_
{
    internal class Potentiation : ApplicationCommandModule
    {
        [SlashCommand("Potentiation", "Math ┇ Calculate the result of Bhaskara's formula")]
        public async Task PotentiationMath(InteractionContext ctx,
            [Option("Base", "base of the question")] double base_,
            [Option("Exponent", "Exponent of the question")] double expoente)
        {
            if (expoente == 0)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent("A potência de um número elevado a zero é 1"));
            }
            else
            {
                double potencia = 1;
                for (int i = 0; i < expoente; i++)
                {
                    potencia *= base_;
                }
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"A potência de {base_} elevado a {expoente} é {potencia}"));
            }
            return;
        }
    }
}