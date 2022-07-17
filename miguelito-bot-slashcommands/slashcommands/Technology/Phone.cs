using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;
using Newtonsoft.Json;

namespace miguelito_bot_slashcommands.slashcommands.Technology
{
    internal class Phone : ApplicationCommandModule
    {
        [SlashCommandGroup("Phone", "Phone related commands")]
        public class GroupContainer : ApplicationCommandModule
        {
            [SlashCommand("Information", "Technology ┇ Receive the information of the desired phone model")]
            public async Task PhoneInformation(InteractionContext ctx, [Option("name", "Name of phone")] string name_)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
                try
                {
                    string url = $"https://api-mobilespecs.azharimm.site/v2/search?query={name_}";
                    string json = await new HttpClient().GetStringAsync(url);
                    dynamic? data = JsonConvert.DeserializeObject(json);
                    string? slug = data?.data.phones[0].slug;
                    url = $"https://api-mobilespecs.azharimm.site/v2/{slug}";
                    json = await new HttpClient().GetStringAsync(url);
                    data = JsonConvert.DeserializeObject(json);
                    var device = data?.data;
                    string brand = device.brand;
                    string name = device.phone_name;
                    string image = device.phone_images[0];
                    string ram = device.specifications[5].specs[1].val[0];
                    string os = device.specifications[4].specs[0].val[0];
                    string cpu = device.specifications[4].specs[2].val[0];
                    string gpu = device.specifications[4].specs[3].val[0];

                    DiscordEmbedBuilder embed = new()
                    {
                        Title = $"{brand} {name}",
                        Color = Variables.Cores(),
                        ImageUrl = image,
                    };
                    embed.AddField("Armazenamento/Ram", $" {ram}", true);
                    embed.AddField("OS", $" {os}", true);
                    embed.AddField("CPU", $" {cpu}");
                    embed.WithFooter($"{brand}");
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
                    await Methods.CommandsUsed("Phone Information", ctx.Guild.Id, ctx.User.Id);
                    return;
                }
                catch
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Não foi possivel encontrar o celular solicitado"));
                    return;
                }
            }
        }
    }
}