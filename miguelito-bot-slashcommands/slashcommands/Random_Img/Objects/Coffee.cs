using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Ramdom_Img.Objects
{
    internal class Coffee : ApplicationCommandModule
    {
        [SlashCommand("Coffee", "Random images ┇ Get a picture of a random coffee")]
        public async Task CoffeeRandom(InteractionContext ctx)
        {
            string url = "https://coffee.alexflipnote.dev/random.json";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            string? file = data.file;
            DiscordEmbedBuilder embed = new()
            {
                Color = Variables.Cores(),
                ImageUrl = file,
            };
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            await Methods.CommandsUsed("Coffee", ctx.Guild.Id, ctx.User.Id);
            return;
        }
    }
}
