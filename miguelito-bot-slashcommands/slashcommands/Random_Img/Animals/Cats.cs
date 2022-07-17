using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Ramdom_Img.Animals
{
    internal class Cats : ApplicationCommandModule
    {
        [SlashCommand("Cats", "Random images ┇ Get a picture of a random cat")]
        public async Task CatsRandom(InteractionContext ctx)
        {
            string url = "https://api.thecatapi.com/v1/images/search";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            string cats = data[0].url;
            string id = data[0].id;
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = cats,
            };
            embed.WithFooter($"ID: {id}");
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            await Methods.CommandsUsed("Cats", ctx.Guild.Id, ctx.User.Id);
            return;
        }
    }
}