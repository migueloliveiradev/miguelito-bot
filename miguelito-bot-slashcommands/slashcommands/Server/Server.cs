using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using miguelito_bot_slashcommands.Utils;

namespace miguelito_bot_slashcommands.slashcommands.Server
{
    internal class Server : ApplicationCommandModule
    {
        [SlashCommandGroup("Server", "Server related commands")]
        public class GroupContainer : ApplicationCommandModule
        {
            [SlashCommand("avatar", "Server ┇ Get the server avatar or server avatar you want")]
            public async Task ServerAvatar(InteractionContext ctx, [Option("server", "server id")] string serverID = null)
            {
                DiscordGuild guild = ctx.Guild;
                if (serverID != null)
                {
                    try
                    {
                        guild = await ctx.Client.GetGuildAsync(Convert.ToUInt64(serverID));
                    }
                    catch
                    {
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Servidor não encontrado."));
                        return;
                    }
                }
                DiscordEmbedBuilder embed = new()
                {
                    Color = Variables.Cores(),
                    ImageUrl = guild.GetIconUrl(ImageFormat.Png, 2048),
                };
                embed.WithAuthor(guild.Name, null, guild.IconUrl);
                embed.WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
                return;
            }

            [SlashCommand("info", "Server ┇ Get the server information or server information you want")]
            public async Task ServerInfo(InteractionContext ctx, [Option("server", "server id")] string serverID = null)
            {
                DiscordGuild guild = ctx.Guild;
                if (serverID != null)
                {
                    try
                    {
                        guild = await ctx.Client.GetGuildAsync(Convert.ToUInt64(serverID));
                    }
                    catch
                    {
                        await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent("Servidor não encontrado.").AsEphemeral(true));
                        return;
                    }
                }
                DiscordEmbedBuilder embed = new()
                {
                    Color = Variables.Cores(),
                };
                embed.AddField("**Informações do Servidor**", $"**{guild.Name}**\nID:**{guild.Id}\n" +
                    $"Criado em: **{guild.CreationTimestamp:dd/MM/yyyy}**", true);
                embed.AddField("**Informações do Dono**", $"**{guild.Owner.Nickname}**({guild.Owner.Id})\n**", true);
                embed.AddField("**Informações dos membros**", $"{guild.Members.Values.Count()} membros.\n" +
                   $"{guild.Members.Values.Count(p => p.Presence.Status == UserStatus.Online)} membros online.\n" +
                   $"{guild.Members.Values.Count(p => p.IsBot)} bots.", true);
                embed.AddField("**Informações dos cargos**", $"{guild.Roles.Count} cargos.", true);
                embed.AddField("**Informações dos canais**", $":speech_balloon: {guild.Channels.Values.Count(p => p.Type == ChannelType.Text)} canais de texto.\n" +
                    $":microphone2: {guild.Channels.Values.Count(p => p.Type == ChannelType.Voice)} canais de voz.\n" +
                    $":keyboard: {guild.Channels.Values.Count(p => p.Type == ChannelType.Category)} categorias.", true);
                embed.WithThumbnail(guild.GetIconUrl(ImageFormat.Png, 2048));
                embed.WithAuthor(guild.Name, null, guild.IconUrl);
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().AddEmbed(embed));
                return;
            }
        }
    }
}