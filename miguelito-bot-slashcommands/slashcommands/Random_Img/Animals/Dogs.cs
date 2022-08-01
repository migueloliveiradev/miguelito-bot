using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Ramdom_Img.Animals
{
    internal class Dogs : ApplicationCommandModule
    {
        [SlashCommand("Dogs", "Random images ┇ Get a picture of a random dogs")]
        public async Task DogsRandom(InteractionContext ctx)
        {
            string url = "https://dog.ceo/api/breeds/image/random";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            string? dogs = data?.message;
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = dogs,
            };
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            return;
        }
    }
}