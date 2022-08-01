using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;

namespace miguelito_bot_slashcommands.slashcommands.Embed
{
    internal class Embed : ApplicationCommandModule
    {
        [SlashCommandPermissions(Permissions.ManageMessages)]
        [SlashCommandGroup("Embed", "Commands related to Embeds")]
        public class GroupContainer : ApplicationCommandModule
        {
            [SlashCommand("Create", "Messages ┇ Create and send a fully customizable embed")]
            public async Task EmbedCreate(InteractionContext ctx,
                [Option("Title", "Embed title")] string Title,
                [Option("Description", "Embed Description")] string Description,
                [Option("Channel", "Channel to send the message")] DiscordChannel channel = null,
                [Option("Image_embed_url", "Embed Image")] string Image = null,
                [Option("Thumbnail_url", "Embed Thumbnail")] string Thumbnail = null,
                [Option("Avatar_message", "Message avatar")] string AvatarUrl = null,
                [Option("User", "Message username")] string username = null)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder().WithContent("Gerando...").AsEphemeral(true));
                if (channel == null)
                {
                    channel = ctx.Channel;
                }
                if (channel.IsCategory)
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Canal invalido."));
                }
                if (AvatarUrl == null)
                {
                    AvatarUrl = ctx.Member.AvatarUrl;
                }
                if (username == null)
                {
                    username = ctx.Member.Username;
                }
                DiscordEmbedBuilder embed = new()
                {
                    Title = Title,
                    Description = Description
                };
                if (Image != null)
                {
                    embed.ImageUrl = Image;
                }
                embed.WithThumbnail(Thumbnail);
                DiscordWebhookBuilder webhook = new()
                {
                    AvatarUrl = AvatarUrl,
                    Username = username
                };
                webhook.AddEmbed(embed);
                DiscordWebhook web = await channel.CreateWebhookAsync("Embed Create");
                await web.ExecuteAsync(webhook);
                await web.DeleteAsync();
                return;
            }
        }
    }
}