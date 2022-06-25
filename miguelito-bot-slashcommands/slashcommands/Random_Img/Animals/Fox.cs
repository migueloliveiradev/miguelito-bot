using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Ramdom_Img.Animals
{
    internal class Fox : ApplicationCommandModule
    {
        [SlashCommand("Fox", "Random images ┇ Get a picture of a random fox")]
        public async Task FoxRandom(InteractionContext ctx)
        {
            string url = "https://randomfox.ca/floof/";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            string? fox = data.image;
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = fox,
            };
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            await Methods.CommandsUsed("Fox", ctx.Guild.Id, ctx.User.Id);
            return;
        }
    }
}
