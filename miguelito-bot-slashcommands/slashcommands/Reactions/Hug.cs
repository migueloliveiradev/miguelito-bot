using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Reactions
{
    internal class Hug : ApplicationCommandModule
    {
        [SlashCommand("Hug", "Reactions ┇ Send a Gif of hug")]
        public async Task KissUser(InteractionContext ctx, [Option("user", "member you want to hug")] DiscordUser user)
        {
            string url = "https://api.waifu.pics/sfw/hug";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            string? gif = data?.url;
            DiscordEmbedBuilder embed = new()
            {
                Description = $"{ctx.Member.Mention} abraçou {user.Mention}",
                Color = Variables.Cores(),
                ImageUrl = gif,
            };
            DiscordButtonComponent GiveBack = new(ButtonStyle.Success, "GiveBack", "Retribuir");
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddComponents(GiveBack).AddEmbed(embed));
            ctx.Client.ComponentInteractionCreated += async (s, e) =>
            {
                if (e.User == user && ctx.GetOriginalResponseAsync().Result.Id == e.Message.Id)
                {
                    await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                    if (e.Id == "GiveBack")
                    {
                        json = await new HttpClient().GetStringAsync(url);
                        data = JsonConvert.DeserializeObject(json);
                        gif = data?.url;
                        DiscordEmbedBuilder embed = new()
                        {
                            Description = $"{user.Mention} abraçou {ctx.Member.Mention} de volta",
                            Color = Variables.Cores(),
                            ImageUrl = gif,
                        };
                        await ctx.Channel.SendMessageAsync(embed);
                        return;
                    }
                }
            };
        }
    }
}