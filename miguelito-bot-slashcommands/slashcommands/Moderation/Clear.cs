using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miguelito_bot_slashcommands.slashcommands.Moderation
{
    internal class Clear : ApplicationCommandModule
    {
        [SlashCommandPermissions(Permissions.ManageMessages)]
        [SlashCommand("Clear", "Moderation ┇ Delete the desired number of messages at once")]
        public async Task TestCommand(InteractionContext ctx,
            [Option("quantity", "Number of messages you want to delete, default 100.")][Minimum(10)][Maximum(1000)] long quantity = 100,
            [Option("user", "If you want to filter only messages from a user")] DiscordUser user = null,
            [Option("channel", "If you want to delete messages from another channel")] DiscordChannel channel = null)
        {
            if (channel == null)
            {
                channel = ctx.Channel;
            }
            IReadOnlyList<DiscordMessage> Messages = await channel.GetMessagesAsync(Convert.ToInt32(quantity));
            if (user == null)
            {

                List<DiscordMessage> MessageDelete = new();
                foreach (DiscordMessage m in Messages)
                {
                    if ((DateTimeOffset.Now - m.CreationTimestamp).Days <= 14)
                    {
                        MessageDelete.Add(m);
                    }
                }
                await channel.DeleteMessagesAsync(MessageDelete);
                string content = $"{quantity} mensagens deletadas.";
                if (Messages.Count != MessageDelete.Count)
                {
                    content += $"\nOutras {Messages.Count - MessageDelete.Count} mensagens não foram apagadas pois foram enviadas a mais de 14 dias.";
                }
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                   new DiscordInteractionResponseBuilder().WithContent(content).AsEphemeral(true));
                return;
            }
            else
            {
                List<DiscordMessage> MessageUser = new();
                foreach (DiscordMessage m in Messages)
                {
                    if (m.Author.Id == user.Id)
                    {
                        MessageUser.Add(m);
                    }
                }
                if (MessageUser.Count > 0)
                {
                    await channel.DeleteMessagesAsync(MessageUser);
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                   new DiscordInteractionResponseBuilder().WithContent($"{MessageUser.Count} mensagens foram deletadas de {user.Mention}."));
                }
                else
                {
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                   new DiscordInteractionResponseBuilder().WithContent($"Não foram encontradas mensagens de {user.Mention}").AsEphemeral(true));
                }
            }
            return;
        }
    }
}