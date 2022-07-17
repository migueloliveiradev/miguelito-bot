using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace miguelito_bot_slashcommands.slashcommands.Messages
{
    internal class Say : ApplicationCommandModule
    {
        [SlashCommandPermissions(Permissions.ManageMessages)]
        [SlashCommand("Say", "Messages ┇ Send a text via the bot in the current channel or in a specific channel")]
        public async Task TestCommand(InteractionContext ctx,
            [Option("text", "Text you want to send")] string text,
            [Option("channel", "Channel you want to send the message")] DiscordChannel channel = null,
            [Choice("yes", 1)]
            [Choice("not", 0)]
            [Option("IsWebhook", "Want to send this message via webhook?")] long IsWebhook = 0)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                   new DiscordInteractionResponseBuilder().WithContent("Enviando....").AsEphemeral(true));
            if (channel == null)
            {
                channel = ctx.Channel;
            }
            if (IsWebhook == 1)
            {
                DiscordWebhookBuilder webhook = new()
                {
                    AvatarUrl = ctx.User.AvatarUrl,
                    Username = ctx.Member.Username,
                    Content = text
                };
                DiscordWebhook web = await channel.CreateWebhookAsync("User Say");
                await web.ExecuteAsync(webhook);
                await web.DeleteAsync();
            }
            else
            {
                await channel.SendMessageAsync(text);
            }
            return;
        }
    }
}