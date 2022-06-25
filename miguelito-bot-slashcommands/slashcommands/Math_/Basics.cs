using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using miguelito_bot_slashcommands.Utils;

namespace miguelito_bot_slashcommands.slashcommands.Math_
{
    internal class Basics : ApplicationCommandModule
    {
        [SlashCommand("Calculate", "Math ┇ Calculate values ​​of basic operations")]
        public async Task Calculate(InteractionContext ctx,
            [Option("Operation", "Enter a valid arithmetic operation.")] string Operation)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            try
            {
                Operation = Operation.Replace(";", "");
                Operation = Operation.Replace(",", ".");
                double result = await CSharpScript.EvaluateAsync<double>($"var i = {Operation}; return i;");
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"O resultado foi: {result}"));
                await Methods.CommandsUsed("Calculate", ctx.Guild.Id, ctx.User.Id);
                return;
            }
            catch
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Isso não é uma operação valida, infelizmente não sou Albert Einstein."));
                return;
            }
        }
    }
}
