using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Reactions
{
    internal class Wink : ApplicationCommandModule
    {
        [SlashCommand("Wink", "Reactions ┇ Send a Gif of wink")]
        public async Task WinkUser(InteractionContext ctx, [Option("user", "member you want to wink")] DiscordUser user)
        {
            string url = "https://api.waifu.pics/sfw/wink";
            string json = await new HttpClient().GetStringAsync(url);
            dynamic? data = JsonConvert.DeserializeObject(json);
            string? gif = data?.url;
            DiscordEmbedBuilder embed = new()
            {
                Description = $"{ctx.Member.Mention} piscou para {user.Mention}",
                Color = Variables.Cores(),
                ImageUrl = gif,
            };
            DiscordButtonComponent GiveBack = new DiscordButtonComponent(ButtonStyle.Success, "GiveBack", "Retribuir");
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddComponents(GiveBack).AddEmbed(embed));
            await Methods.CommandsUsed("Reactions Wink", ctx.Guild.Id, ctx.User.Id);
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
                            Description = $"{user.Mention} piscou de volta pra {ctx.Member.Mention}",
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