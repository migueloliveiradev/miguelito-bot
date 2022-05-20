using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miguelito_bot_slashcommands.slashcommands.Server
{
    internal class Server : ApplicationCommandModule
    {
        [SlashCommandGroup("user", "commands to the user")]
        public class GroupContainer : ApplicationCommandModule
        {
            [SlashCommand("avatar", "receive your avatar or the desired user")]
            public async Task avatar(InteractionContext ctx, [Option("user", "user you want icon")] DiscordUser user = null)
            {
                DiscordEmbedBuilder embed;
                if (server == 0)
                {
                    embed = new DiscordEmbedBuilder
                    {
                        Description = $"{Formatter.MaskedUrl("Baixar", new Uri(ctx.Guild.IconUrl + "?size=2048"), "Baixar")} imagem",
                        Color = cores(),
                        ImageUrl = ctx.Guild.IconUrl + "?size=2048",
                    };
                    embed.WithAuthor(ctx.Guild.Name, null, ctx.Guild.IconUrl)
                         .WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
                    await ctx.RespondAsync(embed);
                }
                else
                {
                    try
                    {
                        DiscordGuild guild = await ctx.Client.GetGuildAsync(server);
                        embed = new DiscordEmbedBuilder
                        {
                            Description = $"{Formatter.MaskedUrl("Baixar", new Uri(guild.IconUrl + "?size=2048"), "Baixar")} imagem",
                            Color = cores(),
                            ImageUrl = guild.IconUrl + "?size=2048",
                        };
                        embed.WithAuthor(guild.Name, null, guild.IconUrl)
                             .WithFooter($"Solicitado por {ctx.User.Username}#{ctx.User.Discriminator}", ctx.User.AvatarUrl);
                        await ctx.RespondAsync(embed);
                    }
                    catch
                    {
                        await ctx.RespondAsync("Meu chapa, infelizmente eu não tô nesse server pra pegar o icon dele :pensive:");
                    }
                }
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            }

            [SlashCommand("info", "receive your information or the desired user")]
            public async Task info(InteractionContext ctx, [Option("user", "user you want information")] DiscordUser user = null)
            {
                DiscordEmbedBuilder embed;
                if (user == null)
                {
                    user = ctx.User;
                    embed = new DiscordEmbedBuilder
                    {
                        Title = ":person_doing_cartwheel: " + user.Username,
                        Color = color.cores(),
                        ImageUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048),
                        Description = ":hash: Tag do Discord: **" + user.Username + "#" + user.Discriminator + "**\n\n" +
                        ":detective: ID do Discord: **" + user.Id + "**\n\n" +
                        ":hourglass_flowing_sand: Conta criada em: **" + user.CreationTimestamp.ToString().Replace("+00:00", "") + "**",
                    };
                }
                else
                {
                    embed = new DiscordEmbedBuilder
                    {
                        Title = ":person_doing_cartwheel: " + user.Username,
                        Color = color.cores(),
                        ImageUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048),
                        Description = ":hash: Tag do Discord: **" + user.Username + "#" + user.Discriminator + "**\n\n" +
                         ":detective: ID do Discord: **" + user.Id + "**\n\n" +
                         ":hourglass_flowing_sand: Conta criada em: **" + user.CreationTimestamp.ToString().Replace("+00:00", "") + "**"
                    };
                }
                embed.WithThumbnail(user.AvatarUrl);
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            }
        }
    }
}
