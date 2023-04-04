using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Math_
{
    internal class Bhaskara : ApplicationCommandModule
    {
        [SlashCommand("Bhaskara", "Math ┇ Calculate the result of Bhaskara's formula")]
        public async Task BhaskaraMath(InteractionContext ctx,
            [Option("A", "value of 'A'")] double a,
            [Option("B", "value of 'B'")] double b,
            [Option("C", "value of 'C'")] double c)
        {
            if (a == 0)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent("A equação não é do tipo ax²+bx+c=0").AsEphemeral(true));
                return;
            }
            else
            {
                double delta = Math.Pow(b, 2) - 4 * a * c;
                if (delta < 0)
                {
                    await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent("A equação não possui raízes reais").AsEphemeral(true));
                }
                else if (delta == 0)
                {
                    double x = -b / (2 * a);
                    await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"A equação possui apenas uma raiz real: {x}"));
                }
                else
                {
                    double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                    await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent($"A equação possui duas raízes reais: {x1} e {x2}"));
                }
            }
            return;
        }
    }
}