using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Ramdom_Img.Animals
{
    internal class Duck : ApplicationCommandModule
    {
        [SlashCommand("Duck", "Random images ┇ Get a picture of a random duck")]
        public async Task DogsRandom(InteractionContext ctx)
        {
            string url = "https://random-d.uk/api/random?format=json";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            string? img = data?.url;
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = img,
            };
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            await Methods.CommandsUsed("Duck", ctx.Guild.Id, ctx.User.Id);
            return;
        }
    }
}
