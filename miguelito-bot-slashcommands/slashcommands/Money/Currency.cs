using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Money
{
    internal class Currency : ApplicationCommandModule
    {
        [SlashCommand("Currency", "Money ┇ Receive the current value of a currency.")]
        public async Task ServerAvatar(InteractionContext ctx,
            [Choice("USD", "USD")][Choice("BRL", "BRL")][Choice("EUR", "EUR")]
            [Choice("BTC", "BTC")][Option("currency", "Currency you want to know the value")] string currency,
            [Choice("USD", "USD")][Choice("BRL", "BRL")][Choice("EUR", "EUR")]
            [Choice("BTC", "BTC")][Option("your_currency", "Choose your currency to see the value")] string currency2)
        {
            if (currency == currency2)
            {
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent("As moedas devem ser diferente").AsEphemeral(true));
                return;
            }
            string url = $"https://economia.awesomeapi.com.br/json/last/{currency}-{currency2}";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder());
            return;
        }
    }
}
