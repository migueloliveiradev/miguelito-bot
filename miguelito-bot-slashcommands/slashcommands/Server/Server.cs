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
                await Methods.CommandsUsed("Server avatar", ctx.Guild.Id, ctx.User.Id);
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
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                            new DiscordInteractionResponseBuilder().WithContent("Servidor não encontrado.").AsEphemeral(true));
                        return;
                    }
                }
                DiscordEmbedBuilder embed = new()
                {
                    Color = Variables.Cores(),
                    Description = $":detective: ID do Servidor: **{guild.Id}**\n\n" +
                        $":crown: Dono: **{guild.Owner.Nickname}**({guild.Owner.Id})\n\n" +
                        $":hourglass_flowing_sand: Criado em: **{guild.CreationTimestamp:dd/MM/yyyy}**\n\n" +
                        $":speech_balloon: Canais: **{guild.Channels.Count}**\n\n" +
                        $":man_cartwheeling: Quantidade aproximada de Membros: **{guild.ApproximateMemberCount}**\n\n" +
                        $":man_cartwheeling: Quantidade aproximada de Membros online: **{guild.ApproximatePresenceCount}**\n\n" +
                        $":notebook_with_decorative_cover: Cargos: **{guild.Roles.Count}**",
                    ImageUrl = guild.GetIconUrl(ImageFormat.Png, 2048),
                };
                embed.WithThumbnail(guild.GetIconUrl(ImageFormat.Png, 2048));
                embed.WithAuthor(guild.Name, null, guild.IconUrl);
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder().AddEmbed(embed));
                await Methods.CommandsUsed("Server Info", ctx.Guild.Id, ctx.User.Id);
                return;
            }
        }
    }
}