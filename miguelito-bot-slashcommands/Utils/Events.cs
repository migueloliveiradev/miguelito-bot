using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace miguelito_bot_slashcommands.Utils
{
    internal class Events
    {
        public static async Task OnReady(DiscordClient sender, ReadyEventArgs e)
        {
            await Variables.VersionAdd();
        }
        public static async Task SlashCommandError(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
        {
            DiscordChannel channel = await sender.Client.GetChannelAsync(1003324488340996177);
            DiscordEmbedBuilder embed = new()
            {
                Description = e.Exception.Message,
            };
            embed.AddField("user", e.Context.User.Id.ToString(), true);
            embed.AddField("guild", e.Context.Guild.Id.ToString(), true);
            embed.AddField("command", e.Context.CommandName);
            await channel.SendMessageAsync(embed);
            await e.Context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent($"Ocorreu um erro ao executar esse comando, já recebemos esse erro, pedimos perdão e iremos trabalhar para corrigir o mais breve.").AsEphemeral(true));
        }
        public static async Task SlashCommandExecuted(SlashCommandsExtension sender, SlashCommandExecutedEventArgs e)
        {
            return;
        }
    }
}